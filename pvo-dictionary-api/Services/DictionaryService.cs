using AutoMapper;
using BanVeXemPhimApi.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Repositories;
using pvo_dictionary_api.Request;

namespace pvo_dictionary_api.Services
{
    public class DictionaryService
    {
        private readonly UserRepository _userRepository;
        private readonly DictionaryRepository _dictionaryRepository;
        private readonly ConceptRepository _conceptRepository;
        private readonly ExampleRepository _exampleRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public DictionaryService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _dictionaryRepository = new DictionaryRepository(apiOption, databaseContext, mapper);
            _conceptRepository = new ConceptRepository(apiOption, databaseContext, mapper);
            _exampleRepository = new ExampleRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        public object GetListDictionary(int userId)
        {
            try
            {
                var dictionariesList = _dictionaryRepository.FindAll().Where(row => row.user_id == userId).ToList();
                return dictionariesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// access to dictionary
        /// </summary>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        public object LoadDictionary(int userId, int dictionaryId)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(dictionaryId);
                if(dictionary == null)
                {
                    throw new ValidateError(2000, "Dictionary doesn’t exist"); 
                }
                return dictionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add Dictionary
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addDictionaryRequest"></param>
        /// <returns></returns>
        public object AddDictionary(int userId, AddDictionaryRequest addDictionaryRequest)
        {
            try
            {
                var dictionaryByName = _dictionaryRepository.FindAll().Where(row => row.dictionary_name == addDictionaryRequest.DictionaryName && row.user_id == userId);
                if (dictionaryByName.Count() > 0)
                {
                    return null;
                }

                var newDictionary = new Dictionary()
                {
                    user_id = userId,
                    dictionary_name = addDictionaryRequest.DictionaryName,
                };

                // Clone case
                //if(addDictionaryRequest.CloneDictionaryId != null)
                //{
                //    newDictionary.
                //}

                _dictionaryRepository.Create(newDictionary);
                _dictionaryRepository.SaveChange();
                return newDictionary;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update dictionary
        /// </summary>
        /// <param name="updateDictionaryRequest"></param>
        /// <returns></returns>
        public object UpdateDictionary(int userId, UpdateDictionaryRequest updateDictionaryRequest)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(updateDictionaryRequest.DictionaryId);
                if(dictionary == null || dictionary.user_id != userId)
                {
                    throw new ValidateError(2000, "Dictionary doesn’t exist");
                }

                var dictionaryListByName = _dictionaryRepository.FindByCondition(row => row.dictionary_name == updateDictionaryRequest.DictionaryName);
                if (dictionaryListByName.Count() > 0)
                {
                    throw new Exception("Dictionary name already in use");
                }
                dictionary.dictionary_name = updateDictionaryRequest.DictionaryName.Trim();
                dictionary.modified = DateTime.Now;

                _dictionaryRepository.UpdateByEntity(dictionary);
                _dictionaryRepository.SaveChange();
                return dictionary;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateDictionaryRequest"></param>
        /// <returns></returns>
        public object DeleteDictionary(int dictionaryId)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(dictionaryId);
                if (dictionary == null)
                {
                    throw new ValidateError(2000, "Dictionary doesn’t exist");
                }

                _dictionaryRepository.DeleteByEntity(dictionary);
                _dictionaryRepository.SaveChange();
                return dictionary;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object TranferDictionary(TranferDictionaryRequest tranferDictionaryRequest)
        {
            try
            {
                var sourceDictionary = _dictionaryRepository.FindOrFail(tranferDictionaryRequest.SourceDictionaryId);
                if (sourceDictionary == null)
                {
                    throw new Exception("Source dictionary is empty");
                }

                var destDictionary = _dictionaryRepository.FindOrFail(tranferDictionaryRequest.DestDictionaryId);
                if (destDictionary == null)
                {
                    throw new Exception("Dest dictionary is empty");
                }

                var conceptListBySourceDictionaryId = _conceptRepository.FindByCondition(row => row.dictionary_id == tranferDictionaryRequest.SourceDictionaryId).ToList();
                foreach(var concept in conceptListBySourceDictionaryId)
                {
                    concept.dictionary_id = tranferDictionaryRequest.DestDictionaryId;
                    _conceptRepository.Create(concept);
                    _conceptRepository.SaveChange();
                }

                var exampleListBySourceDictionaryId = _exampleRepository.FindByCondition(row => row.dictionary_id == tranferDictionaryRequest.SourceDictionaryId).ToList();
                foreach (var example in exampleListBySourceDictionaryId)
                {
                    example.dictionary_id = tranferDictionaryRequest.DestDictionaryId;
                    _exampleRepository.Create(example);
                    _exampleRepository.SaveChange();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetNumberRecord(int dictionaryId)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(dictionaryId);
                if (dictionary == null)
                {
                    throw new Exception("Dictionary is empty");
                }

                var numberConcept = _conceptRepository.FindByCondition(row => row.dictionary_id == dictionaryId).Count();
                var numberExample = _exampleRepository.FindByCondition(row => row.dictionary_id == dictionaryId).Count();

                return new
                {
                    numberConcept = numberConcept,
                    numberExample = numberExample
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
