using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class Dictionary : BaseModel
    {
        [Key]
        public int dictionary_id { get; set; }
        public int user_id { get; set; }
        public string dictionary_name { get; set; }
        public DateTime last_view_at { get; set; }
    }
}
