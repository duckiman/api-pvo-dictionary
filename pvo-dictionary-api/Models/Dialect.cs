using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class Dialect : BaseModel
    {
        [Key]
        public int dialect_id { get; set; }
        public int sys_dialect_id { get; set; }
        public int user_id { get; set; }
        public string dialect_name { get; set; }
        public int dialect_type { get; set; }
        public int sort_order { get; set; }

    }
}
