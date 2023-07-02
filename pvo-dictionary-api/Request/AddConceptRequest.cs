using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class AddConceptRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DictionaryId { get; set; }
    }
}
