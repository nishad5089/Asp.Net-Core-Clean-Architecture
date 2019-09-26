using System;

namespace Infrastructure.Auth.JWT {
    public class JwtSettings {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}