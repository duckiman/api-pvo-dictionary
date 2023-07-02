using pvo_dictionary_api.Models;

namespace pvo_dictionary_api.Dto
{
    public class UserLoginDto
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
