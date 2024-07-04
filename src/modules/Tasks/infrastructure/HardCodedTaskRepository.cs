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

        public List<TodoTask> all()
        {
            return tasks;
        }

        public List<TodoTask> allFromUser(string userId)
        {
            return tasks.Where(task => task.UserId == userId).ToList();
        }

        public void add(TodoTask task)
        {
            tasks.Add(task);
        }

        public void delete(string id)
        {
            var taskToRemove = search(id);
            if (taskToRemove == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            tasks.Remove(taskToRemove);
        }

        public TodoTask? search(string id)
        {
            return tasks.Find(t => t.Id == id);
        }
    }
}