using QuotesApplivcation.BaseClass_Folder;
using QuotesApplivcation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuotesApplivcation.Controllers
{
    public class UserController : BaseController
    {
        //
        QuotesApplicationDatabaseEntities dbhandle = new QuotesApplicationDatabaseEntities();

        // REGISTERING THE USER
        //---------------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterUser()
        {
            //
            return View("RegisterUser");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            //
            if (user.email == null && user.password == null)
            {
                object msg = "Email and Password fields are mandatory..!!";
                return View("RegisterUser",msg);
            }

            //
            dbhandle.Users.Add(user);
            dbhandle.SaveChanges();

            // 
            return Redirect("/User/SignIn");
        }

        // PROFILE UPDATION OF USER
        //---------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Profile()
        {
            // get the details of the user.
            object currentlyLoggedInUser = (from usr in dbhandle.Users
                              where usr.email == User.Identity.Name
                              select usr).First();

            // showing the user its details :
            return View("Profile", currentlyLoggedInUser);
        }

        [HttpPost]
        public ActionResult Profile(User user)
        {
            // finding out the user record.
            User UserToBeUpdated = (from usr in dbhandle.Users
                                            where usr.Id == user.Id
                                            select usr).First();
            // updating the values.
            UserToBeUpdated.first_name = user.first_name;
            UserToBeUpdated.last_name = user.last_name;
            UserToBeUpdated.email = user.email;
            UserToBeUpdated.mobile = user.mobile;

            // updating the database.
            dbhandle.SaveChanges();
            
            // 
            return Redirect("/");
        }

        // PASSWORD UPDATION OF USER
        //---------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult ChangePwd()
        {
            // showing the user its details :
            return View("ChangePwd");
        }
        [HttpPost]
        public ActionResult ChangePwd(FormCollection formCollection)
        {
            // finding out the user record.
            User temp = (from usr in dbhandle.Users
                                    where usr.email == User.Identity.Name
                                    select usr).First();
            //
            if (temp.password == formCollection[0])
            {
                if (formCollection[1] == formCollection[2])
                {
                    //
                    temp.password = formCollection[1];
                    dbhandle.SaveChanges();

                    //
                    return Redirect("/");
                }
                else
                {
                    object msg = "Password confirmation failed!!";
                    return View("ChangePwd", msg);
                }
            }
            else
            {
                object msg = "Enter correct old password !!";
                return View("ChangePwd", msg);
            }

            
            return null;
        }

        // LOGIN OPERATION :
        //---------------------------------------------------------------------------------------
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            //
            return View("SignIn");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(FormCollection formCollection)
        {
            // this email and password will be sent as key and input values as value for it.
            string emailToCheck = formCollection.Get("email");
            string passwdToCheck = formCollection.Get("password");

            try {
                //checking the credentials data with the data in the database.
                User userFound = (from usr in dbhandle.Users
                                  where usr.email == emailToCheck &&
                                        usr.password == passwdToCheck
                                  select usr).First();

                //
                if (userFound != null)
                {
                    //It performs two tasks for us :
                    // 1. creating the sessionObject for that username to be stored in session-pool in server.
                    //    in reply to client..it return this cookie value along with the command
                    //    to store it onto client side as cookies.Set-Cookie: 
                    // 2. Set the username to User global object...we will use it for the purpose of 
                    //    finding out who is currently logged in user.
                    FormsAuthentication.SetAuthCookie(userFound.email, false);
                    return Redirect("/");
                }

            }
            catch(Exception ex){
                object msg = "Credentials are invalid!!";
                return View("SignIn", msg);
            }
            
        return null;
        }

        // LOGOUT OPERATION :
        //---------------------------------------------------------------------------------------
        public ActionResult SignOut()
        {
            // this will dekete the sessionObject from session pool maintained by the server.
            FormsAuthentication.SignOut();

            // returning back to login page.
            return Redirect("/User/SignIn");
        }

    }
}