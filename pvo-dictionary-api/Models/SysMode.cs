using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class SysMode : BaseModel
    {
        [Key]
        public int sys_mode_id { get; set; }
        public string mode_name { get; set; }
        public int mode_type { get; set; }
        public int sort_order { get; set; }
    }
}
