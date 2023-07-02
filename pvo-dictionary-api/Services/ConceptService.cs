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
    public class ConceptService
    {
        private readonly UserRepository _userRepository;
        private readonly DictionaryRepository _dictionaryRepository;
        private readonly ConceptRepository _conceptRepository;
        private readonly ExampleRepository _exampleRepository;
        private readonly ExampleRelationshipRepository _exampleRelationshipRepository;
        private readonly ConceptRelationshipRepository _conceptRelationshipRepository;
        private readonly ConceptLinkRepository _conceptLinkRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public ConceptService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _dictionaryRepository = new DictionaryRepository(apiOption, databaseContext, mapper);
            _conceptRepository = new ConceptRepository(apiOption, databaseContext, mapper);
            _exampleRepository = new ExampleRepository(apiOption, databaseContext, mapper);
            _exampleRelationshipRepository = new ExampleRelationshipRepository(apiOption, databaseContext, mapper);
            _conceptRelationshipRepository = new ConceptRelationshipRepository(apiOption, databaseContext, mapper);
            _conceptLinkRepository = new ConceptLinkRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        public object GetListConcept(int dictionaryId)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(dictionaryId);
                if (dictionary == null)
                {
                    throw new Exception("Dictionary does not exist!");
                }

                var conceptList = _conceptRepository.FindByCondition(row => row.dictionary_id == dictionaryId).ToList();
                return new
                {
                    ListConcept = conceptList,
                    LastUpdatedAt = dictionary.modified
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// add concept
        /// </summary>
        /// <param name="addConceptRequest"></param>
        /// <returns></returns>
        public object AddConcept(AddConceptRequest addConceptRequest)
        {
            try
            {
                var dictionary = _dictionaryRepository.FindOrFail(addConceptRequest.DictionaryId);
                if (dictionary == null)
                {
                    throw new ValidateError(3001, "Dictionary does not exist!");
                }

                var newConcept = new Concept()
                {
                    dictionary_id = addConceptRequest.DictionaryId,
                    title = addConceptRequest.Title,
                    description = addConceptRequest.Description,
                };
                _conceptRepository.Create(newConcept);
                _conceptRepository.SaveChange();

                // update dictionary
                dictionary.modified = DateTime.Now;
                _dictionaryRepository.UpdateByEntity(dictionary);
                _dictionaryRepository.SaveChange();

                return newConcept;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update concept
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UpdateConcept(UpdateConceptRequest request)
        {
            try
            {
                var concept = _conceptRepository.FindOrFail(request.ConceptId);
                if (concept == null)
                {
                    throw new Exception("Concept Id invalid!");
                }
                
                var conceptByTitleNumber = _conceptRepository.FindByCondition(row => row.concept_id != concept.concept_id && row.title == request.Title).Count();
                if(conceptByTitleNumber > 0)
                {
                    throw new Exception("Concept already exists");
                }

                concept.title = request.Title;
                if (!string.IsNullOrEmpty(request.Description))
                {
                    concept.description = request.Description;
                }
                concept.modified = DateTime.Now;
                _conceptRepository.UpdateByEntity(concept);
                _conceptRepository.SaveChange();

                return concept;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="isForced"></param>
        /// <returns></returns>
        public object DeleteConcept(int conceptId, bool? isForced)
        {
            try
            {
                var concept = _conceptRepository.FindOrFail(conceptId);
                if(concept == null)
                {
                    throw new Exception("Concept Id invalid");
                }
                _conceptRepository.DeleteByEntity(concept);
                _conceptRepository.SaveChange();

                return concept;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get concept
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        public object GetConcept(int conceptId)
        {
            try
            {
                var concept = _conceptRepository.FindOrFail(conceptId);
                if (concept == null)
                {
                    throw new Exception("Concept Id invalid");
                }

                var exampleRelationshipList = _exampleRelationshipRepository.FindByCondition(row => row.concept_id == concept.concept_id).ToList();
                var exampleIdList = exampleRelationshipList.Select(row => row.example_id).ToList();
                var exampleList = _exampleRepository.FindByCondition(row => exampleIdList.Contains(row.example_id)).ToList();

                return new
                {
                    Title = concept.title,
                    Description = concept.description,
                    ListExample = exampleList
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search concept
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="dictionaryId"></param>
        /// <returns></returns>
        public object SearchConcept(string searchKey, int? dictionaryId)
        {
            try
            {
                var query = _conceptRepository.FindByCondition(row => row.title.ToLower().Contains(searchKey.ToLower()) || row.description.ToLower().Contains(searchKey.ToLower()));
                if(dictionaryId != null)
                {
                    query = query.Where(row => row.dictionary_id == dictionaryId);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public object GetConceptRelationship(int conceptId, int parentId)
        {
            try
            {
                var conceptRelationship = _conceptRelationshipRepository.FindByCondition(row => row.concept_id == conceptId && row.parent_id == parentId).FirstOrDefault();
                if(conceptRelationship == null)
                {
                    throw new Exception("ConceptRelationship does not exist!");
                }
                var concepLink = _conceptLinkRepository.FindOrFail(conceptRelationship.concept_link_id);
                return concepLink;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update_concept_relationship 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UpdateConceptRelationship(UpdateConceptRelationshipReuqest request)
        {
            try
            {
                if (request.conceptId == request.parentId)
                {
                    throw new ValidateError(3003, "A concept can't link to itself!");
                }

                var concept = _conceptRepository.FindOrFail(request.conceptId);
                if(concept == null)
                {
                    throw new ValidateError(2000, "Concept does not exist!");
                }

                var parent = _conceptRepository.FindOrFail(request.parentId);
                if (parent == null)
                {
                    throw new ValidateError(2000, "Concept parent does not exist!");
                }

                var conceptRelationship = _conceptRelationshipRepository.FindByCondition(row => row.concept_id == request.conceptId && row.parent_id == request.parentId).FirstOrDefault();
                if(conceptRelationship == null)
                {
                    conceptRelationship = new ConceptRelationship()
                    {
                        concept_id = request.conceptId,
                        parent_id = request.parentId,
                        concept_link_id = request.conceptLinkId,
                        dictionary_id = parent.dictionary_id,
                    };

                    _conceptRelationshipRepository.Create(conceptRelationship);
                    _conceptRelationshipRepository.SaveChange();

                    return conceptRelationship;
                }

                conceptRelationship.concept_link_id = request.conceptLinkId;
                conceptRelationship.modified = DateTime.Now;
                _conceptRelationshipRepository.UpdateByEntity(conceptRelationship);
                _conceptRelationshipRepository.SaveChange();
                return conceptRelationship;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get tree
        /// </summary>
        /// <param name="conceptId"></param>
        /// <param name="conceptName"></param>
        /// <returns></returns>
        public object GetTree(int conceptId, string? conceptName)
        {
            try
            {
                var conceptRelationshipParentList = _conceptRelationshipRepository.FindByCondition(row =>  row.concept_id == conceptId).ToList();
                var conceptParentIdList = conceptRelationshipParentList.Select(row => row.parent_id).ToList();
                var listParent = _conceptRepository.FindByCondition(row => conceptParentIdList.Contains(row.concept_id)).ToList();

                var conceptRelationshipChildrenList = _conceptRelationshipRepository.FindByCondition(row => row.parent_id == conceptId).ToList();
                var conceptChildrenIdList = conceptRelationshipChildrenList.Select(row => row.concept_id).ToList();
                var listChildren = _conceptRepository.FindByCondition(row => conceptChildrenIdList.Contains(row.concept_id)).ToList();

                return new
                {
                    ListParent = listParent,
                    ListChildren = listChildren,
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get concept parents
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        public object GetConceptParents(int conceptId)
        {
            try
            {
                var conceptRelationshipParentList = _conceptRelationshipRepository.FindByCondition(row => row.concept_id == conceptId).ToList();
                var conceptParentIdList = conceptRelationshipParentList.Select(row => row.parent_id).ToList();
                var listParent = _conceptRepository.FindByCondition(row => conceptParentIdList.Contains(row.concept_id)).ToList();

                return listParent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get concept children
        /// </summary>
        /// <param name="conceptId"></param>
        /// <returns></returns>
        public object GetConceptChildren(int conceptId)
        {
            try
            {
                var conceptRelationshipChildrenList = _conceptRelationshipRepository.FindByCondition(row => row.parent_id == conceptId).ToList();
                var conceptChildrenIdList = conceptRelationshipChildrenList.Select(row => row.concept_id).ToList();
                var listChildren = _conceptRepository.FindByCondition(row => conceptChildrenIdList.Contains(row.concept_id)).ToList();

                return listChildren;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
