using BusinessLogicLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Services
{
    public interface IDashboardService
    {
        public Task<DashboardViewModel> GetDashboardData(string userId);
    }
}
