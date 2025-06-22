using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class TodoController : Controller
{
    // In-memory list to simulate a database
    private static List<TodoItem> _todoItems = new List<TodoItem>();
    private static int _nextId = 1;

    public IActionResult Index()
    {
        return View(_todoItems);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TodoItem item)
    {
        if (ModelState.IsValid)
        {
            item.Id = _nextId++;
            _todoItems.Add(item);
            return RedirectToAction("Index");
        }
        return View(item);
    }

    public IActionResult Toggle(int id)
    {
        var item = _todoItems.FirstOrDefault(t => t.Id == id);
        if (item != null)
        {
            item.IsCompleted = !item.IsCompleted;
        }
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var item = _todoItems.FirstOrDefault(t => t.Id == id);
        if (item != null)
        {
            _todoItems.Remove(item);
        }
        return RedirectToAction("Index");
    }
}
