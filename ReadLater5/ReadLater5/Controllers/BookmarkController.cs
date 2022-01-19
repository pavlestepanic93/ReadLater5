using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReadLater5.Helper;

namespace ReadLater5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SessionTokenFilter]
    public class BookmarkController : Controller
    {
        IBookmarkService _bookmarkService;
        ICategoryService _categoryService;
        public BookmarkController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }

        [HttpGet("/Bookmark")]
        public IActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks();
            foreach(var i in model)
            {
                i.Category = _categoryService.GetCategory(i.CategoryId);
            }
            return View(model);
        }

        [HttpGet("/Bookmark/Details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);

        }

        [HttpGet("/Bookmark/Create")]
        public IActionResult Create()
        {
            ViewBag.CategoriesSelectedList = new SelectList(_categoryService.GetCategories(), "ID", "Name");
            return View();
        }

        [HttpPost("/Bookmark/Create")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        [HttpGet("/Bookmark/Edit/{id}")]
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            ViewBag.CategoriesSelectedList = new SelectList(_categoryService.GetCategories(), "ID", "Name");
            return View(bookmark);
        }

        [HttpPost("/Bookmark/Edit/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmark);
        }

        [HttpGet("/Bookmark/Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        [HttpPost("/Bookmark/Delete/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
