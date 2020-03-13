using BootApp.Domain.DataAccess;
using BootApp.Models;
using Domain.Entities.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootApp.Controllers
{
    public class HomeController : Controller
    {
        private AuthorDA _authorDA;
        private AuthorDA AuthorDA
        {
            get { return _authorDA ?? (_authorDA = new AuthorDA()); }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult basic() 
        {
            var author = new Author
            {
                Biography = "...",
                FirstName = "Jamie",
                LastName = "Munro"
            };

            //AuthorDA.Inst(author);

            var authors = AuthorDA.GetAll();


            var model = ToModel(authors);


            return View(model);
        }

        public List<AuthorModel> ToModel(List<Author> Authors)
        {
            List<AuthorModel> AuthorModel =
              Authors.Select(p => new AuthorModel()
              {
                  Id            = p.Id,
                  LastName      = p.LastName,
                  FirstName     = p.FirstName,
                  Biography     = p.Biography,
                  Created_at    = p.Created_at
              }).ToList();

            return AuthorModel;
        }
    }
}