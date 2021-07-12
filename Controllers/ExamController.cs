using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace exam.Controllers
{  public class ExamController : Controller
    {
        private MyContext _db;
        private int? uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }
        private bool isLoggedIn
        {
            get { return uid != null; }
        }

        public ExamController(MyContext context)
        {
            _db = context;
        }
        [HttpGet("hobby")]
        public IActionResult Dashboard()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Hobby> allHobbeis = _db
                .Hobbies   
                .Include(m => m.Amateur) 
                .Include(m => m.Hlikes)
                .OrderByDescending(c => c.Hlikes.Count)
                .ToList();
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(allHobbeis);
        }
        [HttpGet("hobby/new")]
        public IActionResult New()  
        {
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View();
        }
        [HttpPost("PostHobby")]
        public IActionResult PostHobby(Hobby hobby)
        {
              if(_db.Hobbies.Any(h=>h.Name == hobby.Name))
                {
                    ModelState.AddModelError("Name", "This hobby Name is already exist!");
                }
            if(ModelState.IsValid)
            {
                hobby.UserId = (int)uid;
                _db.Hobbies.Add(hobby);
                _db.SaveChanges();
                return Redirect($"/hobby/{hobby.HobbyId}");
            }
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View("New");
        }

        [HttpGet("hobby/{hobbyId}")]
        public IActionResult Hobbies(int hobbyId)
        {
            Hobby thisHobby = _db
            .Hobbies  
            .Include(m => m.Amateur) 
            .Include(m => m.Hlikes) 
            .ThenInclude(f => f.Hlike) 
            .FirstOrDefault(m => m.HobbyId == hobbyId);
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(thisHobby);
        }

        [HttpGet("like/{hobbyId}")]
        public IActionResult Join(int hobbyId)
        {
            Like like = new Like();
            like.UserId = (int)uid;
            like.HobbyId = hobbyId;
            _db.Likes.Add(like);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    
        [HttpGet("hobby/edit/{hobbyId}")]
        public IActionResult Edit(int hobbyId)
        {
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            Hobby hobby = _db.Hobbies.FirstOrDefault(m => m.HobbyId == hobbyId);
            return View(hobby);
        }
        [HttpPost("hobby/update/{hobbyId}")]
        public IActionResult Update(Hobby hobby, int hobbyId)
        {

            if(_db.Hobbies.Any(h=>h.Name == hobby.Name))
                {
                    ModelState.AddModelError("Name", "This hobby Name is already exist!");
                }
            if(ModelState.IsValid)
            {
            
                Hobby hobbyFromDB = _db.Hobbies.FirstOrDefault(m => m.HobbyId == hobbyId);
                hobbyFromDB.Name = hobby.Name;
                hobbyFromDB.Description = hobby.Description;
                _db.SaveChanges();
                Console.WriteLine("successfully updated");
                return Redirect($"/hobby/{hobby.HobbyId}");
                // return Redirect("Dashboard");
        
            }
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            Console.WriteLine("There were some errors, should see errors");
            return View("Edit", hobby);
        }
    }
}