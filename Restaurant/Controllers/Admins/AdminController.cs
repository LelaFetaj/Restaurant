using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.DTOs.Admins;
using Restaurant.API.Models.Entities.Admins;
using Restaurant.API.Services.Admins;

namespace Restaurant.API.Controllers.Admins
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddAdmin(CreateAdminDto createAdminDto)
        {
            try
            {
                (bool result, string message) = await adminService.AddAdminAsync(createAdminDto);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Admin>>> GetAllAdmins()
        {
            try
            {
                List<Admin> sectors = await adminService.RetrieveAllAdmins();
                return Ok(sectors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(Guid id)
        {
            try
            {
                Admin admin = await adminService.RetrieveAdminById(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateAdmin(UpdateAdminDto updateAdminDto)
        {
            try
            {
                (bool result, string message) = await adminService.UpdateAdminAsync(updateAdminDto);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAdmin(Guid id)
        {
            try
            {
                (bool result, string message) = await adminService.DeleteAdminByIdAsync(id);

                if (result)
                {
                    return Ok(message);
                }

                return BadRequest(message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
