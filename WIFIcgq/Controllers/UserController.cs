using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIFIcgq.Models;
using WIFIcgq.Extends;
using System.Text.RegularExpressions;

namespace WIFIcgq.Controllers
{
    public class UserController : Controller
    {
        private readonly WIFIcgqContext _context;
        int OCountAD, OCountIO;
        public UserController(WIFIcgqContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            OCountAD = _context.SendDataAd.Count();
            OCountIO = _context.SendDataIo.Count();
            HttpContext.Session.SetInt32("OCountAD", OCountAD);
            HttpContext.Session.SetInt32("OCountIO", OCountIO);
            ViewBag.OCountAD = OCountAD;
            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            string vericode = form["VeriCode"];
            var type = form["type"];
            string warning;
            if (vericode.ToLower() != HttpContext.Session.GetString("LoginValidateCode").ToLower())
            {
                warning = "验证码错误";
                ViewBag.warning = warning;
                return View();
            }
            else
            {
                if (type == "admin")
                {
                    if (_context.AdminInfo.Where(x => x.UserName.Equals(username)).Count() == 0)
                    {
                        warning = "该用户不存在";
                        ViewBag.warning = warning;
                        return View();
                    }
                    else if (_context.AdminInfo.Where(x => x.UserName.Equals(username)).Select(y => y.PassWord).SingleOrDefault() != password)
                    {
                        warning = "密码错误";
                        ViewBag.warning = warning;
                        return View();
                    }
                    else
                    {
                        HttpContext.Session.SetString("sUserName", username);
                        return RedirectToAction("../Admin/Index");
                    }
                }
                else
                {
                    if (_context.UserInfo.Where(x => x.UserName.Equals(username)).Count() == 0)
                    {
                        warning = "该用户不存在";
                        ViewBag.warning = warning;
                        return View();
                    }
                    else if (_context.UserInfo.Where(x => x.UserName.Equals(username)).Select(y => y.PassWord).SingleOrDefault() != password)
                    {
                        warning = "密码错误";
                        ViewBag.warning = warning;
                        return View();
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserName", username);
                        return RedirectToAction("../Device/DeviceManage");
                    }
                }
            }


        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Back()
        {
            return RedirectToAction("../Device/DeviceManage");
        }
        public ActionResult BackToIndex()
        {
            return Redirect("Index");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("sUserName");
            return RedirectToAction("Index");
        }

        public ActionResult AddUser(IFormCollection form)
        {
            var username = form["username"];
            var password = form["password"];
            var ta = _context.UserInfo.Where(m => m.UserName.Equals(username)).Select(m => m.Id);
            if (ta.Count() != 0)
            {
                ViewData["warning"] = "该用户已被注册";
                return View("Register");
            }
            else
            {
                var _user = new UserInfo
                {
                    UserName = username,
                    PassWord = password,
                };
                _context.Add(_user);
                _context.SaveChanges();
                return RedirectToAction("RegisterResult");
            }
        }
        public ActionResult RegisterResult()
        {
            return View();
        }
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(IFormCollection form)
        {
            var username = HttpContext.Session.GetString("UserName");
            var susername = HttpContext.Session.GetString("sUserName");
            var opassword = form["opassword"];
            var npassword = form["npassword"];
            if (susername == null)
            {
                if (_context.UserInfo.Where(x => x.UserName.Equals(username)).Select(y => y.PassWord).SingleOrDefault() == opassword)
                {
                    _context.UserInfo.Where(x => x.UserName.Equals(username)).SingleOrDefault().PassWord = npassword;
                    _context.SaveChanges();
                    HttpContext.Session.Remove("UserName");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Warning"] = "密码错误";
                }
            }
            else
            {
                if (_context.AdminInfo.Where(x => x.UserName.Equals(susername)).Select(y => y.PassWord).SingleOrDefault() == opassword)
                {
                    _context.AdminInfo.Where(x => x.UserName.Equals(susername)).SingleOrDefault().PassWord = npassword;
                    _context.SaveChanges();
                    HttpContext.Session.Remove("sUserName");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Warning"] = "密码错误";
                }
            }
            return View();
        }

        public JsonResult Check(string username, string password)
        {
            var isExistance = _context.UserInfo.Where(x => x.UserName.Equals(username)).Count();
            if (isExistance != 0)
            {
                string Warning = "该用户已被注册";
                return Json(Warning);
            }
            else
                return null;
        }

        public JsonResult Send(string data, string deviceid)
        {
            ReceiveData temp = new ReceiveData();
            var username = HttpContext.Session.GetString("UserName");
            var oridata = deviceid + "00000000" + data + "04FFFF0001";
            var t = Regex.Split(oridata, "(?<=\\G.{2})");
            int j = 0;
            byte[] b = new byte[t.Length - 1];
            try
            {
                foreach (var item in t)
                {
                    b[j] = Convert.ToByte(item, 16);
                    j++;
                    if (j == t.Length - 1)
                        break;
                }
                var check = CRC.ToCRC16(b, false);
                data = "fe" + deviceid + "00000000" + data + "04FFFF0001" + check + "fe";
                temp.Data = data;
                temp.Time = DateTime.Now;
                temp.Id = _context.ReceiveData.Max(x => x.Id) + 1;
                _context.Add(temp);
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }

            return null;
        }
        public JsonResult SendDA(string da1, string da2, string da3, string deviceid)
        {
            ReceiveData temp = new ReceiveData();
            string data;
            var username = HttpContext.Session.GetString("UserName");
            var oridata = deviceid + "000000000f04" + da1 + da2 + da3 + "01";
            var t = Regex.Split(oridata, "(?<=\\G.{2})");
            int j = 0;
            byte[] b = new byte[t.Length - 1];
            try
            {
                foreach (var item in t)
                {
                    b[j] = Convert.ToByte(item, 16);
                    j++;
                    if (j == t.Length - 1)
                        break;
                }
                var check = CRC.ToCRC16(b, false);
                data = "fe" + deviceid + "000000000f04" + da1 + da2 + da3 + "01" + check + "fe";
                temp.Data = data;
                temp.Time = DateTime.Now;
                temp.Id = _context.ReceiveData.Max(x => x.Id) + 1;
                _context.Add(temp);
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public JsonResult SendIO(string port1, string port2, string deviceid)
        {
            ReceiveData temp = new ReceiveData();
            string data;
            var username = HttpContext.Session.GetString("UserName");
            var oridata = deviceid + "000000003004" + port1 + port2 + "0001";
            var t = Regex.Split(oridata, "(?<=\\G.{2})");
            int j = 0;
            byte[] b = new byte[t.Length - 1];
            try
            {
                foreach (var item in t)
                {
                    b[j] = Convert.ToByte(item, 16);
                    j++;
                    if (j == t.Length - 1)
                        break;
                }
                var check = CRC.ToCRC16(b, false);
                data = "fe" + deviceid + "000000003004" + port1 + port2 + "0001" + check + "fe";
                temp.Data = data;
                temp.Time = DateTime.Now;
                temp.Id = _context.ReceiveData.Max(x => x.Id) + 1;
                _context.Add(temp);
                _context.SaveChanges();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public JsonResult LogNav()
        {
            var temp = HttpContext.Session.GetString("sUserName");
            if (temp == null)
                temp = HttpContext.Session.GetString("UserName");
            var result = temp;
            return Json(result);
        }

        public ActionResult Test()
        {
            return View();
        }
        public IActionResult ValidateCode(VierificationCodeServices _vierificationCodeServices)
        {
            string code = "";
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}
