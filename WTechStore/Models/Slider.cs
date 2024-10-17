using System.ComponentModel.DataAnnotations.Schema;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class Slider 
    {
        public int SliderId { get; set; }
        public String SliderTitle { get; set; }
        public String SliderDesc { get; set; }
        public String? SliderImg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public String btnTxt { get; set; }

    }
}
