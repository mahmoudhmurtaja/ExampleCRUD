using ExampleCRUDwhitAjax.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCRUDwhitAjax.Models
{
	[Index(nameof(Email), IsUnique = true, Name = "IX_Users_UniqueEmail")]
	[Index(nameof(PhoneNumber), IsUnique = true, Name = "IX_Users_UniquePhoneNo")]
	public class Employe : BaseModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

		[Display(Name = "PhoneNumber", ResourceType = typeof(Messages))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
		[Phone(ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(Messages))]
		public string PhoneNumber { get; set; }

		[Display(Name = "Email", ResourceType = typeof(Messages))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
		[EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Messages))]
		public string Email { get; set; }

		[Display(Name = "Address", ResourceType = typeof(Messages))]
		[StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
		public string Address { get; set; }
	}
}
