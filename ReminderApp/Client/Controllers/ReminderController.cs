using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReminderApp.Client.DTOs.Reminder;
using ReminderApp.Contracts.ModelName;
using ReminderApp.DataBase;
using ReminderApp.DataBase.Models;
using ReminderApp.Exceptions;

namespace ReminderApp.Client.Controllers
{
    [ApiController]
    public class ReminderController : ControllerBase
    {
        public readonly DataContext _dataContext;
        public readonly IMapper _mapper;

        public ReminderController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        #region List
        [HttpGet("reminders")]
        public async Task<IActionResult> GetAllAsync()
        {
            var reminders = await _dataContext.Reminders.AsNoTracking().ToListAsync();

            return Ok( _mapper.Map<List<ListItemDto>>(reminders));
        }
        #endregion

        #region Add
        [HttpPost("reminder")]
        public async Task<IActionResult> AddAsync([FromForm] AddDto dto)
        {
            var reminder = _mapper.Map<Reminder>(dto);

            await _dataContext.Reminders.AddAsync(reminder);

            await _dataContext.SaveChangesAsync();

            return Created(string.Empty,_mapper.Map<ListItemDto>(reminder));
        }
        #endregion

        #region Update
        [HttpPut("reminder/{id}")]
        public async Task<IActionResult> UpdateAsync(int id ,[FromForm] UpdateDto dto)
        {
            var reminder = await _dataContext.Reminders.FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException(DomainModelNames.REMINDER, id);

            _mapper.Map(dto, reminder);

            _dataContext.SaveChangesAsync();

            return Ok(_mapper.Map<ListItemDto>(reminder));
        }

        #endregion

        #region Delete Invidual
        [HttpDelete("reminder/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var reminder = await _dataContext.Reminders.FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException(DomainModelNames.REMINDER, id);

            _dataContext.Reminders.Remove(reminder);

            _dataContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete Bulk
        [HttpDelete("reminders")]
        public async Task<IActionResult> DeleteBulkAsync()
        {
            var reminders = await _dataContext.Reminders.ToListAsync();

            _dataContext.Reminders.RemoveRange(reminders);

            await _dataContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
