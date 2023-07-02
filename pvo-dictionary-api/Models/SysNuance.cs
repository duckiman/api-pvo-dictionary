using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class SysNuance : BaseModel
    {
        [Key]
        public int sys_nuance_id { get; set; }
        public string nuance_name { get; set; }
        public int nuance_type { get; set; }
        public int sort_order { get; set; }
    }
}
