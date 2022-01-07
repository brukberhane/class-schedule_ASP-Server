using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HiLCoECS.Models;
using HiLCoECS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.Logging;

namespace HiLCoECS.Pages
{
  public class UploadPage : PageModel
  {
    private readonly ILogger<UploadPage> _logger;
    private readonly ScheduleService _schedService;

    public UploadPage(ILogger<UploadPage> logger, ScheduleService scheduleService)
    {
      _logger = logger;
      _schedService = scheduleService;
    }

    [BindProperty] public UploadDb? FileUpload { get; set; }

    public IActionResult OnGet()
    {
      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      // if (FileUpload?.Password == Environment.GetEnvironmentVariable("SCHED_PASSWORD") && FileUpload?.FormFile != null)
      // {
      //   _schedService.RemoveAll();
      //   _schedService.Create(ScheduleConverter.ParseSchedule(FileUpload.FormFile));
      // }
      if (!ModelState.IsValid)
      {
        ViewData["Error"] = "Please correct the form";
        return Page();
      }
      
      if (FileUpload?.Password == Environment.GetEnvironmentVariable("SCHED_PASSWORD") && FileUpload?.FormFile != null){
        await _schedService.RemoveAll();
        await _schedService.Create(ScheduleConverter.ParseSchedule(FileUpload.FormFile));
        return Redirect("/api/Schedule");
      }

      return Page();
    }
  }

  // ReSharper disable once ClassNeverInstantiated.Global
  public class UploadDb
  {
    [Required]
    [Display(Name = "File")]
    public IFormFile? FormFile { get; set; }

    [Display(Name = "Password")]
    [StringLength(50, MinimumLength = 5)]
    public string? Password { get; set; }
  }

}
