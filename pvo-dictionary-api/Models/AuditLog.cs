using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class AuditLog : BaseModel
    {
        [Key]
        public int audit_log_id { get; set; }
        public int user_id { get; set; }
        public string screen_info { get; set; }
        public int action_type { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
    }
}
