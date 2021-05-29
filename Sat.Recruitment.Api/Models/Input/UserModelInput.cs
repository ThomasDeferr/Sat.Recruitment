using System;
using System.ComponentModel.DataAnnotations;
using Sat.Recruitment.Entities.Models;
using Sat.Recruitment.Resources;

namespace Sat.Recruitment.Api.Models.Input
{
    public class UserModelInput
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "UserEmailRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "UserAddressRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "UserPhoneRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "UserTypeRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        public UserType? UserType { get; set; }

        [Required(ErrorMessageResourceName = "UserMoneyRequired", ErrorMessageResourceType = typeof(ValidationMessages))]
        [Range(0, 9999999999.99, ErrorMessageResourceName = "UserMoneyInvalid", ErrorMessageResourceType = typeof(ValidationMessages))]
        public decimal? Money { get; set; }
    }
}
