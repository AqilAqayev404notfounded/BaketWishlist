//using Microsoft.AspNetCore.Mvc;
//using SendMail.Models; // MailModel sınıfının olduğu namespace
//using DataAccessLayer; // SendMailService sınıfının olduğu namespace

//namespace SendMail.Controllers
//{
//    public class SendMailerController : Controller
//    {
//        private readonly SendMailService _sendMailService;

//        public SendMailerController()
//        {
//            ////_sendMailService = new SendMailService(); // DataAccessLayer sınıfı kullanımı
//        }

//        // GET: /SendMailer/
//        public ActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Index(MailModel _objModelMail)
//        {
//            if (ModelState.IsValid)
//            {
//                bool mailSent = _sendMailService.SendMail(_objModelMail.To, _objModelMail.From, _objModelMail.Subject, _objModelMail.Body);

//                if (mailSent)
//                {
//                    ViewBag.Message = "Mail başarıyla gönderildi!";
//                }
//                else
//                {
//                    ViewBag.Message = "Mail gönderimi sırasında bir hata oluştu.";
//                }

//                return View("Index", _objModelMail);
//            }
//            else
//            {
//                return View();
//            }
//        }
//    }
//}
