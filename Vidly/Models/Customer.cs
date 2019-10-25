using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Veuillez introduire un Nom")] //System.ComponentModel.DataAnnotations; . Pour override err msg in validation  
        [MaxLength(255)]
        public string Name { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType MembershipType { get; set; } //C# ? Navigation property : navigate one type to another

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } // for optimization, load only foreign key not entire object. By convention <>Id and will be FK
        
        [Display(Name = "Date of Birth")] // need to recompil
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
    }
}