using MapitSiteAdmin.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MapitSiteAdmin.Models
{
    public class Comment
    {
        [Key]
        [Display(Name = "Id")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Content field is required")]

        public string Content { get; set; } = "Empty";
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //Relationship
        //foreign key diagram
        public int DiagramId { get; set; }
        public Diagram? Diagram { get; set; }
        
        // foreign key user
        // referencing the AspNetUsers table ( ApplicationUser.cs)
        public string UserId { get; set; }
        public virtual MapitUser MapitUser { get; set; }

    }
}
