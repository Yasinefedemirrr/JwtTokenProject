using System.Text.Json.Serialization;

namespace JWT.WEBUI.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
