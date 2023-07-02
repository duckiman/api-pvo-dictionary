using AutoMapper;
using BanVeXemPhimApi.Common;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Repositories;
using pvo_dictionary_api.Request;
using System;
using System.Collections.Generic;

namespace pvo_dictionary_api.Services
{
    public class ExampleService
    {
        private readonly ToneRepository _toneRepository;
        private readonly ModeRepository _modeRepository;
        private readonly RegisterRepository _registerRepository;
        private readonly NuanceRepository _nuanceRepository;
        private readonly DialectRepository _dialectRepository;
        private readonly ExampleLinkRepository _exampleLinkRepository;
        private readonly ExampleRepository _exampleRepository;
        private readonly ConceptRepository _conceptRepository;
        private readonly ExampleRelationshipRepository _exampleRelationshipRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public ExampleService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _toneRepository = new ToneRepository(apiOption, databaseContext, mapper);
            _modeRepository = new ModeRepository(apiOption, databaseContext, mapper);
            _registerRepository = new RegisterRepository(apiOption, databaseContext, mapper);
            _nuanceRepository = new NuanceRepository(apiOption, databaseContext, mapper);
            _dialectRepository = new DialectRepository(apiOption, databaseContext, mapper);
            _exampleLinkRepository = new ExampleLinkRepository(apiOption, databaseContext, mapper);
            _exampleRepository = new ExampleRepository(apiOption, databaseContext, mapper);
            _conceptRepository = new ConceptRepository(apiOption, databaseContext, mapper);
            _exampleRelationshipRepository = new ExampleRelationshipRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// AddExample
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object AddExample(int userId, AddExampleRequest request)
        {
            try
            {
                foreach (var item in request.ListExampleRelationship)
                {
                    var concept = _conceptRepository.FindOrFail(item.ConceptId);
                    var exampleLink = _exampleLinkRepository.FindOrFail(item.ExampleLinkId);
                    if (concept != null && exampleLink != null)
                    {
                        var example = new Example()
                        {
                            dictionary_id = concept.dictionary_id,
                            detail = request.Detail,
                            detail_html = request.DetailHtml,
                            note = request.Note,
                            tone_id = request.ToneId,
                            register_id = request.RegisterId,
                            dialect_id = request.DialectId,
                            mode_id = request.ModeId,
                            nuance_id = request.NuanceId,
                        };

                        _exampleRepository.Create(example);
                        _exampleRepository.SaveChange();

                        var exampleRelationship = new ExampleRelationship()
                        {
                            dictionary_id = concept.dictionary_id,
                            concept_id = item.ConceptId,
                            example_link_id = item.ExampleLinkId,
                            example_id = example.example_id,
                        };
                        _exampleRelationshipRepository.Create(exampleRelationship);
                        _exampleRelationshipRepository.SaveChange();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
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
        public object SearchExample(string? keyword, int? toneId, int? modeId, int? registerId, int? nuanceId, int? dialectId)
        {
            try
            {
                var query = _exampleRepository.FindAll();

                if(!string.IsNullOrEmpty(keyword) )
                {
                    query = query.Where(row => row.detail.ToLower().Contains(keyword.ToLower()));
                }
                if(toneId != null)
                {
                    query = query.Where(row => row.tone_id == toneId);
                }
                if(modeId != null)
                {
                    query = query.Where(row => row.mode_id == modeId);
                }
                if(registerId != null)
                {
                    query = query.Where(row => row.register_id == registerId);
                }
                if(nuanceId != null)
                {
                    query = query.Where(row => row.nuance_id == nuanceId);
                }
                if(dialectId != null)
                {
                    query = query.Where(row => row.dialect_id == dialectId);
                }

                return query.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get example
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        public object GetExample(int exampleId)
        {
            try
            {
                var example = _exampleRepository.FindOrFail(exampleId);
                return example;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update example
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UpdateExample(UpdateExampleRequest request)
        {
            try
            {
                var example = _exampleRepository.FindOrFail(request.ExampleId);
                if(example == null)
                {
                    throw new ValidateError(2000, "Example doesn’t exist");
                }

                example.detail = request.Detail;
                example.detail_html = request.DetailHtml;
                example.note = request.Note;
                example.tone_id = request.ToneId;
                example.register_id = request.RegisterId;
                example.dialect_id = request.DialectId;
                example.mode_id = request.ModeId;
                example.nuance_id = request.NuanceId;

                _exampleRepository.UpdateByEntity(example);
                _exampleRepository.SaveChange();

                return example;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// delete example
        /// </summary>
        /// <param name="exampleId"></param>
        /// <returns></returns>
        public object DeleteExample(int exampleId)
        {
            try
            {
                var example = _exampleRepository.FindOrFail(exampleId);
                if (example == null)
                {
                    throw new ValidateError(2000, "Example doesn’t exist");
                }

                _exampleRepository.DeleteByEntity(example);
                _exampleRepository.SaveChange();

                // @todo
                // delete relationship

                return example;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
