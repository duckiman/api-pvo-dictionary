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
    public class ConceptController : BaseApiController<ConceptController>
    {
        private readonly ConceptService _conceptService;
        public ConceptController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _conceptService = new ConceptService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get_list_concept")]
        public MessageData GetListConcept(int dictionaryId)
        {
            try
            {
                var res = _conceptService.GetListConcept(dictionaryId);
                if (res == null)
                {
                    return new MessageData { Data = res, Status = -1 };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add_concept")]
        public MessageData AddConcept(AddConceptRequest addConceptRequest)
        {
            try
            {
                var res = _conceptService.AddConcept(addConceptRequest);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// update concept
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_concept")]
        public MessageData UpdateConcept(UpdateConceptRequest request)
        {
            try
            {
                var res = _conceptService.UpdateConcept(request);
                if (res == null)
                {
                    return new MessageData { Data = res, Status = -1 };
                }
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// Delete concept
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="isForced"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete_concept")]
        public MessageData DeleteConcept(int conceptId, bool? isForced)
        {
            try
            {
                var res = _conceptService.DeleteConcept(conceptId, isForced);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// get concept
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_concept")]
        public MessageData GetConcept(int conceptId)
        {
            try
            {
                var res = _conceptService.GetConcept(conceptId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// search concept
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search_concept")]
        public MessageData SearchConcept(string searchKey, int? dictionaryId)
        {
            try
            {
                var res = _conceptService.SearchConcept(searchKey, dictionaryId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// get linkage relationship between the child concept and parent concept
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_concept_relationship")]
        public MessageData GetConceptRelationship(int conceptId, int parentId)
        {
            try
            {
                var res = _conceptService.GetConceptRelationship(conceptId, parentId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message, Status = -2 };
            }
        }

        /// <summary>
        /// update_concept_relationship
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update_concept_relationship")]
        public MessageData UpdateConceptRelationship(UpdateConceptRelationshipReuqest request)
        {
            try
            {
                var res = _conceptService.UpdateConceptRelationship(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get tree
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="conceptName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_tree")]
        public MessageData GetTree(int conceptId, string? conceptName)
        {
            try
            {
                var res = _conceptService.GetTree(conceptId, conceptName);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get concept parents
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_concept_parents")]
        public MessageData GetConceptParents(int conceptId)
        {
            try
            {
                var res = _conceptService.GetConceptParents(conceptId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Get concept children
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get_concept_children")]
        public MessageData GetConceptChildren(int conceptId)
        {
            try
            {
                var res = _conceptService.GetConceptChildren(conceptId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
