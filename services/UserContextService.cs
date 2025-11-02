using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.services
{
    public interface IUserContextService
    {
        int GetUserId();
    }
    public class UserContextService:IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}