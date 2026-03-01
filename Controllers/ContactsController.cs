using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Models;
using System.Net;
using System.Net.Mail;

namespace PortfolioMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var smtpSection = _configuration.GetSection("Smtp");

            var smtp = new SmtpClient(smtpSection["Host"])
            {
                Port = int.Parse(smtpSection["Port"]!),
                Credentials = new NetworkCredential(
                    smtpSection["Email"],
                    smtpSection["Password"]),
                EnableSsl = true
            };

            var toYou = new MailMessage(
                model.Email,
                smtpSection["Email"],
                "Новое сообщение с сайта",
                $"Имя: {model.Name}\nEmail: {model.Email}\n\n{model.Message}"
            );

            var autoReply = new MailMessage(
                smtpSection["Email"],
                model.Email,
                "Спасибо за сообщение",
                $"Здравствуйте, {model.Name}!\nСпасибо за сообщение. Я свяжусь с вами в ближайшее время."
            );

            smtp.Send(toYou);
            smtp.Send(autoReply);

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
