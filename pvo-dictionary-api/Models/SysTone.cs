using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class SysTone : BaseModel
    {
        [Key]
        public int sys_tone_id { get; set; }
        public string tone_name { get; set; }
        public int tone_type { get; set; }
        public int sort_order { get; set; }
    }
}
