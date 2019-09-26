using System.Collections.Generic;

namespace Api.Response {
    public class AuthFailedResponse {
        public IEnumerable<string> Errors { get; set; }
    }
}