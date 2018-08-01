using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.DataLayer
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [StringLength(30)]
        public string UserId { get; set; }
        [Column("Password")]
        [StringLength(100)]
        public string Pwd { get; set; }
        [Column("Fullname")]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Email { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Joined { get; set; }
        public bool Active { get; set; }
        public string ProfileImage { get; set; }

        public ICollection<Following> following { get; set; }

        public ICollection<Tweet> tweet { get; set; }

    }
}
