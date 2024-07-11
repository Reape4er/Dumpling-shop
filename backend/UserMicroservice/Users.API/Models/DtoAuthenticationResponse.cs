namespace Users.API.Models
{
    public class DtoAuthenticationResponse
    {
        public required string Token { get; set; }
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}
