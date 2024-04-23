using System.ComponentModel.DataAnnotations;

namespace BankBranches.Models
{
    public class AddEmployeeForm 
    {
        
        
        [Required]
        public string Name { get; set; }


        [RegularExpression(@"^\d{12}$", ErrorMessage = "Civil ID must be 12 digits.")]
        public string CivilId { get; set; }


        [Required]
        public string Position { get; set; }

        
    }
}
