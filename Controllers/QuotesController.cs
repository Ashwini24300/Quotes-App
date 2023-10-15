using QuotesApplivcation.BaseClass_Folder;
using QuotesApplivcation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuotesApplivcation.Controllers
{
    public class QuotesController : BaseController
    {
        //
        QuotesApplicationDatabaseEntities dbhandle = new QuotesApplicationDatabaseEntities();

        // Display :
        //------------------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            // getting all the quotes from all users.
            List<Quote> Quotes = dbhandle.Quotes.ToList();
            return View("ShowAllQuotes", Quotes);
        }

        // Add operation :
        //------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult AddQuotes()
        {
            //taking the user_id of currently logged in user.
            object user_id = (from usr in dbhandle.Users
                           where usr.email == User.Identity.Name
                           select usr).First().Id; 
            //
            return View("AddQuotesView",user_id);
        }

        //
        [HttpPost]
        public ActionResult AddQuotes(Quote quote)
        {
            //
            dbhandle.Quotes.Add(quote);
            dbhandle.SaveChanges(); 

            //
            return Redirect("/");
        }

        // Update :
        //------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult EditQuotes(int id)
        {
            //search for the quote here.
            Quote quoteToBeEdited = dbhandle.Quotes.Find(id);

            //
            return View("EditQuotesView", quoteToBeEdited);
        }

        //
        [HttpPost]
        public ActionResult EditQuotes(Quote quote)
        {
            //search for the quote here.
            Quote quoteToBeUpdated = dbhandle.Quotes.Find(quote.Id);
            quoteToBeUpdated.author = quote.author;
            quoteToBeUpdated.text = quote.text;

            //
            dbhandle.SaveChanges();

            //
            return Redirect("/");
        }

        // Delete :
        //------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult DeleteQuotes(int id)
        {
            //search for the quote here.
            Quote quoteToBeEdited = dbhandle.Quotes.Find(id);
            dbhandle.Quotes.Remove(quoteToBeEdited);

            //
            dbhandle.SaveChanges() ;

            //
            return Redirect("/");
        }

        // Like/UnLike :
        //------------------------------------------------------------------------------------------------
        public ActionResult LikeQuotes(int id)
        {
            //search for the quote here.
            Quote quote = dbhandle.Quotes.Find(id);

            // get the details of the user.
            int usrId = (from usr in dbhandle.Users
                        where usr.email == User.Identity.Name
                         select usr).First().Id;
            //
            if (quote.user_id == usrId)
            {
                object msg = "You cant like quotes owned by you..!!"; 
                return View("ShowAllQuotes",msg);
            }
            
            

            // if there is already entry in favourite_Quote table then
            //  object msg = "Quote is already liked by you..!!"; 
            //  return View("ShowAllQuotes", msg);

            //else make entry into favourite_Quote table and
            //  object msg = "Quote is liked by you..!!"; 
            //  return View("ShowAllQuotes", msg);


            //
            dbhandle.SaveChanges();

            //
            return null;
        }


    }
}