using AutoMapper;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Request;
using pvo_dictionary_api.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace pvo_dictionary_api.Repositories
{
    public class UserRepository : BaseRespository<User>
    {
        private IMapper _mapper;
        public UserRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// UserLogin function. That return User by user login request
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public User UserLogin(UserLoginRequest userLoginRequest)
        {
            try
            {
                var passwordByMD5 = Untill.CreateMD5(userLoginRequest.password);
                return Model.Where(row => row.user_name == userLoginRequest.username && row.password == passwordByMD5).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Check user register
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public bool CheckUserRegister(User user)
        //{
        //    try
        //    {
        //        var userlist = Model.Where(row => row.Username == user.Username || row.NumberPhone == user.NumberPhone).ToList();
        //        if(userlist.Count > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
    }
}
