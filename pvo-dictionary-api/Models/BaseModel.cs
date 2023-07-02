using System;

namespace pvo_dictionary_api.Models
{
    public class BaseModel
    {
        public DateTime created_date { get; set; } = DateTime.UtcNow;
        public DateTime modified { get; set; }
    }
}
