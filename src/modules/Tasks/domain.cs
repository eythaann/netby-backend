using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain
{
    public class TodoTask
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsCompleted { get; set; }

        public TodoTask(string userId)
        {
            Id = Guid.NewGuid().ToString("N");
            UserId = userId;
            Title = "New Task";
            Description = "Made a cake.";
            CreationDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddDays(7);
            IsCompleted = false;
        }
    }

    public class UpdateTaskPayload
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }

    public interface TaskRepository
    {
        void add(TodoTask task);
        void delete(string id);
        TodoTask? search(string id);
        List<TodoTask> all();
        List<TodoTask> allFromUser(string userId);
    }
}