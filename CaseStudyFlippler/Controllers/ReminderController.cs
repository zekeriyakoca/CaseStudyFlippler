using AutoMapper;
using CaseStudyFlippler.Application.Dtos;
using CaseStudyFlippler.Application.Entities;
using CaseStudyFlippler.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyFlippler.API.Controllers
{
    // TODO : Users access should be validated before exposing or allowing upsert the reveiws. 
    //        This scenario shall be implemented after authorization/authentication system is defined.


    //Model validation handled automatically since we are using ApiController attribute
    [ApiController]
    [Route("api/reminder-management")]
    public class ReminderController : ControllerBase
    {
        private readonly ILogger<ReminderController> logger;
        private readonly IReminderRepository reminderRepository;
        private readonly IMapper mapper;

        public ReminderController(ILogger<ReminderController> logger, IReminderRepository reminderRepository, IMapper mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.reminderRepository = reminderRepository ?? throw new ArgumentNullException(nameof(reminderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("/users/{userId}/reminders")]
        [ProducesResponseType(StatusCodes.Status200OK)] // No need to define type of the body since we are using ActionResult<T>
        public async Task<ActionResult<PaginatedList<ReminderDto>>> GetReminders([FromRoute]int userId, [FromQuery] ReminderSearchRequestDto query)
        {
            var reminders = reminderRepository.GetAllLazy().Where(r=>r.UserId == userId);
            if (query != null && query.HasQuery)
            {
                if (!String.IsNullOrWhiteSpace(query.SearchText))
                {
                    reminders = reminders.Where(r =>r.Description != null &&  r.Description.ToLower().Contains(query.SearchText.ToLower()));
                }
                if (query.StartDate != null)
                {
                    reminders = reminders.Where(b => b.RemindAt > query.StartDate);
                }
                if (query.EndDate != null)
                {
                    reminders = reminders.Where(b => b.RemindAt < query.EndDate);
                }
            }

            query.PageSize = query.PageSize == 0 ? 10 : query.PageSize;
            var paginatedReminders = PaginatedList<ReminderDto>.Create(reminders.Select(r => mapper.Map<ReminderDto>(r)), query.Page, query.PageSize);

            return Ok(paginatedReminders);
        }

        [HttpGet("reminders/{reminderId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)] // No need to define type of the body since we are using ActionResult<T>
        public async Task<ActionResult<ReminderDto>> GetReminder([FromRoute] int reminderId)
        {
            var reminder = reminderRepository.Find(r => r.Id == reminderId).SingleOrDefault();
            if (reminder == default)
                return NotFound();
            return Ok(reminder);
        }

        [HttpPost("/users/{userId}/reminders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Reminder>> CreateReminder([FromRoute] int userId, [FromBody] ReminderCreateDto dto)
        {
            if (dto == default)
                return BadRequest();
            if (userId != dto.UserId)
                return BadRequest("userId should be the same in both url and body");
            //TODO : Add Duplication rule if needed
            var entityToCreate = mapper.Map<Reminder>(dto);
            //Validation of user existance is implemented in Add method of ReminderRepository. Will be moved here after UserRepository is implemented.
            reminderRepository.Add(entityToCreate);

            await reminderRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReminder), new { reminderId = entityToCreate.Id }, entityToCreate);
        }

        [HttpPut("/users/{userId}/reminders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateReminder([FromRoute] int userId, [FromBody] ReminderUpdateDto dto)
        {
            if (dto == default)
                return BadRequest();
            var currentEntity = await reminderRepository.Get(dto.Id);
            if (currentEntity == default)
                return BadRequest();
            if (currentEntity.UserId != userId)
                return BadRequest();  // Will be replaced with Unauthorized

            mapper.Map<ReminderUpdateDto, Reminder>(dto, currentEntity);

            await reminderRepository.SaveChangesAsync();
            return Ok(mapper.Map<ReminderDto>(currentEntity));
        }

        [HttpDelete("/users/{userId}/reminders/{reminderId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteReminder([FromRoute] int userId, [FromRoute] int reminderId)
        {
            var entityToDelete = reminderRepository.Find(r => r.Id == reminderId && r.UserId == userId).SingleOrDefault();
            if (entityToDelete == default)
                return BadRequest();
            reminderRepository.Remove(entityToDelete);

            await reminderRepository.SaveChangesAsync();

            return NoContent();
        }

        //TODO : Implement DeleteBulkReminder, UpdateBulkReminder and CreateBulkReminder...


    }
}
