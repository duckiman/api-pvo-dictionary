using AutoMapper;
using AutoMapper.Configuration;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Controllers;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Dto;
using pvo_dictionary_api.Request;
using pvo_dictionary_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace pvo_dictionary_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly AccountService _accountService;
        public AccountController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _accountService = new AccountService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get achievement list of user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public MessageData Login(UserLoginRequest userLoginRequest)
        {
            try
            {
                var res = _accountService.UserLogin(userLoginRequest);
                if (res == null)
                {
                    return new MessageData { Data = res, Status = -1, ErrorCode = 1000, Des = "Incorrect email or password!" };
                }
                if (!string.IsNullOrEmpty(res.token))
                {
                    return new MessageData { Data = res, Status = -1, ErrorCode = 1004, Des = "Unactivated account!" };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// account register
        /// </summary>
        /// <param name="userRegisterRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public MessageData Register(UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var res = _accountService.UserRegister(userRegisterRequest);
                if(res == null)
                {
                    return new MessageData { Data = res, Status = -1, ErrorCode =1001, Des = "Email has been used!" };
                }
                return new MessageData { Data = res, Status = 1};
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// Send Activate Email
        /// </summary>
        /// <param name="sendActivateEmailRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("send_activate_email")]
        public MessageData SendActivateEmail(SendActivateEmailRequest sendActivateEmailRequest)
        {
            try
            {
                var res = _accountService.SendActivateEmail(sendActivateEmailRequest);
                if (!res)
                {
                    return new MessageData { Data = res, Status = -1, ErrorCode = 1002, Des = "Invalid account!" };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// Activate Account
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("activate_account")]
        public ContentResult ActivateAccount(string username)
        {
            try
            {
                var res = _accountService.ActivateAccount(username);
                if (res)
                {
                    return base.Content("<div style=\"width: 100%; height: 100vh; display: flex; justify-content: center; align-items: center; font-size: 25px;\">Account "+ username + " actived successful!</div>", "text/html");
                }
                return base.Content("<div>Active tài khoản thất bại</div>", "text/html");
            }
            catch (Exception ex)
            {
                return base.Content("<div>Error: "+ex.Message+"</div>", "text/html");
            }
        }

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("forgot_password")]
        public MessageData ForgotPassword(string email)
        {
            try
            {
                var res = _accountService.ForgotPassword(email);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("reset_password")]
        public MessageData ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var res = _accountService.ResetPassword(resetPasswordRequest);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }
    }
}
