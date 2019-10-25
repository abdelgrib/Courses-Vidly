using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    // custom validation need using System.ComponentModel.DataAnnotations;
    // we should apply Min18YearsIfAMember to a member
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) // choose second overload with context param
        {
            var customer = (Customer)validationContext.ObjectInstance;
            
            if (customer.MembershipTypeId == MembershipType.Unknown || // 0 is a value of the no-option option in a drop down
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                
                return ValidationResult.Success; //if success use static member succes

            if (customer.Birthday == null)
                return new ValidationResult("Birthday est requis."); // instanciate in error

            var age = DateTime.Today.Year - customer.Birthday.Value.Year; // Value because nullable Date
            return age >= 18 
                ? ValidationResult.Success 
                : new ValidationResult("L'utilisateur doit avoir au mois 18 pour s'abonner");

        }

    }
}