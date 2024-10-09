using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.ServicesContract;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class BasketController : BaseApiController
    {
        private readonly IBasketServices _basketServices;
        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasket()
    {
        var buyerId = Request.Cookies["BuyerId"];
        if (string.IsNullOrEmpty(buyerId))
        {
            return NotFound("No BuyerId found in cookies");
        }

        var basket = await _basketServices.GetBasketAsync(buyerId);
        if (basket == null)
        {
            return NotFound("Basket not found");
        }

        return Ok(basket);
    }
}}