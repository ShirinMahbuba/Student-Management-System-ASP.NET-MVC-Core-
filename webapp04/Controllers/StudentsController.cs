using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp04.Data;
using webapp04.Models;

namespace webapp04.Controllers
{
    public class StudentsController : Controller
    {
        private readonly webapp04Context _context;
        private readonly IWebHostEnvironment _env;

        public StudentsController(webapp04Context context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;


        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentRoll,Name,Email,Phone,EnrollmentDate,PhotoPath")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentRoll,Name,Email,Phone,EnrollmentDate,PhotoPath")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);

        }

        public async Task<IActionResult> Upload(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile filetoupload)
        {
            if (filetoupload == null)
            {
                ViewBag.msg = "file ould not be null";
                return RedirectToAction("Upload",new { id = id });
            }
            if(filetoupload.Length  == 0)
            {
                ViewBag.msg = "File has no ontain.Try again...";
                return RedirectToAction("Upload", new {id=id });
            }
            if(filetoupload.Length>2048000)
            {
                 ViewBag.msg = "File size is too large.Max size is 2MB.Try again...";
                return RedirectToAction("Upload", new { id = id });
            }

            string ext = Path.GetExtension (filetoupload.FileName);

            if(ext !=".jpg" && ext !=".png" && ext != ".pdf" && ext !=".bmp" )
            {
                ViewBag.msg ="["+ext+ "] Only .jpg,.png,.pdf and .bmp files are allowed.Try again...";
                return RedirectToAction("Upload", new { id = id });
            }

            var std = _context .Student.FirstOrDefault(x=> x.Id == id);
            if(std == null)
            {
               ViewBag.msg ="Student not found.Try again...";
                return RedirectToAction("Upload", new { id = id });
            }
            int stdid = std.Id;
            string photo_id= stdid.ToString().PadLeft(8,'X');
            string webroot= _env.WebRootPath;
            string app_upload_folder = "StudentPhoto";
            photo_id = "Pic" + photo_id + ext;
            string filepath =Path.Combine (webroot,app_upload_folder,photo_id );
            using (var stream = new FileStream (filepath,FileMode.Create))
            {
                await filetoupload.CopyToAsync (stream);

                ViewBag.msg="Student photo uploaded successfully.";

                std.PhotoPath = photo_id;
                _context.Student.Update (std);
                await _context.SaveChangesAsync ();
                return RedirectToAction("Upload", new { id = id });
            }


        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
