using pvo_dictionary_api.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class SysDialect : BaseModel
    {
        [Key]
        public int sys_dialect_id { get; set; }
        public string dialect_name { get; set; }
        public int dialect_type { get; set; }
        public int sort_order { get; set; }
    }
}
