using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnviroment;

        public BookController(ApplicationDbContext context,IWebHostEnvironment webHostEnviroment) {
            this.context = context;
            this.webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            var booksvm = context.Books.Include(book=>book.Authors)
                .Include(book=>book.Categories)
                .ThenInclude(book=>book.Category).ToList()
                .Select((book) => new BookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Authors.Name,
                    Publisher = book.Publisher,
                    PublishDate = book.PublishDate,
                    ImageUrl = book.ImageUrl,
                    Categories = book.Categories.Select(bookCategory=>bookCategory.Category.Name).ToList(),
                }).ToList();
            return View(booksvm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.Authors.OrderBy((author) => author.Name).ToList();
            var  authorsList = authors.Select((author) => new SelectListItem
            {
                Value = author.Id.ToString(),
                Text = author.Name,

            }).ToList();


            var categoryList = context.Categories.ToList().Select((category) =>


               new SelectListItem
               {
                   Value = category.Id.ToString(),
                   Text = category.Name,

               }).ToList();


            var bookFormvm = new BookFormVM() {
               Authors= authorsList,
               Categories=categoryList
            };


            return View("BookForm", bookFormvm);


        }

        [HttpPost]
        public IActionResult Create(BookFormVM bookFormvm)
        {
            if (!ModelState.IsValid)
            {
                return View("BookForm",bookFormvm);
            }
            string  imgName = null;
            if (bookFormvm!=null) {
                 imgName = Path.GetFileName(bookFormvm.ImageUrl.FileName);
                var path = Path.Combine($"{webHostEnviroment.WebRootPath}/img/books", imgName);
                var stream = System.IO.File.Create(path);
                bookFormvm.ImageUrl.CopyTo(stream);
            }

            var book = new Book
            {
                Title = bookFormvm.Title,
                AuthorId = bookFormvm.AuthorId,
                Publisher = bookFormvm.Publisher,
                PublishDate = bookFormvm.PublishDate,
                Description = bookFormvm.Description,
                ImageUrl=imgName,
                Categories = bookFormvm.SelectedCategories.Select((categoryId) => new BookCategory
                {
                    CategoryId = categoryId,


                }).ToList(),
            };
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) {
            var book = context.Books.Find(id);
            if (book != null)
            {
                if (book.ImageUrl != null)
                {
                    var path = Path.Combine(webHostEnviroment.WebRootPath, "img/books", book.ImageUrl);
                    System.IO.File.Delete(path);

                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
  
            return RedirectToAction("Index");
        
                }







    }



}
