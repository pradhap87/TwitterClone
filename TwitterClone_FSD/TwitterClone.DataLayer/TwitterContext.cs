using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.DataLayer
{
   public class TwitterContext:DbContext
    {
        public TwitterContext() : base("FSD_MVCConnectionString")
        {

        }
        //Entity Set
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Following> Followers { get; set; }
    }
}
