using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pvo_dictionary_api.Models
{
    public class User : BaseModel
    {
        [Key]
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string? email { get; set; }
        public string? display_name { get; set; }
        public string? full_name { get; set; }
        public DateTime? birthday { get; set; }
        public string? position { get; set; }
        public string? avatar { get; set; }
        public int status { get; set; }
        public string? otp { get; set; } = "";
    }
}
