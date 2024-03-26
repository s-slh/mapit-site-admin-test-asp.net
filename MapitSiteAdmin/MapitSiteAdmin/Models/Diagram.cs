using MapitSiteAdmin.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace MapitSiteAdmin.Models
{
    public class Diagram
    {
        [Key]
        [Display(Name = "Id")]
        public int DiagramId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; } = "Dafault name";

        public string? Specification { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //one to many comments
        public List<Comment>? Comments { get; set; }

        //foreign key project id
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        //foreign key category id
        public int CategId { get; set; }
        public Category? Category { get; set; }

        // foreign key user
        // referencing the AspNetUsers table ( ApplicationUser.cs)
        public string UserId { get; set; }
        public virtual MapitUser MapitUser { get; set; }


    }
}
