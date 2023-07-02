using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class ExampleRelationship : BaseModel
    {
        [Key]
        public int example_relationship_id { get; set; }
        public int dictionary_id { get; set; }
        public int concept_id { get; set; }
        public int example_id { get; set; }
        public int example_link_id { get; set; }

    }
}
