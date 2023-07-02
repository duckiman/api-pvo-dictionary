using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class Concept : BaseModel
    {
        [Key]
        public int concept_id { get; set; }
        public int dictionary_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
