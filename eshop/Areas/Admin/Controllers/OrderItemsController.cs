using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class OrderItemsController : Controller
    {
        private readonly EshopDBContext _context;

        public OrderItemsController(EshopDBContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderItems
        public async Task<IActionResult> Index()
        {
            var eshopDBContext = _context.OrderItem.Include(o => o.Order).Include(o => o.Product);
            return View(await eshopDBContext.ToListAsync());

           /* var orderItem = _context.OrderItem.Where(oi => oi.ID == 2).FirstOrDefault();

            _context.Entry(orderItem)
                .Reference(oi => oi.Order)
                .Load();

            _context.Entry(orderItem)
                .Reference(oi => oi.Product)
                .Load();

            return View(await _context.OrderItem.Include(o => o.Order).Include(o => o.Product).ToListAsync()); */
        }

        // GET: Admin/OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItem
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // GET: Admin/OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "OrderNumber");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt");
            return View();
        }

        // POST: Admin/OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,ProductID,Amount,Price,ID,DateTimeCreated")] OrderItems orderItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "OrderNumber", orderItems.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItems.ProductID);
            return View(orderItems);
        }

        // GET: Admin/OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItem.FindAsync(id);
            if (orderItems == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "OrderNumber", orderItems.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItems.ProductID);
            return View(orderItems);
        }

        // POST: Admin/OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,ProductID,Amount,Price,ID,DateTimeCreated")] OrderItems orderItems)
        {
            if (id != orderItems.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemsExists(orderItems.ID))
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
            ViewData["OrderID"] = new SelectList(_context.Order, "ID", "OrderNumber", orderItems.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ImageAlt", orderItems.ProductID);
            return View(orderItems);
        }

        // GET: Admin/OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItem
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // POST: Admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderItems = await _context.OrderItem.FindAsync(id);
            _context.OrderItem.Remove(orderItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemsExists(int id)
        {
            return _context.OrderItem.Any(e => e.ID == id);
        }
    }
}
