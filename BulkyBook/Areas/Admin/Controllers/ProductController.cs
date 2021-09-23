using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(coverType => new SelectListItem
                {
                    Text = coverType.Name,
                    Value = coverType.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(productViewModel);
            }

            productViewModel.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productViewModel.Product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (product.Id == 0)
        //        {
        //            _unitOfWork.Product.Add(product);
        //        }
        //        else
        //        {
        //            _unitOfWork.Product.Update(product);
        //        }
        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(product);
        //}

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allProducts = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new
            {
                data = allProducts
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Product.Get(id);
            if (obj == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Delete successful"
            });
        }
        #endregion
    }
}
