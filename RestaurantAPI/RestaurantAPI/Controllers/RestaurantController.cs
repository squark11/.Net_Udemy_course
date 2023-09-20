﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{

    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }



        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] UpdateRestaurantDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _restaurantService.Update(id, updateDto);

            var isUpdated = _restaurantService.Update(id, updateDto);

            if (!isUpdated) { 
                return NotFound();
            } 
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _restaurantService.Delete(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            
            var restaurantsDtos = _restaurantService.GetAll();
            
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]

        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            
            var restaurant = _restaurantService.GetById(id);


            if(restaurant is null)
            {
                return NotFound();
            }
            var restaurantDto = restaurant;
            return Ok(restaurantDto);
        }
         
    }
}
