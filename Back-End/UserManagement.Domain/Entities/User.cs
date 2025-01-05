using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
        public string Gender { get; set; }


        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone cannot be longer than 15 characters.")]
        public string Phone { get; set; }

        [Phone(ErrorMessage = "Invalid cell phone number format.")]
        [StringLength(15, ErrorMessage = "Cell phone cannot be longer than 15 characters.")]
        public string Cell { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street Name is required.")]
        [StringLength(100, ErrorMessage = "Street Name cannot be longer than 100 characters.")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Street Number is required.")]
      
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
      
        public int PostCode { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
