using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        // - exclude MembershipType = domain class : dependency : if we change this class our Dto will be impacted. We need to either use primitive type or custom Dto (MembershipTypeDto)
        // - we don't need display attributes : will not used in forms
        // - it's new class

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        //[Min18YearsIfAMember] //using Vidly.Models; //com because IHttpActionResult
        public DateTime? Birthday { get; set; }
    }
}