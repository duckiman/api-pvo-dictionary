using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class ConceptRelationship : BaseModel
    {
        [Key]
        public int concept_relationship_id { get; set; }
        public int dictionary_id { get; set; }
        public int concept_id { get; set; }
        public int parent_id { get; set; }
        public int concept_link_id { get; set; }
    }
}
