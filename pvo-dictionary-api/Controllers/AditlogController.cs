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
    public class AditlogController : BaseApiController<AditlogController>
    {
        private readonly UserService _userService;
        public AditlogController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _userService = new UserService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="limit"></param>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_logs")]
        public MessageData GetLogs(DateTime dateFrom, DateTime dateTo, int pageIndex, int limit, string? searchFilter)
        {
            try
            {
                var res = _userService.GetLogs(UserId, dateFrom, dateTo, pageIndex, limit, searchFilter);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// Save audit log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save_log")]
        public MessageData SaveLog(SaveLogRequest request)
        {
            try
            {
                var res = _userService.SaveLog(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }
    }
}
