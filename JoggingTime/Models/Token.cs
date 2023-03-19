using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JoggingTime.Models
{
    public class Token : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; } = true;

        public virtual User User { get; set; }
    }
}
