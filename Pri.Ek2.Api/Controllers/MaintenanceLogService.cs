using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Mechanic,Admin")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceLogService _maintenanceService;

        public MaintenanceController(IMaintenanceLogService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

    }
}
