namespace Authorization.Application.Models.Responses
{
    public class GetUserTokenResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
    }
}
