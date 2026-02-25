using Microsoft.AspNetCore.Mvc;
using PortfolioMVC.Models;
using System.Net;
using System.Net.Mail;

namespace PortfolioMVC.Controllers
{
    public class ContactController : Controller
    {
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

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    "elnurgasanovv07@gmail.com",
                    "pass123"),
                EnableSsl = true
            };

            var messageToYou = new MailMessage(
                model.Email,
                "elnurgasanovv07@gmail.com",
                "Новое сообщение с сайта",
                $"Имя: {model.Name}\nEmail: {model.Email}\nСообщение:\n{model.Message}"
            );

            var autoReply = new MailMessage(
                "elnurgasanovv07@gmail.com",
                model.Email,
                "Спасибо за сообщение",
                $"Здравствуйте, {model.Name}! Спасибо за сообщение. Я свяжусь с вами скоро."
            );

            smtp.Send(messageToYou);
            smtp.Send(autoReply);

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
