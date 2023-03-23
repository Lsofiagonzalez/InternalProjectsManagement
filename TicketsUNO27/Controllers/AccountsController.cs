//using AppRecordatorio.Models;
//using Core.Models.Security;
//using Core.Models.User;
//using System.Data.Entity;
//using System.Linq;
//using System.Web.Mvc;


//namespace TicketsUNO27.Controllers
//{
//    public class AccountsController : Controller
//    {
//        private Context db = new Context();
//        // GET: Accounts
//        public ActionResult Index()
//        {

//            if (SessionPersister.Id != null)
//                return RedirectToAction("home", "Home", null);

//            return View();
//        }

//        public ActionResult ResetPassword(string userName, string message)
//        {
//            if (!string.IsNullOrEmpty(message))
//            {
//                ViewBag.Error = message;
//            }

//            ViewBag.UserName = userName;
//            return View();
//        }


//        [HttpPost]
//        public ActionResult Login(Users avm)
//        {

//            Users am = new Users();
//            Accounts ResultUser = am.Login(avm.Accounts.UserName, avm.Accounts.Password);
//            var usuario = avm.Accounts.UserName;
//            var password = avm.Accounts.Password;

//            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) || ResultUser == null)
//            {
//                ViewBag.Error = "Usuario o contraseña incorrectos";
//                return View("index");
//            }

//            // Validamos que la contraseña no sea igual a la del password.
//            if (!ValidateUserAndPassword(usuario, password)) return RedirectToAction("ResetPassword", new { userName = avm.Accounts.UserName, message = "El usuario y la contraseña, no pueden se iguales... Debe cambiar la contraseña" });
//            // get user data for the session.
//            SessionPersister.UserName = avm.Accounts.UserName;
//            SessionPersister.Id = ResultUser.Id;

//            return RedirectToAction("index");

//        }

//        [HttpPost]
//        public ActionResult EditPassword(AccountsViewModel avm)
//        {
//            var validate = ValidateEditPassWord(avm);

//            // Validaciones para la edición de la contraseña.
//            if (!validate.Item1) return RedirectToAction("ResetPassword", new { userName = avm.Accounts.UserName, message = validate.Item2 });

//            var user = db.Usuario.Where(x => x.Documento.Trim() == avm.Accounts.UserName).FirstOrDefault();

//            // Validamos que el usuario a editar exista.
//            if (user == null)
//            {
//                ViewBag.Error = "El Usuario no existe, consulte con el administrador.";
//                return RedirectToAction("index");
//            }

//            user.Clave = avm.Accounts.Password;
//            db.Entry(user).State = EntityState.Modified;
//            db.SaveChanges();


//            // get user data for the session.
//            SessionPersister.UserName = avm.Accounts.UserName;
//            SessionPersister.Id = user.Id;

//            return RedirectToAction("index");

//        }

//        private (bool, string) ValidateEditPassWord(AccountsViewModel avm)
//        {
//            // Validamos que los campos de contraseña no vengan vacíos.
//            if (string.IsNullOrEmpty(avm.Accounts.Password) || string.IsNullOrEmpty(avm.Accounts.ConfirmPassword)) return (false, "Las contraseñas no pueden estar vacías...");


//            // Validamos que la contraseña no sea igual a la del password.
//            if (!ValidateUserAndPassword(avm.Accounts.UserName, avm.Accounts.Password)) return (false, "El usuario y la contraseña, no pueden ser iguales...");


//            // Validamos que las contraseñas coincidan.
//            if (!avm.Accounts.Password.Equals(avm.Accounts.ConfirmPassword)) return (false, "Las contraseñas no coinciden...");


//            return (true, string.Empty);
//        }

//        private bool ValidateUserAndPassword(string usuario, string password)
//        {
//            if (usuario.Equals(password)) return false;

//            return true;

//        }

//        public ActionResult Logout()
//        {
//            SessionPersister.UserName = string.Empty;
//            SessionPersister.Id = null;
//            return View("index");
//        }

//        public ActionResult AccessDenied()
//        {
//            return View();
//        }
//    }
//}