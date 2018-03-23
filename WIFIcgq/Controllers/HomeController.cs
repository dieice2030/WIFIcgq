using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WIFIcgq.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Threading;

namespace WIFIcgq.Controllers
{
    public class HomeController : Controller
    {
        WIFIcgqContext _context;
        int OCountAD,OCountIO;
        //WebSocketClient socket;
        
        public HomeController(WIFIcgqContext context)
        {
            _context = context;
        }

        //public ActionResult Index()
        //{
        //    //CancellationToken t;
        //    //Uri uri = new Uri(192.168.1.107:10000);
        //    //socket.ConnectAsync(adsf@192.168.1.107,t);
        //    OCountAD = _context.SendDataAd.Count();
        //    HttpContext.Session.SetInt32("OCountAD", OCountAD);
        //    ViewBag.OCountAD = OCountAD;
        //    return View();
        //}
        public JsonResult Test()
        {
            OCountAD = (int)HttpContext.Session.GetInt32("OCountAD");
            var NCount = _context.SendDataAd.Count();
            if (NCount != OCountAD)
            {
                var result = _context.SendDataAd.Select(x => x.Data).Skip(_context.SendDataAd.Count() - 1).First().ToString();
                var temp = Regex.Split(result, "(?<=\\G.{2})");
                string[] test = new string[3];
                double voltage=0;
                test[0] = temp[13];
                test[1] = temp[14];
                test[2] = temp[15];
                for(int i = 0; i < 3; i++)
                {
                    voltage += Convert.ToInt32(test[i],16);
                }
                voltage = Math.Round( voltage / Math.Pow(2, 23) * 5, 7);
                OCountAD = NCount;
                HttpContext.Session.SetInt32("OCountAD", OCountAD);
                return Json(voltage);
            }
            else
                return Json(null);
        }
        //public JsonResult Send(string data)
        //{
        //    ReceiveData temp = new ReceiveData();
        //    temp.Id = _context.ReceiveData.Max(x => x.Id) + 1;
        //    var username = HttpContext.Session.GetString("UserName");
        //    var deviceid = _context.UserInfo.Where(x => x.UserName == username).Select(y => y.DeviceId).First();
        //    data = "fe" + deviceid + "00000000" + data + "04FFFF000171D1FE";
        //    temp.Data = data;
        //    _context.Add(temp);
        //    _context.SaveChanges();
        //    return Json(null);
        //}

        public JsonResult ShowPort()
        {
            OCountIO = (int)HttpContext.Session.GetInt32("OCountIO");
            var NCount = _context.SendDataIo.Count();
            if (NCount != OCountIO)
            {
                var result = _context.SendDataIo.Select(x => x.Data).Skip(_context.SendDataIo.Count() - 1).First().ToString();
                var temp = Regex.Split(result, "(?<=\\G.{2})");
                string[] test = new string[2];
                test[0] = temp[10];
                test[1] = temp[11];
                OCountIO = NCount;
                HttpContext.Session.SetInt32("OCountIO", OCountIO);
                return Json(test);
            }
            else
                return null;
        }
    }
}
