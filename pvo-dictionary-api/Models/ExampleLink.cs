using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class ExampleLink : BaseModel
    {
        [Key]
        public int example_link_id { get; set; }
        public int dictionary_id { get; set; }
        public int sys_example_link_id { get; set; }
        public string example_link_name { get; set; }
        public int example_link_type { get; set; }
        public int user_id { get; set; }
        public int sort_order { get; set; }

    }
}
