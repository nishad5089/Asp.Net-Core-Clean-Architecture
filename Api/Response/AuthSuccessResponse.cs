using System;

namespace Api.Response
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public double? ExpiresIn { get; set; }
    }
}