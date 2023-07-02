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
    public class DictionaryController : BaseApiController<DictionaryController>
    {
        private readonly DictionaryService _dictionaryService;
        public DictionaryController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _dictionaryService = new DictionaryService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list_dictionary")]
        public MessageData GetListDictionary()
        {
            try
            {
                var res = _dictionaryService.GetListDictionary(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// access to dictionary
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("load_dictionary")]
        public MessageData LoadDictionary(int dictionaryId)
        {
            try
            {
                var res = _dictionaryService.LoadDictionary(UserId, dictionaryId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// AddDictionary
        /// </summary>
        /// <param name="addDictionaryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add_dictionary")]
        public MessageData AddDictionary(AddDictionaryRequest addDictionaryRequest)
        {
            try
            {
                var res = _dictionaryService.AddDictionary(UserId, addDictionaryRequest);
                if (res == null)
                {
                    return new MessageData { Data = null, Status = -1, ErrorCode = 2001, Des = "Dictionary name already in use" };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// update dictionary
        /// </summary>
        /// <param name="addDictionaryRequest"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update_dictionary")]
        public MessageData UpdateDictionary(UpdateDictionaryRequest updateDictionaryRequest)
        {
            try
            {
                var res = _dictionaryService.UpdateDictionary(UserId, updateDictionaryRequest);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("delete_dictionary")]
        public MessageData DeleteDictionary(int dictionaryId)
        {
            try
            {
                var res = _dictionaryService.DeleteDictionary(dictionaryId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("transfer_dictionary")]
        public MessageData TranferDictionary(TranferDictionaryRequest tranferDictionaryRequest)
        {
            try
            {
                var res = _dictionaryService.TranferDictionary(tranferDictionaryRequest);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get_number_record")]
        public MessageData GetNumberRecord(int dictionaryId)
        {
            try
            {
                var res = _dictionaryService.GetNumberRecord(dictionaryId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }
    }
}
