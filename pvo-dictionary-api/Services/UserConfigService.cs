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
    public class UserConfigService
    {
        private readonly ToneRepository _toneRepository;
        private readonly ModeRepository _modeRepository;
        private readonly RegisterRepository _registerRepository;
        private readonly NuanceRepository _nuanceRepository;
        private readonly DialectRepository _dialectRepository;
        private readonly ExampleLinkRepository _exampleLinkRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public UserConfigService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _toneRepository = new ToneRepository(apiOption, databaseContext, mapper);
            _modeRepository = new ModeRepository(apiOption, databaseContext, mapper);
            _registerRepository = new RegisterRepository(apiOption, databaseContext, mapper);
            _nuanceRepository = new NuanceRepository(apiOption, databaseContext, mapper);
            _dialectRepository = new DialectRepository(apiOption, databaseContext, mapper);
            _exampleLinkRepository = new ExampleLinkRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// Get dictionary list by user
        /// </summary>
        /// <returns></returns>
        public object GetListExampleLink(int userId)
        {
            try
            {
                var exampleLink = _exampleLinkRepository.FindByCondition(row => row.user_id == userId).ToList();
                return exampleLink;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get_list_example_attribute
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetListExampleAttribute(int userId)
        {
            try
            {
                return new
                {
                    Tone = _toneRepository.FindByCondition(row => row.user_id==userId).ToList(),
                    Mode = _modeRepository.FindByCondition(row => row.user_id==userId).ToList(),
                    Register = _registerRepository.FindByCondition(row => row.user_id==userId).ToList(),
                    Nuance = _nuanceRepository.FindByCondition(row => row.user_id==userId).ToList(),
                    Dialect = _dialectRepository.FindByCondition(row => row.user_id==userId).ToList(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
