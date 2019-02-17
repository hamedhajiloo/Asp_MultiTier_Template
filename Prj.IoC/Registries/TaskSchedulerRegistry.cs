using Prj.Services.TaskSchedulerConfig;
using StructureMap;

namespace Prj.IoC.Registries
{
    public class TaskSchedulerRegistry : Registry
    {
        public TaskSchedulerRegistry()
        {
            this.Scan(scan =>
            {
                scan.Assembly("Prj.Web");
                scan.AddAllTypesOf<ScheduledTaskTemplate>().NameBy(item => item.FullName);
            });
        }
    }
}
