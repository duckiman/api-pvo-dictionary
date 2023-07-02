using AutoMapper;
using pvo_dictionary_api.Common;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Respositories;

namespace pvo_dictionary_api.Repositories
{
    public class ConceptLinkRepository : BaseRespository<ConceptLink>
    {
        private readonly IMapper _mapper;
        public ConceptLinkRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            _mapper = mapper;
        }
    }
}
