using System.ComponentModel.DataAnnotations;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class Feedback 
    {
        public int FeedbackId { get; set; }
        public String FeedbackName { get; set; }
        [DataType(DataType.MultilineText)]
        public String Review { get; set; }

    }
}
