using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class Example : BaseModel
    {
        [Key]
        public int example_id { get; set; }
        public int dictionary_id { get; set; }
        public string detail { get; set; }
        public string detail_html { get; set; }
        public string note { get; set; }
        public int tone_id { get; set; }
        public int register_id { get; set; }
        public int dialect_id { get; set; }
        public int mode_id { get; set; }
        public int nuance_id { get; set; }
    }
}
