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
using Microsoft.AspNetCore.StaticFiles;

namespace pvo_dictionary_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TemplateController : BaseApiController<TemplateController>
    {
        private readonly ConceptService _conceptService;
        public TemplateController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _conceptService = new ConceptService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("download")]
        public async Task<IActionResult> download()
        {
            try
            {
                //return download file
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "default_template_protect.xlsx");

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filepath, out var contenttype))
                {
                    contenttype = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(bytes, contenttype, Path.GetFileName(filepath));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
