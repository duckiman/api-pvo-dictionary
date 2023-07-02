using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class UpdateUserInforRequest
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Position { get; set; }
        public IFormFile? avatar { get; set; }
    }
}
