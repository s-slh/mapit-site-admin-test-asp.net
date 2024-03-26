using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapitSiteAdmin.Models;
using Microsoft.AspNetCore.Identity;

namespace MapitSiteAdmin.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MapitUser class
public class MapitUser : IdentityUser
{ //Relation ship
    //one to many Comments
    public List<Comment> Comments;
    //one to many diagrams
    public List<Diagram> Diagrams;
    
    //one to many Projects
    public virtual ICollection<Project> Projects { get; set; }


}

