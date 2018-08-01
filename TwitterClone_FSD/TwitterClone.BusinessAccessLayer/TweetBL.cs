using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataLayer;
using System.Data.Entity.Migrations;

namespace TwitterClone.BusinessAccessLayer
{
    public class TweetBL
    {
        TwitterContext db = new TwitterContext();
        public void AddTweet(Tweet item)
        {
            db.Tweets.Add(item);
            db.SaveChanges();
        }
        public void UpdateTweet(Tweet item)
        {
            db.Tweets.AddOrUpdate(item);
            db.SaveChanges();
        }
        public void DeleteTweet(Tweet item)
        {
            db.Tweets.Attach(item);
            db.Tweets.Remove(item);
            db.SaveChanges();
        }

        public List<Tweet> GetTweet(String UserId)
        {
            var blogs = db.Tweets.SqlQuery("SELECT Tweet.Tweet_Id, Tweet.UserId, Tweet.message, Tweet.created FROM Following INNER JOIN Person ON Following.UserId = Person.UserId INNER JOIN Tweet ON Person.UserId = Tweet.UserId WHERE Following.FollowerUserId ='" + UserId + "'").ToList();
            List<Tweet> tweets = new List<Tweet>();
            foreach (Tweet t in blogs)
            {
                tweets.Add(t);
            }
            var blogs_my = db.Tweets.SqlQuery("SELECT Tweet.Tweet_Id, Tweet.UserId, Tweet.message, Tweet.created FROM Tweet where UserId='" + UserId + "'").ToList();
            foreach (Tweet t in blogs_my)
            {
                tweets.Add(t);
            }
            return tweets;
        }

        public List<int> GetCount(String UserId)
        {
            List<int> list = new List<int>();
            //How may i am following 
            var Following_count = db.Followers.Where(f => f.FollowerUserId == UserId)
                .Select(f => f.FollowerUserId).Count();
            list.Add(Following_count);

            //How many of them following me
            var Followers_count = db.Followers.Where(f => f.UserId == UserId)
                .Select(f => f.UserId).Count();
            list.Add(Followers_count);

            //My Total twittes 
            var Tweets_Count = db.Tweets.Where(t => t.UserId == UserId)
                .Select(t => t.Tweet_Id).Count();
            list.Add(Tweets_Count);
            return list;
        }

        public Tweet GetTweetbyID(int Id)
        {
            var blogs = db.Tweets.SqlQuery("SELECT * FROM dbo.Tweet where Tweet_Id =" + Id).ToList();
            Tweet tweets = new Tweet();
            tweets.Tweet_Id = blogs[0].Tweet_Id;
            tweets.message = blogs[0].message;
            tweets.UserId = blogs[0].UserId;

            return tweets;
        }
    }
}
