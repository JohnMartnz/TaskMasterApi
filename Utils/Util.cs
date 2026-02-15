using TaskMasterApi.Services;

namespace TaskMasterApi.Utils;

public static class Util
{
  public static Models.Task? FindTaskById(int id)
  {
    return TaskDataStore.Current.Tasks.FirstOrDefault(task => task.Id == id);
  }

  public static int GetNextIndex()
  {
    IEnumerable<Models.Task> tasks = TaskDataStore.Current.Tasks;

    if (!tasks.Any())
    {
      return 1;
    }

    return tasks.Max(task => task.Id) + 1;
  }
}
