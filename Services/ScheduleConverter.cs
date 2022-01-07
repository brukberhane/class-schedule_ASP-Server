using HiLCoECS.Models;
using Microsoft.VisualBasic.FileIO;
using HiLCoECS.Utils;
using MongoDB.Bson;

// ReSharper disable IdentifierTypo

namespace HiLCoECS.Services
{
  public static class ScheduleConverter
  {
    public static List<Schedule> ParseSchedule(IFormFile file)
    {
      List<ServerSchedule> schedules = new List<ServerSchedule>();
      using (TextFieldParser parser = new TextFieldParser(file.OpenReadStream()))
      {
        // ! This line seems counter intuitive because it might mess up the line number but it's essential
        // to keep the process going.
        Console.WriteLine(parser.ReadLine());
        
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        while (!parser.EndOfData)
        {
          // Process Schedules on the server side.
          if (parser.LineNumber != 1)
          {
            string[]? fields = parser.ReadFields();
            schedules.Add(ServerSchedule.FromCSV(fields));
          }
        }
      }
      // ? Convert the schedules to the more structured versions and return them.
      return ConvertSchedules(schedules);
    }

    static List<Schedule> ConvertSchedules(List<ServerSchedule> serverSchedules)
    {
      List<Schedule> schedules = new List<Schedule>();
      // FIXME: Remember to restore contingency if this LINQ fails.
      // var temp = serverSchedules.GroupBy(p => p.SectionName).Select(s => s.First()).ToArray();
      var batches = serverSchedules.Select(p => p.SectionName).GroupBy(p => p).Select(p => p.First()).ToArray();
      // var batches = temp.Select(s => s.SectionName).ToArray();
      foreach (var batch in batches) 
      {
        // * Create a new schedule for every batch 
        var sched = new Schedule();
        // ? Extract the schedules matching this batch;
        var servS = serverSchedules.Where(p => p.SectionName == batch);
        sched.BatchId = batch;
        // TODO: Maybe remove this linter thingy once you know it works
        // ReSharper disable PossibleMultipleEnumeration
        sched.Program = servS.Select(p => p.ProgramName).FirstOrDefault();
        sched.Shift = servS.Select(p => p.ShiftName).FirstOrDefault();
        // ? Initialize the days so they get recognized as an array.
        sched.Days = new List<Day> { };
        foreach (var day in Constants.CLASS_DAYS)
        {
          var scheDay = new Day
          {
            DoW = day,
            // ? Initialize the periods so it gets recognized as an array.
            Periods = new List<Period> { }
          };
          for (int i = 1; i <= Constants.PERIODS_IN_DAY; i++)
          {
            var period = new Period
            {
              Number = i
            };
            var servP = servS.Where(p => p.DayName == day && p.PeriodNumber == i);
            period.Name = servP.Select(p => p.CourseTitle).FirstOrDefault();
            period.Code = servP.Select(p => p.CourseCode).FirstOrDefault();
            period.Room = servP.Select(p => p.RoomName).FirstOrDefault();
            period.Lecturer = servP.Select(p => p.InstructorName).FirstOrDefault();
            // Manually process class time
            switch (period.Number)
            {
              case 1:
                period.Time = "08:00";
                break;
              case 2:
                period.Time = "10:00";
                break;
              case 3:
                period.Time = "13:30";
                break;
              case 4:
                period.Time = "15:30";
                break;
              default:
                break;
            }
            scheDay.Periods?.Add(period);
          }

          sched.Days.Add(scheDay);
        }
        schedules.Add(sched);
      }
      return schedules;
    }
  }
}