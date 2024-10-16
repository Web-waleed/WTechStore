using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class advertisement : SharedProp
    {
        public int Id { get; set; }
        [Required]
        public string adsName { get; set; }
        public string adstitle { get; set; }
        public decimal? adsprice { get; set; }
        public String? adsimg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public String btntitle { get; set; }
    }
}
