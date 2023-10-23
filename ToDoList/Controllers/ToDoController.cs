using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers;

public class ToDoController : Controller
{
    private readonly AppDbContext _db;

    public ToDoController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<ToDo> objTodo = _db.ToDos;

        return View(objTodo);
    }

    //get
    public IActionResult Create()
    {
        return View();
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ToDo obj)
    {
        if (ModelState.IsValid)
        {
            _db.ToDos.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "todo created successfully";
            return Redirect("Index");
        }

        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var toDoFromDb = _db.ToDos.Find(id);
        
        if (toDoFromDb == null)
        {
            return NotFound();
        }
        return View(toDoFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ToDo obj)
    {
        if (ModelState.IsValid)
        {
            _db.ToDos.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    
    //get
    public IActionResult Delete(int? id)
    {
        if (id==null || id==0)
        {
            return NotFound();
        }

        var toDoFromDb = _db.ToDos.Find(id);
        if (toDoFromDb == null)
        {
            return NotFound();
        }

        return View(toDoFromDb);
    }

    //post
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.ToDos.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.ToDos.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "todo deleted successfully";
        return RedirectToAction("Index");

        
    }
}