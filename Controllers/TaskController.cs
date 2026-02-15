using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMasterApi.Services;
using TaskMasterApi.Utils;

namespace TaskMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Models.Task>> GetTask()
        {
            return Ok(TaskDataStore.Current.Tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Task> GetTaskById(int id)
        {
            Models.Task? task = Util.FindTaskById(id);

            if (task == null)
            {
                return NotFound("Task Not Found");
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Models.Task> CreateTask(Models.TaskInsert taskInsert)
        {
            Models.Task newTask = new Models.Task()
            {
                Id = Util.GetNextIndex(),
                Title = taskInsert.Title,
                Description = taskInsert.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            TaskDataStore.Current.Tasks.Add(newTask);
            return Ok(newTask);
        }

        [HttpPut("{id}")]
        public ActionResult<Models.Task> UpdateTask(int id, Models.TaskInsert taskInsert)
        {
            Models.Task? task = Util.FindTaskById(id);

            if (task == null)
            {
                return NotFound("Task Not Found");
            }

            task.Title = taskInsert.Title;
            task.Description = taskInsert.Description;
            task.UpdatedAt = DateTime.Now;

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public ActionResult<Models.Task> DeleteTask(int id)
        {
            Models.Task? task = Util.FindTaskById(id);

            if (task == null)
            {
                return NotFound("Task Not Found");
            }

            TaskDataStore.Current.Tasks.Remove(task);

            return NoContent();
        }
    }
}
