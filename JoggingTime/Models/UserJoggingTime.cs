using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingTime.Models
{
    public class UserJoggingTime:BaseModel
    {
        public DateTime JoggingDate { get; set; }
        public double JoggingDuration { get; set; }
        public double Distance { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
