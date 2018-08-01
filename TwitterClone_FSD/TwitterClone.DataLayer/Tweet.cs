using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TwitterClone.DataLayer;

namespace TwitterClone.DataLayer
{
    [Table("Tweet")]
    public class Tweet
    {
          [Key]          
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Tweet_Id { get; set; }
          
          [StringLength(30)]
          public string UserId { get; set; }

          [StringLength(140)]
          public string message { get; set; }

          [Column(TypeName = "Date")]
          public DateTime created { get; set; }

          [ForeignKey("UserId")]
          public Person Persons { get; set; }
          
    }
}
