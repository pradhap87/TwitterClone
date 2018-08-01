using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.DataLayer
{
    [Table("Following")]
    public class Following
    {
        public string UserId { get; set; }
        public string FollowerUserId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int following_id { get; set; }
        [ForeignKey("UserId")]
        public Person User_Id { get; set; }
    }
}
