using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace TwitterClone.UI.Models
{
    public class PersonVM
    {
        [Required]
        public string UserId { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Pwd { get; set; }
        [DisplayName("Fullname")]
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Joined { get; set; }
        public bool Active { get; set; }
        public string ProfileImage { get; set; }
        public String status { get; set; }
        public String followstatus { get; set; }
    }
}