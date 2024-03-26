using MapitSiteAdmin.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Xml.Linq;



namespace MapitSiteAdmin.Models
{
    public class Project
    {
        [Key]
        [Display(Name = "Id")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string? Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //Relationship:
        
        //one to many Diagrams
        public List<Diagram>? Diagrams { get; set; }

        // foreign key user
        // referencing the AspNetUsers table ( ApplicationUser.cs)
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual MapitUser? MapitUser { get; set; } 

    }
}
