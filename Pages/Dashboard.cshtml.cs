using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagerUI.Services;

namespace TaskManagerUI.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly TaskService _taskService;

        public DashboardModel(TaskService taskService)
        {
            _taskService = taskService;
            TaskStatusCounts = string.Empty;
            PriorityCounts = string.Empty;
        }

        public string TaskStatusCounts { get; set; }
        public string PriorityCounts { get; set; }

        public void OnGet()
        {
            var tasks = _taskService.GetTasks();

            TaskStatusCounts = Newtonsoft.Json.JsonConvert.SerializeObject(new int[]
            {
                tasks.Count(t => t.Status == "Pending"),
                tasks.Count(t => t.Status == "In Progress"),
                tasks.Count(t => t.Status == "Completed")
            });

            PriorityCounts = Newtonsoft.Json.JsonConvert.SerializeObject(new int[]
            {
                tasks.Count(t => t.Priority == 1), // High
                tasks.Count(t => t.Priority == 2), // Medium
                tasks.Count(t => t.Priority == 3)  // Low
            });
        }
    }
}
