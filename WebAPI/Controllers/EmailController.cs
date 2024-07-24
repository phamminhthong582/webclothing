using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request.Email;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRespository _emailRespository;

        public EmailController(IEmailRespository emailRespository)
        {
            _emailRespository = emailRespository;

        }
        [HttpPost("SendTestEmail")]
        public async Task<IActionResult> SendTestEmail([FromBody] EmailSendRequest emailSendDTO)
        {
            if (emailSendDTO == null)
            {
                return BadRequest("Invalid email request");
            }

            try
            {
                await _emailRespository.SendEmailAsync(emailSendDTO.To, emailSendDTO.Subject, emailSendDTO.message);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
