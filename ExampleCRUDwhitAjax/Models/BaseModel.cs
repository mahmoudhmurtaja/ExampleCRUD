using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCRUDwhitAjax.Models
{
    public class ApplicationConstant
    {
        public const int MaxStringName = 250;
        public const int MinStringName = 3;
    }
    public class BaseModel
    {
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // For Search Operations.
        [NotMapped]
        public string? Keyword { get; set; }
    }
}
