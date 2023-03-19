using System.ComponentModel.DataAnnotations.Schema;

namespace JoggingTime.ViewModels.Jogging
{
    public class JoggingViewModel
    {
        public int ID { get; set; }
        public DateTime JoggingDate { get; set; }
        public double JoggingDuration { get; set; }
        public double Distance { get; set; }
        public string UserName { get; set; }
    }
}