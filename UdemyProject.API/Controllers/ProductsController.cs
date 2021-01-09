﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyProject.API.DTOs;
using UdemyProject.API.Filters;
using UdemyProject.Core.Models;
using UdemyProject.Core.Services;

namespace UdemyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty,_mapper.Map<ProductDto>(newProduct));
        }

        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            var product = _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;

            _productService.Remove(product);

            return NoContent();
        }
    }
}
