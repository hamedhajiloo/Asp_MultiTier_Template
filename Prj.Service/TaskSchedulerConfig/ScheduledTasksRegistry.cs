namespace Prj.Services.TaskSchedulerConfig
{
    public static class ScheduledTasksRegistry
    {
        public static void Start()
        {
            //ScheduledTasksCoordinator.Current.AddScheduledTasks(); => in IoC Config
            ScheduledTasksCoordinator.Current.OnUnexpectedException = (exception, scheduledTask) =>
            {
                //todo: log the exception.
                System.Diagnostics.Trace.WriteLine(scheduledTask.Name + ":" + exception.Message);
            };

            ScheduledTasksCoordinator.Current.Start();
        }

        public static void End()
        {
            ScheduledTasksCoordinator.Current.Dispose();
        }
    }
}