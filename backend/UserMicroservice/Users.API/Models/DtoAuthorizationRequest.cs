namespace Users.API.Models
{
    public class DtoAuthenticationRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
