using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.DataLayer;
using TwitterClone.BusinessLayer;
using TwitterClone.UI.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using HandsOnMVCUsingExceptionHandling;

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        UserBL obj = new UserBL();
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                Session["UserName"] = null;
                ViewBag.Message = "";
                return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Index", "User");
                return View("Error");
            }
        }

        //
        // GET: /User/Login
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                Session["UserName"] = null;
                ViewBag.Message = "";
                return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Login", "User");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(PersonVM item)
        {
            try
            {
                Person objPerson = new Person();
                objPerson = obj.Validate(item.UserId, Encrypt(item.Pwd));
                if (objPerson != null)
                {
                    Session["UserName"] = objPerson.Name;
                    Session["UserId"] = objPerson.UserId;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Username / password not correct";
                    return View();
                }

            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Login", "User");
                return View("Error");
            }
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(PersonVM item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Person p = new Person()
                    {
                        UserId = item.UserId,
                        Pwd = Encrypt(item.Pwd),
                        Email = item.Email,
                        Name = item.Name,
                        Active = true,
                        Joined = DateTime.Now
                    };
                    var status = obj.AddUser(p);
                    if (status == null)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Message = status;
                        return View();
                    }


                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Create", "User");
                return View("Error");
            }
        }


        public string Encrypt(string asInputString)
        {
            #region Variables
            string encodedData = "";
            #endregion
            try
            {
                byte[] encData_byte = new byte[asInputString.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(asInputString);
                encodedData = Convert.ToBase64String(encData_byte);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Encrypt", "User");
                throw Ex;
            }
            return encodedData;
        }

        public string Decrypt(string asEncryptedString)
        {
            string result = null;
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(asEncryptedString);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                result = new String(decoded_char);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Decrypt", "User");
                throw Ex;
            }
            return result;
        }

        //
        // GET: /User/
        [HttpGet]
        public ActionResult TweeterProfile()
        {
            try
            {
                Person objPerson = new Person();
                objPerson = obj.GetUser(Session["UserId"].ToString());
                PersonVM personVM = new PersonVM()
                {
                    UserId = objPerson.UserId,
                    Name = objPerson.Name,
                    Pwd = Decrypt(objPerson.Pwd).ToString(),
                    Email = objPerson.Email,
                    ProfileImage = objPerson.ProfileImage,
                    Joined = objPerson.Joined,
                    Active = objPerson.Active
                };
                return View(personVM);
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "TwotterProfile", "User");
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult TweeterProfile(PersonVM item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Person p = new Person()
                    {
                        UserId = item.UserId,
                        Name = item.Name,
                        Email = item.Email,
                        ProfileImage = item.ProfileImage,
                        Pwd = Encrypt(item.Pwd),
                        Active = true,
                        Joined = item.Joined
                    };

                    obj.UpdateUser(p);
                    Person objPerson = new Person();
                    objPerson = obj.GetUser(Session["UserId"].ToString());
                    PersonVM personVM = new PersonVM()
                    {
                        UserId = objPerson.UserId,
                        Name = objPerson.Name,
                        Pwd = Decrypt(objPerson.Pwd).ToString(),
                        Email = objPerson.Email,
                        ProfileImage = objPerson.ProfileImage,
                        Joined = objPerson.Joined,
                        Active = objPerson.Active,
                        status = "Profile Updated"
                    };

                    return View(personVM);

                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "TwitterProfile", "User");
                return View("Error");
            }
        }

        public ActionResult Delete(PersonVM item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = obj.DeleteUser(item.UserId.ToString());

                    return View("Delete");

                }
                else
                    return View();
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "Delete", "User");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            return View("Index");
        }

        //[HttpPost]
        public ActionResult SearchUser(TweetVM collection)
        {
            try
            {
                if (collection.Search_User != null)
                {
                    Session["SerchText"] = collection.Search_User;
                    Person objPerson = new Person();
                    objPerson = obj.GetUser(Session["SerchText"].ToString());
                    if (objPerson != null)
                    {
                        var followStatus = obj.GetFollower(objPerson.UserId, Session["UserId"].ToString());

                        PersonVM personVM = new PersonVM()
                        {
                            UserId = objPerson.UserId,
                            Name = objPerson.Name,
                            Pwd = Decrypt(objPerson.Pwd).ToString(),
                            Email = objPerson.Email,
                            ProfileImage = objPerson.ProfileImage,
                            Joined = objPerson.Joined,
                            Active = objPerson.Active,
                            status = ""
                        };
                        if (followStatus != true)
                        {
                            personVM.followstatus = "false";
                        }
                        else
                            personVM.followstatus = "true";

                      return View(personVM);
                    }
                    else
                    {
                        PersonVM personVM = new PersonVM()
                        {
                            status = "No user found"
                        };
                        return View(personVM);
                    }
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "SearchUser", "User");
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Un_FollowUser(PersonVM collection)
        {
            try
            {

                Following f = new Following()
                {
                    UserId = collection.UserId,
                    FollowerUserId = Session["UserId"].ToString()
                };
                obj.RemoveFollowers(f);

                Person objPerson = new Person();
                objPerson = obj.GetUser(Session["SerchText"].ToString());

                PersonVM personVM = new PersonVM()
                {
                    UserId = objPerson.UserId,
                    Name = objPerson.Name,
                    Pwd = Decrypt(objPerson.Pwd).ToString(),
                    Email = objPerson.Email,
                    ProfileImage = objPerson.ProfileImage,
                    Joined = objPerson.Joined,
                    Active = objPerson.Active,
                    status = ""
                };
                return RedirectToAction("Index", "Home");
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "FollowUser", "User");
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult FollowUser(PersonVM collection)
        {
            try
            {

                Following f = new Following()
                {
                    UserId = collection.UserId,
                    FollowerUserId = Session["UserId"].ToString()
                };
                obj.AddFollowers(f);

                Person objPerson = new Person();
                objPerson = obj.GetUser(Session["SerchText"].ToString());

                PersonVM personVM = new PersonVM()
                {
                    UserId = objPerson.UserId,
                    Name = objPerson.Name,
                    Pwd = Decrypt(objPerson.Pwd).ToString(),
                    Email = objPerson.Email,
                    ProfileImage = objPerson.ProfileImage,
                    Joined = objPerson.Joined,
                    Active = objPerson.Active,
                    status = ""
                };
                return RedirectToAction("Index", "Home");
            }
            catch (Exception Ex)
            {
                Logger.WriteError(Ex, "FollowUser", "User");
                return View("Error");
            }

        }

    }
}
