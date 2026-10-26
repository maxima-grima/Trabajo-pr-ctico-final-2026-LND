namespace Trabajo_Practico_Final_2026.Models.DTO.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
