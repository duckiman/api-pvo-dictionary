using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Models
{
    public class UserSetting : BaseModel
    {
        [Key]
        public int user_setting_id { get; set; }
        public int user_id { get; set; }
        public string setting_key { get; set; }
        public string setting_value { get; set; }

    }
}
