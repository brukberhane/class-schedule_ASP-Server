using HiLCoECS.Models;
using MongoDB.Driver;

namespace HiLCoECS.Services
{
    public class ScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedules;

        public ScheduleService(IScheduleDatabaseSettings settings){
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
              MongoClient client = new MongoClient(settings.ConnectionString);
              var database = client.GetDatabase(settings.DatabaseName);

              _schedules = database.GetCollection<Schedule>(settings.ScheduleCollectionName);

            } else
            {
              MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONN_STRING"));
              var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE"));

              _schedules = database.GetCollection<Schedule>(Environment.GetEnvironmentVariable("MONGO_COLLECTION"));
            }
        }

        public async Task<List<Schedule>> GetSchedules() =>
            await _schedules.Find(_=> true).ToListAsync();
        public async Task<Schedule> GetSchedule(String batchId) => 
            await _schedules.Find<Schedule>(schedule => schedule.BatchId == batchId).FirstOrDefaultAsync(); 

        public async Task<Schedule> Create(Schedule schedule)
        {
            await _schedules.InsertOneAsync(schedule);
            return schedule;
        }

        public async Task<List<Schedule>> Create(List<Schedule> schedules){
            await _schedules.InsertManyAsync(schedules);
            return schedules;
        }
        public async Task Remove(Schedule schedule) => 
            await _schedules.DeleteOneAsync(sched => sched._ID == schedule._ID);
        public async Task Remove(string id) => 
            await _schedules.DeleteOneAsync(schedule => schedule.BatchId == id);

        public async Task RemoveAll() => 
            await _schedules.DeleteManyAsync(schedule => true);
    }
}
