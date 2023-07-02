using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Repositories;
using pvo_dictionary_api.Request;

namespace pvo_dictionary_api.Services
{
    public class ConceptLinkService
    {
        private readonly UserRepository _userRepository;
        private readonly DictionaryRepository _dictionaryRepository;
        private readonly ConceptRepository _conceptRepository;
        private readonly ConceptLinkRepository _conceptLinkRepository;
        private readonly ConceptRelationshipRepository _conceptRelationshipRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public ConceptLinkService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _dictionaryRepository = new DictionaryRepository(apiOption, databaseContext, mapper);
            _conceptRepository = new ConceptRepository(apiOption, databaseContext, mapper);
            _conceptLinkRepository = new ConceptLinkRepository(apiOption, databaseContext, mapper);
            _conceptRelationshipRepository = new ConceptRelationshipRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        public object GetListConceptLink(int userId)
        {
            try
            {
                var conceptLinkList = _conceptLinkRepository.FindByCondition(row => row.user_id == userId).ToList();

                return conceptLinkList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
