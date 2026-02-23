using ASP_NET_08._TaskFlow_DTOs.DTOs.TaskItem_DTOs;
using ASP_NET_08._TaskFlow_DTOs.Models;

namespace ASP_NET_08._TaskFlow_DTOs.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createDto);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateDto);
    Task<bool> DeleteAsync(int id);
}
