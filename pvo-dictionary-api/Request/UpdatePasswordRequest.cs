namespace pvo_dictionary_api.Request
{
    public class UpdatePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
