using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using HiLCoECS.Models;
using HiLCoECS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiLCoECS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService) =>
            _scheduleService = scheduleService;

        [HttpGet("all")]
        public async Task<List<Schedule>> Get() =>
            await _scheduleService.GetSchedules();

        [HttpGet]
        public async Task<List<string?>> ScheduleList()
        {
            List<Schedule> schedules = await _scheduleService.GetSchedules();
            List<string?> list = schedules.Select(s => s.BatchId).ToList();
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> Get(string id)
        {
            var schedule = await _scheduleService.GetSchedule(id);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (schedule is null) return NotFound();
            return schedule;
        }
    }
}