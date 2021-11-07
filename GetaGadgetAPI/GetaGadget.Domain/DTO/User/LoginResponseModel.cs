namespace GetaGadget.Domain.DTO.User
{
    public class LoginResponseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public int UserRole { get; set; }

        public string Token { get; set; }
    }
}
