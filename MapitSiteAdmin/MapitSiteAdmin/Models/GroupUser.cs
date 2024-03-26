using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MapitSiteAdmin.Areas.Identity.Data;

namespace MapitSiteAdmin.Models
{
    public class GroupUser
    {
        [Key]
        [Display(Name = "Id")]
        public int GroupUserId { get; set; }

        //foreign key group
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group? Group { get; set; }
        //foreign key user
        public string? UserId { get; set; }
        public virtual MapitUser MapitUser { get; set; }
    }
}
