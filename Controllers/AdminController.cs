using ByTheBooksWeb.Data;
using CrudOperations.Encryption;
using CrudOperations.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db= db;
        }

        public IActionResult UserDetails()
        {
            IEnumerable<UserDetails> userDetails = _db.UserDetails.ToList();
            return View(userDetails);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDetails userDetails)
        {
            PasswordEncryption passwordEncryption = new PasswordEncryption();

            string encryptedpassword = passwordEncryption.Encrypt(userDetails.EncryptedPassword, out string saltCode);

            userDetails.SaltPassword = saltCode;
            userDetails.EncryptedPassword = encryptedpassword;


            // Value enter are valid.
            if (ModelState.IsValid == true)
            {
               

                _db.UserDetails.Add(userDetails);
                _db.SaveChanges();
                TempData["Success"] = "Created Successfully";
                return RedirectToAction("UserDetails");
            }
            return View();
        }


        // Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var data = _db.UserDetails.Find(id);

            return View(data);

        }


        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDetails userDetails)
        {
            // Name and Display Order is equal.
            if (userDetails.Name == userDetails.EncryptedPassword)
            {
                ModelState.AddModelError("Password", "The Password cannot exactly match the Name.");
            }

            PasswordEncryption passwordEncryption = new PasswordEncryption();

            string encryptedpassword = passwordEncryption.Encrypt(userDetails.EncryptedPassword, out string saltCode);

            userDetails.SaltPassword = saltCode;
            userDetails.EncryptedPassword = encryptedpassword;



            // Value enter are valid.
            if (ModelState.IsValid == true)
            {
                _db.UserDetails.Update(userDetails);
                _db.SaveChanges();
                TempData["Success"] = "Edited Successfully";
                return RedirectToAction("UserDetails");
            }

            return View();
        }


        // Get
        public IActionResult Delete(int? id)
        {
            UserDetails? userDetails = _db.UserDetails.Find(id);

            if (id.HasValue == false)
            {
                return NotFound();
            }

            // Value enter are valid.

            _db.UserDetails.Remove(userDetails);
            _db.SaveChanges();
            TempData["Success"] = "Deleted Successfully";
            return RedirectToAction("UserDetails");

        }


        
       




    }
}
