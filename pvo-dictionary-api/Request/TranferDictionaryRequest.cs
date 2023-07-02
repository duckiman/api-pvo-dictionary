using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class TranferDictionaryRequest
    {
        public int SourceDictionaryId { get; set; }
        public int DestDictionaryId { get; set; }
        public bool IsDeleteDestData { get; set; }
    }
}
