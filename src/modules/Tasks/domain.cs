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
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }

    public interface TaskRepository
    {
        void Add(TodoTask task);
        void Delete(string id);

        void Update(string id, UpdateTaskPayload payload);

        TodoTask? Search(string id);
        List<TodoTask> All();
        List<TodoTask> AllFromUser(string userId);
    }
}