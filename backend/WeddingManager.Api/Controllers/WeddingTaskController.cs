using Microsoft.AspNetCore.Mvc;
using WeddingManager.Application.DTOs;
using WeddingManager.Application.Services;


namespace WeddingManager.Api.Controllers
{
    [ApiController]
    [Route("api/weddings/{weddingId}/tasks")]

    public class WeddingTaskController : ControllerBase
    {
        private readonly WeddingTaskService _weddingTaskService;

        public WeddingTaskController(WeddingTaskService weddingTaskService)
        {
            _weddingTaskService = weddingTaskService;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasksByWeddingId(Guid weddingId)
        {
            try
            {


                var tasks = await _weddingTaskService.GetTasksByWeddingIdAsync(weddingId);

                var response = tasks.Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Description,
                    t.DueDate,
                    t.IsCompleted,
                    t.WeddingId
                });

                return Ok(response);
            } catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while retrieving tasks (GetTasksByWeddingId endpoint) : {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeddingTask(Guid weddingId, [FromBody] CreateWeddingTaskDto createWeddingTaskDto)
        {
            try
            {
                var weddingTask = await _weddingTaskService.CreateWeddingTaskAsync(weddingId, createWeddingTaskDto);

                return Ok(new TaskResponseDto
                {
                    Id = weddingTask.Id,
                    Title = weddingTask.Title,
                    Description = weddingTask.Description,
                    DueDate = weddingTask.DueDate,
                    IsCompleted = weddingTask.IsCompleted,
                    WeddingId = weddingTask.WeddingId
                });
            }

            catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while creating the wedding task: {ex.Message}");
            }


        }

        [HttpGet("{taskId:guid}")]
        public async Task<IActionResult> GetSingleTaskByWeddingId(Guid weddingId, Guid taskId)
        {
            try
            {


                var task = await _weddingTaskService.GetSingleWeddingTaskByIdAsync(taskId, weddingId);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(new TaskResponseDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                    WeddingId = task.WeddingId
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while retrieving the task: {ex.Message}");
            }
            
        }

        [HttpPut("{taskId:guid}")]
        public async Task<IActionResult> UpdateWeddingTask(Guid weddingId, Guid taskId, [FromBody] UpdateWeddingTaskDto updateWeddingTaskDto)
        {
            try
            {
                var updatedTask = await _weddingTaskService.UpdateWeddingTaskAsync(taskId, weddingId, updateWeddingTaskDto);

                return Ok(updatedTask);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while updating the wedding task: {ex.Message}");
            }
        }
        
        [HttpDelete("{taskId:guid}")]
        public async Task<IActionResult> DeleteWeddingTask(Guid weddingId, Guid taskId)
        {
            try

            {   
                if (weddingId == Guid.Empty || taskId == Guid.Empty)
                {
                    return BadRequest("Invalid weddingId or taskId.");
                }
                var deletedTaskId = await _weddingTaskService.DeleteWeddingTaskAsync(taskId);

                return Ok(deletedTaskId);
            }

            catch(Exception ex)
            {
                return this.StatusCode(500, $"An error occurred while deleting the wedding task: {ex.Message}");
            }
        }

    }
}