using LibraryControl.Business.Abstract;
using LibraryControl.Entity.Entities;
using LibraryControl.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryControl.Controllers
{
    public class AdminController : Controller
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IWriterService _writerService;
        private ISaleService _saleService;
        public AdminController(IBookService bookService, ICategoryService categoryService, IWriterService writerService, ISaleService saleService)
        {
            this._bookService = bookService;
            this._categoryService = categoryService;
            this._writerService = writerService;
            this._saleService = saleService;
        }

        public async Task<IActionResult> Books(string categoryName, string search)
        {
            var books = await _bookService.GetAllByCategoryNameAsync(categoryName, search);

            string title = "Tüm kayıtlar listeleniyor.";

            if (!string.IsNullOrEmpty(categoryName))
            {
                title = categoryName + " kategorisindeki kayıtlar listeleniyor.";
            }

            if (!string.IsNullOrEmpty(search))
            {
                title = title + " Kayıtlar üzerinde aranan kelime: '" + search + "'.";
            }

            ViewBag.Title = title;

            return View(books);
        }

        public async Task<IActionResult> Book(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync((int)id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDetails = new BookDetailModel()
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                Price = book.Price,
                Writer = book.Writer.Name,
                Category = book.Category.Name,
                ImageUrl = book.ImageUrl,
                Stock = book.Stock,
                Sold = book.Sales.Where(i => i.RecordType == false).Count(),
                Leased = book.Sales.Where(i => i.RecordType == true).Count()
            };

            return View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _bookService.DeleteAsync((int)id);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla silindi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("books");
        }

        public async Task<IActionResult> RegisterBook()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Writers = await _writerService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBook(BookModel model, IFormFile? file)
        {
            if (!ModelState.IsValid || file == null)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Writers = await _writerService.GetAllAsync();
                if (file == null)
                {
                    TempData["imageError"] = "Görsel seçimi boş geçilemez.";
                }
                return View(model);
            }

            var book = new Book()
            {
                Stock = (int)model.Stock,
                Name = model.Name,
                Description = model.Description,
                Price = (double)model.Price,
                WriterId = model.WriterId,
                CategoryId = model.CategoryId
            };

            var imageExtantion = Path.GetExtension(file.FileName);
            var imageName = string.Format($"{Guid.NewGuid()}{imageExtantion}");
            book.ImageUrl = imageName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var result = await _bookService.CreateAsync(book);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla oluşturuldu.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("books");
        }

        public async Task<IActionResult> EditBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync((int)id);

            if (book == null)
            {
                return NotFound();
            }

            var bookModel = new BookModel()
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                Stock = book.Stock,
                Price = book.Price,
                WriterId = book.WriterId,
                CategoryId = book.CategoryId,
                ImageUrl = book.ImageUrl
            };

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Writers = await _writerService.GetAllAsync();

            return View(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookModel model, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Writers = await _writerService.GetAllAsync();
                return View(model);
            }

            var book = await _bookService.GetByIdAsync(model.Id);

            if (book == null)
            {
                return NotFound();
            }

            book.Id = model.Id;
            book.Name = model.Name;
            book.Description = model.Description;
            book.Stock = (int)model.Stock;
            book.WriterId = model.WriterId;
            book.CategoryId = model.CategoryId;
            book.Price = (double)model.Price;
            book.ImageUrl = model.ImageUrl;

            if (file != null)
            {
                var imageExtantion = Path.GetExtension(file.FileName);
                var imageName = string.Format($"{Guid.NewGuid()}{imageExtantion}");
                book.ImageUrl = imageName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", imageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var result = await _bookService.UpdateAsync(book);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla güncellendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("books", "admin");
        }

        public async Task<IActionResult> Sales(string search, string selectFilter)
        {
            var sales = await _saleService.GetAllAsync();

            string title = "Tüm Kayıtlar listeleniyor.";

            if (!string.IsNullOrEmpty(search))
            {
                sales = await _saleService.GetAllBySearchAsync(search);
                title = title + " Kayıtlar üzerinde aranan kelime: '" + search + "'.";
            }

            switch (selectFilter)
            {
                case "all":
                    title = "Tüm kayıtlar listeleniyor.";
                    break;
                case "sale":
                    title = "Sadece satın alma kayıtları listeleniyor.";
                    sales = sales.Where(i => i.RecordType == false).ToList();
                    break;
                case "lease":
                    title = "Sadece kiralama kayıtları listeleniyor.";
                    sales = sales.Where(i => i.RecordType == true).ToList();
                    break;
            }

            return View(sales);
        }

        [HttpPost]
        public async Task<IActionResult> Sales(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _saleService.ConfirmLeaseAsync((int)id);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Teslim alma başarılı.",
                AlertType = "primary"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("sales");
        }

        public async Task<IActionResult> BookSale(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetByIdAsync((int)id);

            var saleModel = new SaleModel()
            {
                BookId = book.Id,
                BookName = book.Name,
                BookImageUrl = book.ImageUrl,
                BookPrice = book.Price
            };

            return View(saleModel);
        }

        [HttpPost]
        public async Task<IActionResult> BookSale(SaleModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var sale = new Sale()
            {
                Name = model.Name,
                Surname = model.Surname,
                Phone = model.Phone,
                Email = model.Email,
                Adress = model.Adress,
                BookId = model.BookId,
                RecordType = model.RecordType,
                Date = model.Date
            };

            var result = await _saleService.CreateAsync(sale);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla oluşturuldu.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("sales");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSale(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _saleService.DeleteAsync((int)id);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla silindi.",
                AlertType = "warning"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("sales");
        }

        public async Task<IActionResult> Writers(string search)
        {
            var writers = await _writerService.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                writers = await _writerService.GetAllBySearchAsync(search);
            }

            var model = new WriterModel()
            {
                Writers = writers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Writers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _writerService.GetByIdAsync((int)id);

            if (writer == null)
            {
                return NotFound();
            }

            var result = await _writerService.DeleteAsync(writer.Id);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("writers");
        }

        [HttpPost]
        public async Task<IActionResult> AddWriter(WriterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["nameError"] = "Yazar adı boş geçilemez.";
                return RedirectToAction("writers");
            }

            if (model == null)
            {
                return NotFound();
            }

            var writer = new Writer()
            {
                Name = model.WriterName
            };

            var result = await _writerService.CreateAsync(writer);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla eklendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("writers");
        }

        public async Task<IActionResult> Categories(string search)
        {
            var categories = await _categoryService.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                categories = await _categoryService.GetAllBySearchAsync(search);
            }

            var model = new CategoryModel()
            {
                Categories = categories
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Categories(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category == null)
            {
                return NotFound();
            }

            var result = await _categoryService.DeleteAsync(category.Id);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla silindi.",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("categories");
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["nameError"] = "Kategori adı boş geçilemez.";
                return RedirectToAction("categories");
            }

            if (model == null)
            {
                return NotFound();
            }

            var category = new Category()
            {
                Name = model.CategoryName
            };

            var result = await _categoryService.CreateAsync(category);

            if (result == false)
            {
                return NotFound();
            }

            var msg = new AlertMessage()
            {
                Message = "Kayıt başarıyla eklendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("categories");
        }
    }
}
