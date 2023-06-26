namespace AccountService.Dtos
{
    public class UserCreateDto
    {
        public string UserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;


        public string Email { get; set; } = null!;

    }
}
