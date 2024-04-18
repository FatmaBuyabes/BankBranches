using System.ComponentModel.DataAnnotations;

namespace BankBranches.Models
{
    public class NewBranchForm
    {
        [Required]
        public string name { get; set; }

        [Url]
        public string location { get; set; }
        [Required]
        public string branchManager { get; set; }
        [Required]//[Range(0, 10000)]
        public int employeeCount { get; set; }
    }
}
