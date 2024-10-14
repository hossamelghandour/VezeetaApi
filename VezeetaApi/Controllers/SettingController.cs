using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VezeetaApi.Dto;
using VezeetaApi.Repository.DiscountRepo;

namespace VezeetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SettingController : ControllerBase
    {
        private readonly IDiscountRepo discountRepo;

        public SettingController(IDiscountRepo discountRepo)
        {
            this.discountRepo = discountRepo;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] DiscountDto item)
        {
            discountRepo.Add(item);
            return Ok(true);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromForm] DiscountDto item)
        {
            try
            {
                discountRepo.Update(id, item);
                return Ok("Discount updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }



        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {

            try
            {

                discountRepo.Delete(id);
                return Ok("DiscountCode Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }
        [HttpPut("Deactivate")]
        public IActionResult DeActivated(int id)
        {

            try
            {

                discountRepo.DeActivated(id);
                return Ok("DiscountCode DeActivated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }
    }
}
