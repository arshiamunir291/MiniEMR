using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniEMR.Controllers
{
    [ApiController]
    public abstract class BaseAPIController : ControllerBase
    {
        protected int CurrentUserId =>
            int.Parse(
                User.FindFirst("userId")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        protected string CurrentUserRole =>
            User.FindFirst(ClaimTypes.Role)?.Value
            ?? User.FindFirst("role")!.Value;
    }
}
