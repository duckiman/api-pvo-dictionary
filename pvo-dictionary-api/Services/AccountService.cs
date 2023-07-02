using AutoMapper;
using AutoMapper.Configuration;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Repositories;
using pvo_dictionary_api.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Net.Mail;
using pvo_dictionary_api.Dto;
using BanVeXemPhimApi.Common;

namespace pvo_dictionary_api.Services
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public AccountService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public UserLoginDto UserLogin(UserLoginRequest userLoginRequest)
        {
            try
            {
                var user = _userRepository.UserLogin(userLoginRequest);
                if (user == null)
                {
                    return null;
                }
                if (user.status == 0)
                {
                    return new UserLoginDto()
                    {
                        token = "",
                        user = user,
                    };
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiOption.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claimList = new[]
                {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.UserData, user.user_name),
                new Claim(ClaimTypes.Sid, user.user_id.ToString()),
            };
                var token = new JwtSecurityToken(
                    issuer: _apiOption.ValidIssuer,
                    audience: _apiOption.ValidAudience,
                    expires: DateTime.Now.AddDays(1),
                    claims: claimList,
                    signingCredentials: credentials
                    );
                var tokenByString = new JwtSecurityTokenHandler().WriteToken(token);
                return new UserLoginDto()
                {
                    token = tokenByString,
                    user = user,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// account/register
        /// </summary>
        /// <param name="userRegisterRequest"></param>
        /// <returns></returns>
        public object UserRegister(UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var user = _userRepository.FindByCondition(row => row.user_name == userRegisterRequest.username).FirstOrDefault();
                if (user != null)
                {
                    return null;
                }
                var newUser = new User()
                {
                    user_name = userRegisterRequest.username,
                    password = Untill.CreateMD5(userRegisterRequest.password),
                    email = userRegisterRequest.username,
                    status = 0,
                };
                _userRepository.Create(newUser);
                _userRepository.SaveChange();
                return newUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send Activate Email
        /// </summary>
        /// <param name="sendActivateEmailRequest"></param>
        /// <returns></returns>
        public bool SendActivateEmail(SendActivateEmailRequest sendActivateEmailRequest)
        {
            try
            {
                var user = _userRepository.FindByCondition(row => row.user_name == sendActivateEmailRequest.username && row.password == Untill.CreateMD5(sendActivateEmailRequest.password)).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                var tb = "<div>Hello "+ sendActivateEmailRequest.username + "</div>" +
                    "<div>You have just registered account pvo dictionary system</div>" +
                    "<a href=\""+ _apiOption.BaseUrl + "/api/account/activate_account?username=" + sendActivateEmailRequest.username+"\">Click here to active your account</a>";

                MailMessage mail = new MailMessage("pvo.dictionary.hung.dv@gmail.com", sendActivateEmailRequest.username, "PVO Dictionary send email active account", tb);
                mail.IsBodyHtml = true;
                //gửi tin nhắn
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Host = "smtp.gmail.com";
                //ta không dùng cái mặc định đâu, mà sẽ dùng cái của riêng mình
                client.UseDefaultCredentials = false;
                client.Port = 587; // vì sử dụng Gmail nên mình dùng port 587
                                   // thêm vào credential vì SMTP server cần nó để biết được email + password của email đó mà bạn đang dùng
                client.Credentials = new System.Net.NetworkCredential("pvo.dictionary.hung.dv@gmail.com", "pjesgrpquyrjjzed");
                client.EnableSsl = true; //vì ta cần thiết lập kết nối SSL với SMTP server nên cần gán nó bằng true
                client.Send(mail);
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Activate Account
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ActivateAccount(string username)
        {
            try
            {
                var user = _userRepository.FindAll().Where(row => row.user_name == username).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                user.status = 1;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ForgotPassword(string email)
        {
            try
            {
                var user = _userRepository.FindAll().Where(row => row.user_name == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                var listNumber = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                var otp = "";
                Random rand = new Random();
                for (int i = 0; i< 4; i++)
                {
                    var randomNumber = rand.Next(0, 9);
                    otp += listNumber[randomNumber];
                }
                user.otp = otp;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                var tb = "<div>Hello " + user.user_name + "</div>" +
                    "<div>OTP Code is: "+ user.otp + "</div>" +
                    "<div>Dont share this code to anyone</div>";

                MailMessage mail = new MailMessage("pvo.dictionary.hung.dv@gmail.com", user.user_name, "PVO Dictionary send OTP to reset password", tb);
                mail.IsBodyHtml = true;
                //gửi tin nhắn
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Host = "smtp.gmail.com";
                //ta không dùng cái mặc định đâu, mà sẽ dùng cái của riêng mình
                client.UseDefaultCredentials = false;
                client.Port = 587; // vì sử dụng Gmail nên mình dùng port 587
                                   // thêm vào credential vì SMTP server cần nó để biết được email + password của email đó mà bạn đang dùng
                client.Credentials = new System.Net.NetworkCredential("pvo.dictionary.hung.dv@gmail.com", "pjesgrpquyrjjzed");
                client.EnableSsl = true; //vì ta cần thiết lập kết nối SSL với SMTP server nên cần gán nó bằng true
                client.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public bool ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var user = _userRepository.FindAll().Where(row => row.user_name == resetPasswordRequest.Email).FirstOrDefault();
                if (user == null)
                {
                    throw new ValidateError(2000, "Email doesn’t exist");
                }

                if(user.otp != resetPasswordRequest.Otp)
                {
                    throw new ValidateError(3003, "OTP invalid!");
                }

                user.password = Untill.CreateMD5(resetPasswordRequest.NewPassword);
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
