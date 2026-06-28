using Task2_Internship.DTOs.Dashboard;

namespace Task2_Internship.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}