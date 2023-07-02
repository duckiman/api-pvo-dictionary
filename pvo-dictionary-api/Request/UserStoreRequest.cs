using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace pvo_dictionary_api
.Request
{
    public class UserStoreRequest
    {
        public string Name { get; set; }
        public string CitizenIdentification { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
