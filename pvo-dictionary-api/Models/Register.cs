using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class Register : BaseModel
    {
        [Key]
        public int register_id { get; set; }
        public int sys_register_id { get; set; }
        public int user_id { get; set; }
        public string register_name { get; set; }
        public int register_type { get; set; }
        public int sort_order { get; set; }

    }
}
