using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MyBestBooks.DataAccess.Repository.IRepository;
using MyBestBooks.Models;
using MyBestBooks.Models.ViewModels;
using MyBestBooks.Utility;


namespace MyBestBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)] // that line to allow only the Admin to modify our content
									   // (Not someone who have the path when we do copy paste to the Url).
									   // we can give this authorization to every action for category instead of this line
	public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; // for saving the images
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment; // now we will be able to access to the folder wwwroot/images
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id) // Up(date+In)sert
        {
            // we need to pass the list of all the categories to put it in a dropdown for the product property...
            // we can't pass it in the objProductList because it return an only one object...
            // the only way to return a list of product.

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            // that's to retrieve (récupérer) an IEnumerable of category but we need to convert it to an IEnumerable SelectListItem
            // (we do that with the Projections in EF Core) to use it we did the function of Select
            {
                Text = u.Name,
                Value = u.Id.ToString() // each category object here will be converted to a SelectListItem and it will have a Text and a Value
            });
            // with the projections we can have only some columns and not all the columns...
            // with that we have the category List right here but how we pass that in our view ???
            // The VIEWBAG: it transfers data from the controller to View, not vice-versa.
            // Ideal for situations in which the temporary data is not in a model... and it's a dinamic property
            // The VIEWDATA: can do the same thing

            /*ViewBag.CategoryList = CategoryList;*/ // first CategoryList is a key Value (we can name it what we want).
                                                     // once we get that we can put it in a dropdown in the create view

            ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
            // inside the IFormFile we will recieve the file that was uploaded
            // so we have to capture that file and save that into the images/product
            // but how to access the root of wwwroot folder ?
            // to do that we have to inject something called IWebHostEnvironment and that's provided by default with the .net project.
            // so basecally we have to inject it like we have injected the IUnitOfWork but we don't have to register that
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // here we get the root path
                // we will check if the file is not null
                if (file != null) // if it's not null we want to upload the file and save that in the product folder
                { 
                    string fileName = Guid.NewGuid().ToString() // we rename the file to be a random Guid (random name for our file)
                                    + Path.GetExtension(file.FileName); // to preserve the extension there to this file
                    // so the image that will be saving will have a Guid and it will have the same extension that cart uploaded
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
					// we will combine the wwwRootPath with another path wich is the images/product

					// the coming condition is to replace the image with a new one when we are updating a product
					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) // to be sure that it's not null (update not create)
                    {
                        // delete the old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\')); // the forward slash is to remove the one who exists in the database before the url of the image
                                                                                                   // (we can check that forward slash in the path of the image in the databse.
                                                                                                   // that will basically combine and give us the path of the old image
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); // delete the old image
                        }
                    }

                    // after deleting now we will uplaod the new image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream); // that will copy the file to the new location (befor 2 lines)
                    }

                    // and next we will update the image
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                // how to identify that we are going to add or to update ? so we need a condition if the id exists or not
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product); // here we add our product
                }
                else 
                {
					_unitOfWork.Product.Update(productVM.Product); // here we update our product
				}
                _unitOfWork.Save(); // we save it
                TempData["success"] = "Product created successfully - تمت عملية الإضافة بنجاح ";
                return RedirectToAction("Index");
            }
            return View();

        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Models.Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Product? ProductFromDb1 = _db.Products.FirstOrDefault(u=>u.Id==id);
        //    //Product? ProductFromDb2 = _db.Products.Where(u=>u.Id==id).FirstOrDefault();
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Models.Product obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product updated successfully - تمت عملية التحيين بنجاح ";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Models.Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Models.Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully - تمت عملية الحذف بنجاح ";
        //    return RedirectToAction("Index");

        //}

        // MVC have his own API so we need just to call it... to can use the ajax request
        // all the coming is to can inject the search bar and many other functionalities
        #region API CALLS 
        // we can test that with running the application and go to getall(https://localhost:7001/Admin/Product/getall)
        // we will got all our products quickly (the power of API in MVC)
        // now we can use the ajax api from "datatables" (from the internet) without any problem
        // we will put that .js into the wwwRoot.js so let's go there...
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
        }
        [HttpDelete] // with that the code in prodect.js can use this IActionresult in controller (it's necessary to share this function)
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u=>u.Id == id);
               if (productToBeDeleted == null)
            {
               return Json(new { success = false, message = "error while deleting" });
            }
            var oldImagePath =
                               Path.Combine(_webHostEnvironment.WebRootPath, 
                               productToBeDeleted.ImageUrl.TrimStart('\\')); 

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Product deleted successfully - تمت عملية الحذف بنجاح" });
        }
        #endregion
        // now we can remove the delete view product in area admin 
    }
}
// TempData["success"] = "Product deleted successfully - تمت عملية الحذف بنجاح ";
// return RedirectToAction("Index");