using Microsoft.AspNetCore.Http;

namespace Moodie.Helper
{
    public class AuthHelper
    {
        private readonly JWTService _jwtService;

        public AuthHelper(JWTService jwtService)
        {
            _jwtService = jwtService;
        }

        public bool IsUserLoggedIn(HttpRequest request, out int userId)
        {
            userId = 0;

            try
            {
                var jwt = request.Cookies["jwt"];
                if (string.IsNullOrEmpty(jwt))
                {
                    return false;
                }

                var token = _jwtService.Verify(jwt);
                userId = int.Parse(token.Issuer);
                return userId > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}