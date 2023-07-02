using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class AddDictionaryRequest
    {
        public string DictionaryName { get; set; }
        public int? CloneDictionaryId { get; set; }
    }
}
