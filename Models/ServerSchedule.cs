namespace HiLCoECS.Models
{
  public class ServerSchedule
  {
    public int? PeriodNumber { get; set; }
    public string? Campus { get; set; }
    public string? RoomName { get; set; }
    public string? ShiftName { get; set; }
    public string? ProgramName { get; set; }
    public string? SectionName { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseTitle { get; set; }
    public int? CreditHour { get; set; }
    public string? DayName { get; set; }
    public string? InstructorName { get; set; }
    public string? InstructorDepartment { get; set; }

    public static ServerSchedule FromCSV(string[]? csvLine)
    {
        ServerSchedule schedule = new ServerSchedule();
        schedule.PeriodNumber = string.IsNullOrEmpty(csvLine?[0]) ? 0 : int.Parse(csvLine[0]);
        schedule.Campus = csvLine?[1];
        schedule.RoomName = csvLine?[2];
        schedule.ShiftName = csvLine?[3];
        schedule.ProgramName = csvLine?[4];
        schedule.SectionName = csvLine?[5];
        schedule.CourseCode = csvLine?[6];
        schedule.CourseTitle = csvLine?[7];
        schedule.CreditHour = string.IsNullOrEmpty(csvLine?[8]) ? 0 : int.Parse(csvLine[8]);
        schedule.DayName = csvLine?[9];
        schedule.InstructorName = csvLine?[10];
        schedule.InstructorDepartment = csvLine?[11];

        return schedule;
    }
  }
}