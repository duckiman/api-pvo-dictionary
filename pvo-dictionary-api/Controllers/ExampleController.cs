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
using pvo_dictionary_api.Models;

namespace pvo_dictionary_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExampleController : BaseApiController<ExampleController>
    {
        private readonly ExampleService _exampleService;
        public ExampleController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _exampleService = new ExampleService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add_example")]
        public MessageData AddExample(AddExampleRequest request)
        {
            try
            {
                var res = _exampleService.AddExample(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// search example
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="toneId"></param>
        /// <param name="modeId"></param>
        /// <param name="registerId"></param>
        /// <param name="nuanceId"></param>
        /// <param name="dialectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search_example")]
        public MessageData SearchExample(string? keyword, int? toneId, int? modeId, int? registerId, int? nuanceId, int? dialectId)
        {
            try
            {
                var res = _exampleService.SearchExample(keyword, toneId, modeId, registerId, nuanceId, dialectId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get example id
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_example")]
        public MessageData GetExample(int exampleId)
        {
            try
            {
                var res = _exampleService.GetExample(exampleId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// update example
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_example")]
        public MessageData UpdateExample(UpdateExampleRequest request)
        {
            try
            {
                var res = _exampleService.UpdateExample(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get example id
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete_example")]
        public MessageData DeleteExample(int exampleId)
        {
            try
            {
                var res = _exampleService.DeleteExample(exampleId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
