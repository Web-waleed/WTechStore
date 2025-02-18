using System.ComponentModel.DataAnnotations;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class Feedback 
    {
        public int FeedbackId { get; set; }
        public string FeedbackName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }

    }
}
