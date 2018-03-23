using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WIFIcgq.Extends;
using WIFIcgq.Models;

namespace WIFIcgq.Controllers
{
    public class AdminController :AdminBase
    {
        private readonly WIFIcgqContext _context;

        public AdminController(WIFIcgqContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserInfo.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfo
                .SingleOrDefaultAsync(m => m.UserName.Equals(id));
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,DeviceModel,Address,Tel,Coporation,Department,Sex")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                if (_context.UserInfo.Where(x => x.UserName.Equals(userInfo.UserName)).Count() != 0)
                {
                    ViewBag.warning = "该用户已存在";
                    return View();
                }
                else{
                    _context.Add(userInfo);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfo.SingleOrDefaultAsync(m => m.UserName.Equals(id));
            if (userInfo == null)
            {
                return NotFound();
            }
            return View(userInfo);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,PassWord,DeviceModel,Address,Tel,Coporation,Department,Sex")] UserInfo userInfo)
        {
            if (id != userInfo.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoExists(userInfo.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfo
                .SingleOrDefaultAsync(m => m.UserName.Equals(id));
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userInfo = await _context.UserInfo.SingleOrDefaultAsync(m => m.UserName.Equals(id));
            _context.UserInfo.Remove(userInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfoExists(string id)
        {
            return _context.UserInfo.Any(e => e.UserName.Equals(id));
        }
    }
}
