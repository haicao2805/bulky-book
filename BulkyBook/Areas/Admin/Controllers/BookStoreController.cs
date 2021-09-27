using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookStoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookStoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            BookStore bookStore = new BookStore();
            if (id == null)
            {
                return View(bookStore);
            }

            bookStore = _unitOfWork.BookStore.Get(id.GetValueOrDefault());
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookStore bookStore)
        {
            if (ModelState.IsValid)
            {
                if (bookStore.Id == 0)
                {
                    _unitOfWork.BookStore.Add(bookStore);
                }
                else
                {
                    _unitOfWork.BookStore.Update(bookStore);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(bookStore);
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allBookStores = _unitOfWork.BookStore.GetAll();
            return Json(new
            {
                data = allBookStores
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.BookStore.Get(id);
            if (obj == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            _unitOfWork.BookStore.Remove(obj);
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
