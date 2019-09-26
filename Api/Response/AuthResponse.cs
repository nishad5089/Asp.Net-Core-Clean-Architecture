namespace Api.Response {
    public class AuthResponse<T> {
        public AuthResponse () { }

        public AuthResponse (T response) {
            AuthToken = response;
        }

        public T AuthToken { get; set; }
    }
}