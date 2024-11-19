using System.Collections.Generic;
using System.Linq;
using TaskManagerUI.Data;
using TaskManagerUI.Models;

namespace TaskManagerUI.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> GetTasks(string? filter = null, string? sort = null)
        {
            var query = _context.Tasks.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.Status == filter);
            }

            // Apply sorting
            query = sort switch
            {
                "priority" => query.OrderBy(t => t.Priority),
                "deadline" => query.OrderBy(t => t.Deadline),
                _ => query.OrderBy(t => t.CreatedAt) // Default: sort by creation date
            };

            return query.ToList();
        }

        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void UpdateTask(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
