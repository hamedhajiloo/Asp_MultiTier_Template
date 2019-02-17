using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prj.Services.Models
{
    public class UserEditBindingModel
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        [Range(0, 1)]
        public byte Gender { get; set; }
        public string BirthDate { get; set; }
        public bool NewsLetter { get; set; }
        public string DisplayName { get; set; }
        public String BankCardNumber { get; set; }
        public IList<DefaultKeyValueServiceModel<int, string>> SocialNetworks { get; set; }
    }

}
