using GetaGadget.API.Authorization;
using GetaGadget.BusinessLogic.Services;
using GetaGadget.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace GetaGadget.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly WishlistService _wishlistService;
        private readonly ILogger<UserController> _logger;

        public WishlistController(WishlistService wishlistService, ILogger<UserController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
        }

        [HttpGet]
        [GetaGadgetAuthorize]
        public IActionResult Index()
        {
            try
            {
                var userId = (int) GetCurrentUserId();

                var wishlistProductModel = _wishlistService.GetUserWishlist(userId);

                return new JsonResult(wishlistProductModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order details");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("Add")]
        [GetaGadgetAuthorize]
        public IActionResult Add([FromQuery] int productId)
        {
            try
            {
                _wishlistService.AddProductToWishlist((int)GetCurrentUserId(), productId);

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to order");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [GetaGadgetAuthorize]
        public IActionResult Delete([FromQuery] int productId)
        {
                try
                {
                    _wishlistService.RemoveProductFromWishlist((int)GetCurrentUserId(), productId);

                    return new JsonResult(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error removing product from order");
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
        }

        private int? GetCurrentUserId()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                return int.Parse(JwtService.GetClaim(TokenClaim.UserId, token));
            }
            else
            {
                return null;
            }
        }
    }
}
