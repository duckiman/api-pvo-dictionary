using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class ResetPasswordRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword{ get; set; }
    }
}
