using GetaGadget.Common;
using GetaGadget.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace GetaGadget.API.Authorization
{
    public class GetaGadgetAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int[] _userRoles;

        public GetaGadgetAuthorizeAttribute()
        {
            _userRoles = new int[]
            {
               (int) UserRoleType.User,
               (int) UserRoleType.Admin
            };
        }

        public GetaGadgetAuthorizeAttribute(UserRoleType userRole)
        {
            _userRoles = new int[]
            {
                (int) userRole
            };
        }

        public GetaGadgetAuthorizeAttribute(UserRoleType[] userRoles)
        {
            _userRoles = (int[])userRoles.Select(ur => (int)ur);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Settings.TokenSecretBytes),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    int.TryParse(jwtToken.Claims.FirstOrDefault(x => x.Type == ((int)TokenClaim.UserRoleId).ToString())?.Value, out var userRoleId);

                    if (!_userRoles.Contains(userRoleId))
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
                else
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
