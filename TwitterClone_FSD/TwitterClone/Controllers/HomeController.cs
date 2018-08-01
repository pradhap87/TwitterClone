using HandsOnMVCUsingExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.BusinessAccessLayer;
using TwitterClone.DataLayer;
using TwitterClone.UI.Models;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {

        TweetBL obj = new TweetBL();
        public ActionResult Index()
        {
            try
            {
                ViewBag.Message = "Twitter Clone.";
                if (Session["UserName"] != null)
                {
                    List<int> count = obj.GetCount(Session["UserId"].ToString());
                    Session["Following"] = count[0].ToString();
                    Session["Followers"] = count[1].ToString();
                    Session["TotalTwittes"] = count[2].ToString();
                    return View();
                }
                else
                    return RedirectToAction("Index", "User");
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Index", "Home");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Index(TweetVM itemTweet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tweet t = new Tweet()
                    {
                        UserId = Session["UserId"].ToString(),
                        message = itemTweet.message,
                        created = DateTime.Now
                    };
                    obj.AddTweet(t);
                    return RedirectToAction("Index");

                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Index", "Home");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Tweet()
        {
            try
            {
                List<Tweet> t = new List<Tweet>();
                t = obj.GetTweet(Session["UserId"].ToString());
                return View(t);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Tweet", "Home");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Tweet(TweetVM collection)
        {
            try
            {
                List<Tweet> t = new List<Tweet>();
                t = obj.GetTweet(Session["UserId"].ToString());
                return View(t);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Tweet", "Home");
                return View("Error");
            }
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                Tweet t = new Tweet();
                t = obj.GetTweetbyID(id);
                TweetVM tweetVM = new TweetVM()
                {
                    UserId = t.UserId,
                    Tweet_Id = t.Tweet_Id,
                    message = t.message,

                };

                return View(tweetVM);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Edit", "Home");
                return View("Error");
            }
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TweetVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tweet t = new Tweet()
                    {
                        Tweet_Id = id,
                        UserId = collection.UserId,
                        message = collection.message,
                        created = DateTime.Now
                    };

                    obj.UpdateTweet(t);
                    return RedirectToAction("Index");

                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Edit", "Home");
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Tweet t = new Tweet();
                t = obj.GetTweetbyID(id);
                TweetVM tweetVM = new TweetVM()
                {
                    UserId = t.UserId,
                    Tweet_Id = t.Tweet_Id,
                    message = t.message,

                };

                return View(tweetVM);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Delete", "Home");
                return View("Error");
            }
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, TweetVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tweet t = new Tweet()
                    {
                        Tweet_Id = id,
                        UserId = collection.UserId,
                        message = collection.message,
                        created = DateTime.Now
                    };

                    obj.DeleteTweet(t);
                    return RedirectToAction("Index");

                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Delete", "Home");
                return View("Error");
            }
        }
    }
}
