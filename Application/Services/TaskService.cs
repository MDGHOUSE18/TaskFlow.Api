using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Application.Interfaces;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.DTOs.Tasks;
using TaskFlow.Api.Infrastructure.Data;

namespace TaskFlow.Api.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskResponseDto> CreateAsync(string userId, CreateTaskRequestDto request)
        {

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = Domain.Entities.TaskStatus.Pending,
                DueDate = request.DueDate,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.TaskItems.Add(task);
            _context.SaveChanges();
            return MapToResponse(task);
        }


        public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(string userId, TaskQueryParameters query)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((query.Page - 1) * query.PageSize)
                .Select(t => MapToResponse(t))
                .ToListAsync();
        }

        public async Task<TaskResponseDto> GetByIdAsync(string userId, Guid taskId)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            return MapToResponse(task);
        }

        public async Task UpdateAsync(string userId, Guid taskId, UpdateTaskRequestDto request)
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            task.Title = request.Title;
            task.Description = request.Description;
            task.Priority = request.Priority;
            task.Status = request.Status;
            task.DueDate = request.DueDate;
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(string userId, Guid taskId)
        {
            var task =await _context.TaskItems.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
        private static TaskResponseDto MapToResponse(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}
