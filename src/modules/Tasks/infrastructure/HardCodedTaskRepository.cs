using Tasks.Domain;

namespace Tasks
{
    internal class HardCodedTaskRepository : TaskRepository
    {
        private List<TodoTask> tasks;

        public HardCodedTaskRepository()
        {
            tasks = new List<TodoTask>();
        }

        public List<TodoTask> All()
        {
            return tasks;
        }

        public List<TodoTask> AllFromUser(string userId)
        {
            return tasks.Where(task => task.UserId == userId).ToList();
        }

        public void Add(TodoTask task)
        {
            tasks.Add(task);
        }

        public void Update(string id, UpdateTaskPayload payload)
        {
            var task = Search(id);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            task.Title = payload.Title;
            task.Description = payload.Description;
            task.IsCompleted = payload.IsCompleted;
        }

        public void Delete(string id)
        {
            var taskToRemove = Search(id);
            if (taskToRemove == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            tasks.Remove(taskToRemove);
        }

        public TodoTask? Search(string id)
        {
            return tasks.Find(t => t.Id == id);
        }
    }
}