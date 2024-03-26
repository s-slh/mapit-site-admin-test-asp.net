using System.ComponentModel.DataAnnotations;

namespace MapitSiteAdmin.Models
{
    public class Category
    {
        [Key]
        public int CategId { get; set; }
        public string? Name { get; set; }

        //Relation ship
        //one to many done
        public List<Diagram>? Diagrams { get; set; }
    }
}
