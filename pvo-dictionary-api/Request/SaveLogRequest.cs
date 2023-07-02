using System.ComponentModel.DataAnnotations;

namespace pvo_dictionary_api.Request
{
    public class SaveLogRequest
    {
        public int ActionType { get; set; }
        public string ScreenInfo { get; set; }
        public string Description { get; set; }
        public string? Reference { get; set; }
    }
}
