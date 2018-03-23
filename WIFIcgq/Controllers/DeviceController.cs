using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIFIcgq.Extends;
using WIFIcgq.Models;

namespace WIFIcgq.Controllers
{
    public class DeviceController : UserBase
    {
        private readonly WIFIcgqContext _context;
        public DeviceController(WIFIcgqContext context)
        {
            _context = context;
        }

        public ActionResult DeviceManage()
        {
            try
            {
                var username =HttpContext.Session.GetString("UserName");
                var modelInfo = _context.UserInfo.Where(x => x.UserName.Equals(username)).Select(y => y.DeviceModel).SingleOrDefault();
                var model = Regex.Split(modelInfo, "(?<=\\G.{1})");
                int isAdded = 0;
                for (int i = 0; i < 3; i++)
                {
                    ViewData[i.ToString()] = 0;
                    if (isAdded >= modelInfo.Count())
                        continue;
                    if (Convert.ToInt32(model[isAdded]) - 1 == i)
                    {
                        ViewData[i.ToString()] = 1;
                        isAdded++;
                    }
                }
            }
            catch (Exception)
            {

            }
            return View();
        }
        public ActionResult AddDevice()
        {
            return View();
        }

        public ActionResult ModelManage()
        {
            try
            {
                var username = HttpContext.Session.GetString("UserName");
                var modelInfo = _context.UserInfo.Where(x => x.UserName.Equals(username)).Select(y => y.DeviceModel).SingleOrDefault();
                var model = Regex.Split(modelInfo, "(?<=\\G.{1})");
                int isAdded = 0;
                for (int i = 0; i < 3; i++)
                {
                    ViewData[i.ToString()] = 0;
                    if (isAdded >= modelInfo.Count())
                        continue;
                    if (Convert.ToInt32(model[isAdded]) - 1 == i)
                    {
                        ViewData[i.ToString()] = 1;
                        isAdded++;
                    }
                }
            }
            catch (Exception)
            {

            }
            return View();
        }
        public ActionResult SaveChanges(IFormCollection form)
        {
            var a = form["mark0"];
            var b = form["mark1"];
            var c = form["mark2"];
            string temp="";
            var username = HttpContext.Session.GetString("UserName");
            var model = _context.UserInfo.Where(x => x.UserName.Equals(username)).First();
            if (a == "1")
                temp += "1";
            if (b == "1")
                temp += "2";
            if (c == "1")
                temp += "3";
            model.DeviceModel = temp;
            _context.SaveChanges();

            return RedirectToAction("../Device/DeviceManage");
        }


        public JsonResult DelModel(bool delad, bool delda, bool delio, string mark0, string mark1, string mark2)
        {
            string[] result = new string[3];
            if (delad == true)
                result[0] = 0.ToString();
            else
                result[0] = mark0;

            if (delda == true)
                result[1] = 0.ToString();
            else
                result[1] = mark1;

            if (delio == true)
                result[2] = 0.ToString();
            else
                result[2] = mark2;
            return Json(result);
        }
        public JsonResult AddModel(bool addad,bool addda,bool addio, string mark0, string mark1, string mark2)
        {
            string[] result = new string[3];
            if (addad == true)
                result[0] = 1.ToString();
            else
                result[0] = mark0;

            if (addda == true)
                result[1] = 1.ToString();
            else
                result[1] = mark1;

            if (addio == true)
                result[2] = 1.ToString();
            else
                result[2] = mark2;

            return Json(result);
        }

        public JsonResult ShowDevice()
        {
            var username = HttpContext.Session.GetString("UserName");
            var devicelist = _context.DeviceInfo.Where(x => x.UserName.Equals(username)).Select(y => y.DeviceId).ToList();
            return Json(devicelist);
        }
        public JsonResult CheckDevice(string deviceid)
        {
            var username = HttpContext.Session.GetString("UserName");
            int isExistance;
            DeviceInfo temp = new DeviceInfo();
            string Warning;
            if (deviceid == null)
            {
                return Json("设备号不能为空");
            }
            if (_context.DeviceInfo.Where(x => x.UserName.Equals(username)).Count() != 0)
            {
                isExistance = _context.DeviceInfo.Where(x => x.UserName.Equals(username)).Where(y => y.DeviceId.Equals(deviceid)).Count();
                if (isExistance != 0)
                {
                    Warning = "设备已注册";
                    return Json(Warning);
                }
            }
            temp.Id = _context.DeviceInfo.Max(x => x.Id) + 1;
            temp.UserName = username;
            temp.DeviceId = deviceid;
            _context.Add(temp);
            _context.SaveChanges();
            return Json("设备添加成功");
        }

        public ActionResult DelDevice(string id)
        {
            var username = HttpContext.Session.GetString("UserName");
            var device = _context.DeviceInfo.Where(x => x.UserName.Equals(username)).Where(y => y.DeviceId.Equals(id)).Single();
            _context.Remove(device);
            _context.SaveChanges();
            return RedirectToAction("AddDevice");
        }
    }
}
