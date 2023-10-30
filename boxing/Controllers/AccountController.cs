using boxing.Data;
using boxing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace boxing.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbContext _dbcotex;


        public AccountController (DbContext dbcontx)
        {
            _dbcotex = dbcontx;
        }
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel u)
        {
            try
            {
                using (SqlConnection con = new(_dbcotex.Valor))
                {
                    using (SqlCommand cmd = new("sp_register", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@names", SqlDbType.VarChar).Value = u.names;
                        cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = u.lastname;
                        cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = u.birthdate;
                        cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = u.mail;
                        cmd.Parameters.Add("@users", SqlDbType.VarChar).Value = u.users;
                        cmd.Parameters.Add("@Pass", SqlDbType.VarChar).Value = u.password;
                        var token = Guid.NewGuid();
                        cmd.Parameters.Add("@token", SqlDbType.VarChar).Value = token.ToString();
                        cmd.Parameters.Add("@states", SqlDbType.Bit).Value = 0;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Email email = new();
                        if (u.mail != null)
                            email.Enviar(u.mail, token.ToString());

                        con.Close();
                    }
                }
                return RedirectToAction("token", "account");

            }
            catch (System.Exception e)
            {
                ViewData["error"] = e.Message;
                return View();
            }
            //return View();
        }

        public ActionResult Token()
        {
            string token = Request.Query["valor"];

            if (token != null)
            {
                try
                {
                    using (SqlConnection con = new(_dbcotex.Valor))
                    {
                        using (SqlCommand cmd = new("sp_validate", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@token", SqlDbType.VarChar).Value = token;

                            con.Open();
                            cmd.ExecuteNonQuery();

                            ViewData["mensaje"] = "Your account has been successfully validated!";
                            con.Close();

                        }
                    }
                    return View();
                }
                catch (System.Exception e)
                {
                    ViewData["mensaje"] = e.Message;
                    return View();
                }
            }
            else
            {
                ViewData["mensaje"] = "Check your email to activate your account..";
                return View();
            }
        }



    }
}
