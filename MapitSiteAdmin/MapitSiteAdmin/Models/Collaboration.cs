using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MapitSiteAdmin.Areas.Identity.Data;

namespace MapitSiteAdmin.Models
{
    public class Collaboration
    {
        [Key]
        [Display(Name = "Id")]
        public int CollabId { get; set; }

        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string? Role { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;


        //foriegn key project:
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        // foreign key user
        // referencing the AspNetUsers table ( ApplicationUser.cs)
        public string UserId { get; set; }
        public virtual MapitUser MapitUser { get; set; }
    }
}
