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
    public class ConceptLinkController : BaseApiController<ConceptLinkController>
    {
        private readonly ConceptLinkService _conceptLinkService;
        public ConceptLinkController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _conceptLinkService = new ConceptLinkService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list_concept_link")]
        public MessageData GetListConceptLink()
        {
            try
            {
                var res = _conceptLinkService.GetListConceptLink(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }
    }
}
