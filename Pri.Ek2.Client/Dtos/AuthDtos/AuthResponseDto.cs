using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Dtos.AuthDtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserProfileResponseDto UserProfile { get; set; }
    }
}
