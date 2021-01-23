﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyProject.Core.Services;
using UdemyProject.Web.DTOs;

namespace UdemyProject.Web.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;        
        public NotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var category = await _categoryService.GetByIdAsync(id);
            if(category != null)
            {
                await next();
            }

            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;
                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı");

                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }
        }
    }
}
