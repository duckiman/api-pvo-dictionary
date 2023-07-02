using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class UpdateDictionaryRequest
    {
        public int DictionaryId { get; set; }
        public string DictionaryName { get; set; }
    }
}
