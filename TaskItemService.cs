using ASP_NET_08._TaskFlow_DTOs.Data;
using ASP_NET_08._TaskFlow_DTOs.Models;
using ASP_NET_08._TaskFlow_DTOs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ASP_NET_08._TaskFlow_DTOs.DTOs.TaskItem_DTOs;

namespace ASP_NET_08._TaskFlow_DTOs.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDbContext _context;

    public TaskItemService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto CreateDto)
    {

        var taskItem = new TaskItem
        {
            Title = CreateDto.Title,
            Description = CreateDto.Description,
            CreatedAt = DateTime.UtcNow,
            Status = Models.TaskStatus.ToDo,
            ProjectId = CreateDto.ProjectId
        };

        var projectExixts = await _context
                                        .Projects
                                        .AnyAsync(p => p.Id == taskItem.ProjectId);

        if (!projectExixts)
            throw new
                ArgumentException($"Project with ID {taskItem.ProjectId} not found");



        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        await _context
            .Entry(taskItem)
            .Reference(t => t.Project)
            .LoadAsync();

        var response = new TaskItemResponseDto()
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            ProjectId = taskItem.ProjectId,
            Status = taskItem.Status.ToString(),
            ProjectName = taskItem.Project?.Name ?? string.Empty
        };

        return response;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task is null) return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    

    
    public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
    {
        var tasks = await _context.TaskItems
                            .Include(t => t.Project)
                            .ToListAsync();

        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(int id)
    {
        var taskItem = await _context.TaskItems
                                .Include(t => t.Project)
                                .FirstOrDefaultAsync(t => t.Id == id);

        if (taskItem is null) return null;

        return MapToResponseDto(taskItem);
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _context.TaskItems
                            .Include(t => t.Project)
                            .Where(t => t.ProjectId == projectId)
                            .ToListAsync();

        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto taskItem)
    {
        var existingTask = await _context.TaskItems
                                    .Include(t => t.Project)
                                    .FirstOrDefaultAsync(t => t.Id == id);

        if (existingTask is null) return null;

        existingTask.Title = taskItem.Title;
        existingTask.Description = taskItem.Description;
        existingTask.Status = taskItem.Status;
        existingTask.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponseDto(existingTask);
    }


    private TaskItemResponseDto MapToResponseDto(TaskItem task)
    {
        return new TaskItemResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.ToString(),
            ProjectId = task.ProjectId,
            ProjectName = task.Project?.Name ?? string.Empty
        };
    }
}
