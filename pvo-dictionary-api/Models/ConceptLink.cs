using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class ConceptLink : BaseModel
    {
        [Key]
        public int concept_link_id { get; set; }
        public int sys_concept_link_id { get; set; }
        public string concept_link_name { get; set; }
        public int concept_link_type { get; set; }
        public int user_id { get; set; }
        public int sort_order { get; set; }
    }
}
