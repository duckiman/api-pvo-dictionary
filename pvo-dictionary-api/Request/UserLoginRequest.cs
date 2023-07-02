using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pvo_dictionary_api.Request
{
    public class UserLoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
