using System;
namespace HiLCoECS.Models
{
    public class ScheduleDatabaseSettings: IScheduleDatabaseSettings
    {
        public string ScheduleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IScheduleDatabaseSettings
    {
        string ScheduleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

