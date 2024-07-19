using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response.Account;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Presentation.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IConfiguration _configuration;
        public LoginPageModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [BindProperty] public LoginAccountRespone accountRespone { get; set; }
        [BindProperty] public CreateAccountRequest createAccountRequest { get; set; }
        public async Task<IActionResult> OnPostLogin()
        {
            var json = JsonSerializer.Serialize(accountRespone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("https://localhost:7075/api/Account/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RepoRespone<string>>(data);
                if (result.Data != null)
                {
                    var role = GetRoleFromJwt(result.Data);
                    if (role == "Admin")
                    {
                        HttpContext.Session.SetString("SerectKey", result.Data);
                        HttpContext.Session.SetString("Role", "Admin");
                        return RedirectToPage("/AdminProduct");
                    }
                    else
                    {
                        HttpContext.Session.SetString("SerectKey", result.Data);
                        HttpContext.Session.SetString("Role", "User");
                        return RedirectToPage("/HomePage");
                    }

                }
                else
                {
                    ViewData["Message"] = result.Message;
                    return Page();
                }
            }
            return Page();

        }
        public async Task<IActionResult> OnPostRegister()
        {
            var json = JsonSerializer.Serialize(createAccountRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7075/api/Account/CreateAccount", content);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RepoRespone<AccountRespone>>(data);
            if (result.Success == false)
            {
                ViewData["Notification"] = result.Message;
                return Page();
            }
            else
            {
                ViewData["Notification"] = result.Message;
                return RedirectToPage("/LoginPage");
            }
        }
        private string GetRoleFromJwt(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:SerectKey"]);

            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtTokenDecoded = (JwtSecurityToken)validatedToken;

            // Truy cập vào các thông tin trong payload
            var roleClaim = jwtTokenDecoded.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            return roleClaim;
        }
        public void OnGet()
        {
        }
    }
}
