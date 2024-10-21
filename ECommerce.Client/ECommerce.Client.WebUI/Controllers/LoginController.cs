using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using ECommerce.Client.WebUI.Models.LoginModels;
using ECommerce.Client.WebUI.Models.Responses;
using ECommerce.Client.WebUI.Models.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ECommerce.Client.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public LoginController(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            RequestParameters param = new()
            {
                controller = "users",
                action = "Login"
            };
            var response = await _customHttpClientService.Post(param, model);

            if (response.Message != null)
            {
                MessageResponse mresponse = JsonConvert.DeserializeObject<MessageResponse>(response.Message);

                if (mresponse.AccessToken != null)
                {
                    Token token = new()
                    {
                        AccessToken = mresponse.AccessToken,
                        Expiration = Convert.ToDateTime(mresponse.Expiration),
                    };
                    // Kimlik bilgilerini (Claims) oluşturma
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UsernameOrEmail),
                new Claim("AccessToken", token.AccessToken),
                new Claim("Expiration", token.Expiration.ToString())
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Kullanıcıyı oturum açtırma (Cookies)
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = token.Expiration
                        });

                    return RedirectToAction("Index", "Home"); // Giriş başarılı, yönlendir
                }
                else
                {
                    ViewBag.Message = mresponse.Message;
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("AccessToken");
            HttpContext.Session.Remove("TokenExpiration");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return RedirectToAction("Index");
        }
    }
}
