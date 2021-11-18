using HiLCoECS.Models;
using MongoDB.Driver;

namespace HiLCoECS.Services
{
    public class ScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedules;

        public ScheduleService(IScheduleDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _schedules = database.GetCollection<Schedule>(settings.ScheduleCollectionName);
        }

        public List<Schedule> GetSchedules() => 
            _schedules.Find(schedule => true).ToList();
        
        public Schedule GetSchedule(String id) => _schedules.Find<Schedule>(schedule => schedule._ID == id).FirstOrDefault(); 
        
    }
}