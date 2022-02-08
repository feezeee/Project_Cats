namespace Cat.API.Response
{
    public class AuthorizationResponse
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
