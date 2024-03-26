using MapitSiteAdmin.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MapitSiteAdmin.Models
{
    public class Group
    {
        [Key]
        [Display(Name = "Id")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string? Name { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
