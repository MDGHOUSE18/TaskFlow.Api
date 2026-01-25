using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.Api.Application.Interfaces;
using TaskFlow.Api.Common;
using TaskFlow.Api.DTOs.Tasks;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequestDto requestDto)
        {
            var result = await _taskService.CreateAsync(UserId, requestDto);
            return Ok(ApiResponse<TaskResponseDto>.Ok(result));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskQueryParameters query)
        {
            var result = await _taskService.GetAllAsync(UserId, query);
            return Ok(ApiResponse<IEnumerable<TaskResponseDto>>.Ok(result));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetByIdAsync(UserId, id);
            return Ok(ApiResponse<TaskResponseDto>.Ok(result));

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskRequestDto updateTask)
        {
            await _taskService.UpdateAsync(UserId, id, updateTask);
            return Ok(ApiResponse<string>.Ok("Task Updated Sucessfully"));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteAsync(UserId, id);
            return Ok(ApiResponse<string>.Ok("Task Deleted Sucessfully"));

        }
    }
}
