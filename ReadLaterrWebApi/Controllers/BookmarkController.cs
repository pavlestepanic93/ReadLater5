using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadLaterrWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        IBookmarkService _bookmarkService;
        ICategoryService _categoryService;
        public BookmarkController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }
        // GET: Bookmark
        [Authorize]
        public IActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks();
            foreach (var i in model)
            {
                i.Category = _categoryService.GetCategory(i.CategoryId);
            }
            return Ok(model);
        }

        // GET: Bookmark/Details/5
        [Authorize]
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
            return Ok(bookmark);

        }

        // GET: Bookmark/Create
        [Authorize]
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Bookmark/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return Ok(bookmark);
        }

        // GET: Bookmark/Edit/5
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
            return Ok(bookmark);
        }

        // POST: Bookmark/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return Ok(bookmark);
        }

        // GET: Bookmark/Delete/5
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
            return Ok(bookmark);
        }

        // POST: Bookmark/Delete/5
        [HttpPost, ActionName("Delete")]
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
