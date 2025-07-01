using Microsoft.AspNetCore.Mvc;
using ShortUrlMvc.Data;
using ShortUrlMvc.Models;
using ShortUrlMvc.Services;

namespace ShortUrlMvc.Controllers
{
    public class UrlController : Controller
    {
        //Variaveis
        private readonly AppDbContext _context;
        private readonly UrlShortService _urlShortService;

        //Construtor injeçao de dependência
        public UrlController(AppDbContext context, UrlShortService urlShortService)
        {
            _context = context;
            _urlShortService = urlShortService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve all URLs from the database
            var urls = _context.Urls.ToList();
            return View(urls);
        }
        [HttpGet]
        public IActionResult ShortenUrl()
        {
            // Return the view for shortening URLs
            return View();
        }

        [HttpPost]
        public IActionResult ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return BadRequest("URL não pode ser vazia.");
            }

            int hashCode = Math.Abs(originalUrl.GetHashCode());
            var shortCode = _urlShortService.IdToShortURL(hashCode);

            var mappedUrl = new UrlModel
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                ShortUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}",
                CreatedAt = DateTime.UtcNow
            };

            _context.Urls.Add(mappedUrl);
            _context.SaveChanges();

            // Return the shortened URL

            //return Ok(new { mappedUrl.ShortUrl });
            ViewBag.ShortUrl = mappedUrl.ShortUrl;
            return View();
        }

        [HttpGet("{shortCode}")]
        public IActionResult RedirectToOriginalUrl(string shortCode)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortCode == shortCode);
            if (url == null)
            {
                return NotFound("URL não encontrada.");
            }

            // Redirect to the original URL
            //Thread.Sleep(5000); // Optional: Add a delay before redirecting
            ViewBag.OriginalUrl = url.OriginalUrl;
            return View("Redirecting");
            //return Redirect(url.OriginalUrl);
        }
    }
}