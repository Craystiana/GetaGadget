﻿using GetaGadget.API.Authorization;
using GetaGadget.BusinessLogic.Services;
using GetaGadget.Common.Enums;
using GetaGadget.Domain.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GetaGadget.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Detail")]
        [GetaGadgetAuthorizeAttribute]
        public IActionResult Detail([FromQuery] int productId)
        {
            try
            {
                return new JsonResult(_productService.GetProductDetails(productId));
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to fetch car details for product with id " + productId + ".\nError:\n" + e);
                return new JsonResult(new ProductModel());
            }
        }

        [HttpGet]
        [Route("Edit")]
        [GetaGadgetAuthorizeAttribute(UserRoleType.Admin)]
        public IActionResult Edit([FromQuery] int productId)
        {
            try
            {
                return new JsonResult(_productService.GetProduct(productId));
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to fetch product details for product with id " + productId + ".\nError:\n" + e);
                return new JsonResult(new ProductEditModel());
            }
        }

        [HttpPost]
        [Route("Edit")]
        [GetaGadgetAuthorizeAttribute(UserRoleType.Admin)]
        public IActionResult Edit([FromBody] ProductEditModel model)
        {
            try
            {
                if (model.Id != null)
                {
                    _productService.Edit(model);
                }
                else
                {
                    _productService.Add(model);
                }

                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while adding the product " + ex);
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("List")]
        [GetaGadgetAuthorizeAttribute]
        public IActionResult List([FromBody] ProductQueryModel model)
        {
            try
            {
                return new JsonResult(_productService.GetList(model));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching the product list " + ex);
                return new JsonResult(false);
            }
        }

        [HttpPost]
        [Route("Delete")]
        [GetaGadgetAuthorizeAttribute(UserRoleType.Admin)]
        public IActionResult Delete([FromQuery] int productId)
        {
            try
            {
                _productService.Delete(productId);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to delete product with id " + productId + ".\nError:\n" + ex);
                return new JsonResult(false);
            }
        }
    }
}
