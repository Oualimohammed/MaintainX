namespace Pri.Ek2.Client.Dtos.AuthDtos
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}
