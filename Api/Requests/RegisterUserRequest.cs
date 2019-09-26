using System.ComponentModel.DataAnnotations;

namespace Api.Requests {
    public class RegisterUserRequest {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}