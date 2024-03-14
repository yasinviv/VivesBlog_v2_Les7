using Microsoft.AspNetCore.Mvc;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticlesController(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var articles = _dbContext.Articles.ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
            article.PublishedDate = DateTime.Now;

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] Article article)
        {
            var dbArticle = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (dbArticle is null)
            {
                return RedirectToAction("Index");
            }

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = _dbContext.Articles
                .FirstOrDefault(p => p.Id == id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
