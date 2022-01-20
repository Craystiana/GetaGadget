using GetaGadget.API.Authorization;
using GetaGadget.BusinessLogic.Services;
using GetaGadget.Common.Enums;
using GetaGadget.Domain.DTO.Order;
using GetaGadget.Domain.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace GetaGadget.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<UserController> _logger;

        public OrderController(OrderService orderService, ILogger<UserController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [GetaGadgetAuthorize]
        public IActionResult Index()
        {
            try
            {
                var userId = (int) GetCurrentUserId();

                var orderModel = _orderService.GetCurrentOrder(userId);

                return new JsonResult(orderModel);
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
                _orderService.AddProductToOrder((int)GetCurrentUserId(), productId);

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
                    _orderService.RemoveProductFromOrder((int)GetCurrentUserId(), productId);

                    return new JsonResult(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error removing product from order");
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
        }

        [HttpGet]
        [Route("History")]
        [GetaGadgetAuthorize]
        public IActionResult History()
        {
            try
            {
                var userId = (int)GetCurrentUserId();

                return new JsonResult(_orderService.GetHistory(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order history");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("Place")]
        [GetaGadgetAuthorize]
        public IActionResult PlaceOrder([FromBody] PlaceOrderModel model)
        {
            try
            {
                _orderService.PlaceOrder((int)GetCurrentUserId(), model);

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order history");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCoupons")]
        [GetaGadgetAuthorize]
        public IActionResult GetCoupons()
        {
            try
            {
                return new JsonResult(_orderService.GetCoupons());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching coupons");
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
