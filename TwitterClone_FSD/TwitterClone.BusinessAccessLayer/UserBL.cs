using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataLayer;

namespace TwitterClone.BusinessLayer
{
    public class UserBL
    {
        TwitterContext db = new TwitterContext();
        public string AddUser(Person item)
        {
            string Status = null;
            var obj = db.Persons.SingleOrDefault(i => i.UserId == item.UserId);
            if (obj == null)
            {
                db.Persons.Add(item);
                db.SaveChanges();
            }
            else
            {
                Status = "User Id already exist in the System. Please try to logIn";
            }
            return Status;
        }

        public void AddFollowers(Following item)
        {
            db.Followers.Add(item);
            db.SaveChanges();
        }
        public void RemoveFollowers(Following item)
        {
            db.Database.ExecuteSqlCommand("Delete following where UserId = '" + item.UserId + "' and FollowerUserId = '" + item.FollowerUserId +"'");
        }

        public Person GetUser(string uId)
        {
            Person obj = db.Persons.SingleOrDefault(i => i.UserId == uId);
            return obj;
        }

        public bool GetFollower(string uId, string followUserId)
        {
            int obj = db.Followers.Where(x => x.UserId == uId).Where(x => x.FollowerUserId == followUserId).Count();

            return (obj > 0);
        }
        public void UpdateUser(Person item)
        {
            db.Persons.AddOrUpdate(item);
            db.SaveChanges();
        }
        public string DeleteUser(string uId)
        {
            db.Tweets.Where(x => x.UserId == uId).ToList().ForEach(x =>
            {
                db.Tweets.Remove(x);
            }
            );

            db.Followers.Where(x => x.UserId == uId).ToList().ForEach(x =>
            {
                db.Followers.Remove(x);
            }
            );

            db.Persons.Remove(db.Persons.Single(a => a.UserId == uId));

            db.SaveChanges();

            return "Sucess";
        }
        public Person Validate(string uname, string pwd)
        {
            using (TwitterContext db = new TwitterContext())
            {
                Person obj = db.Persons.SingleOrDefault(i => i.UserId == uname && i.Pwd == pwd);
                return obj;
            }
        }
    }
}
