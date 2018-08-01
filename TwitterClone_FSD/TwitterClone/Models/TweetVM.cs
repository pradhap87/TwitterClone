using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace TwitterClone.UI.Models
{
    public class TweetVM
    {
        public int Tweet_Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [MaxLength(140, ErrorMessage = "Tweet must be a maximum of 140 characters")]
        [StringLength(140)]
        public string message { get; set; }
        public DateTime created { get; set; }
        public string Search_User { get; set; }

        public string Followers { get; set; }
        public string Following { get; set; }
        public string TotalTwittes { get; set; }
    }
}