using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;
        public AuthorsController(ApplicationDbContext context) {
            this.context = context;
        }
        public IActionResult Index()
        {
            var authorsvm = context.Authors.ToList().Select((author) =>
            new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                UpdatededAt = author.UpdatededAt,
                CreatedAt = author.CreatedAt,
            }).ToList();

            return View(authorsvm);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View("AuthorForm");

        }

        [HttpPost]
        public IActionResult Create(AuthorFormVM authorvm)
        {
            if (!ModelState.IsValid)
            {
                return View("AuthorForm", authorvm);

            }
            var author = new Author()
            {
                Id = authorvm.Id,
                Name = authorvm.Name,

            };
            try {



                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            } catch {
                ModelState.AddModelError("Name", errorMessage: "author name already exists");
                return View("AuthorForm", authorvm);
            }
            

        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var author = context.Authors.Find(id);
            var authorFormvm = new AuthorFormVM()
            {
                Id = author.Id,
                Name = author.Name,
            };
            return View("AuthorForm", authorFormvm);
        }
        [HttpPost]
        public IActionResult Edit(AuthorFormVM authorfvm)
        {

            if (!ModelState.IsValid)
            {
                return View("AuthorForm", authorfvm);
            }
            try {
                var author = context.Authors.Find(authorfvm.Id);
                author.Name = authorfvm.Name;
                author.UpdatededAt = DateTime.Now;
                context.SaveChanges();
                return RedirectToAction("Index");
            } catch {
                ModelState.AddModelError("Name", errorMessage: "author name already exists");
                return View("AuthorForm", authorfvm);
            }
            



        }



        public IActionResult CheckName(AuthorFormVM authorformvm)
        {
            var result = context.Authors.Any((auth) => auth.Name == authorformvm.Name);
            return Json(!result);
        }


        public IActionResult Details(int id )
        {
            var auth = context.Authors.Find(id);
            var authvm = new AuthorVM
            {
                Id = auth.Id,
                Name = auth.Name,
                CreatedAt = auth.CreatedAt,
                UpdatededAt = auth.UpdatededAt,

            };

            return View(authvm);
        }





        public IActionResult Delete(int id)
        {
            var author = context.Authors.Find(id);
            context.Authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
