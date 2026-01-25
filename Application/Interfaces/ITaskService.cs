using TaskFlow.Api.DTOs.Tasks;

namespace TaskFlow.Api.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponseDto> CreateAsync(string userId, CreateTaskRequestDto request);
        Task<IEnumerable<TaskResponseDto>> GetAllAsync(string userId, TaskQueryParameters query);
        Task<TaskResponseDto> GetByIdAsync(string userId, Guid taskId);
        Task UpdateAsync(string userId, Guid taskId, UpdateTaskRequestDto request);
        Task DeleteAsync(string userId, Guid taskId);
    }
}
