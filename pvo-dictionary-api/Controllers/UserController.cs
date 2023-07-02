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
    [Authorize]
    public class UserController : BaseApiController<UserController>
    {
        private readonly UserService _userService;
        public UserController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _userService = new UserService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="userRegisterRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_password")]
        public MessageData UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            try
            {
                var res = _userService.UpdatePassword(UserId, updatePasswordRequest);
                if (res == null)
                {
                    return new MessageData { Data = res, Status = -1, ErrorCode = 1000, Des = "Incorrect email or password!" };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// update user info
        /// </summary>
        /// <param name="updateUserInforRequest"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update_user_info")]
        public MessageData UpdateUserInfor(UpdateUserInforRequest updateUserInforRequest)
        {
            try
            {
                var res = _userService.UpdateUserInfor(UserId, updateUserInforRequest);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }
    }
}
