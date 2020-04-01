using Gourd.IdentityService.Common;
using Gourd.IdentityService.Model;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _appcontext;

        public UserController(
         ApplicationDbContext appcontext)
        {
            _appcontext = appcontext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(IdentityUser dto)
        {
            JsonResponse json = new JsonResponse();
            var query = _appcontext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(dto.UserName))
            {
                query = query.Where(m => m.UserName == dto.UserName);
            }
            var list = await query.ToListAsync();
            json.status = 0;
            json.total = list.Count();
            json.data = list;
            return Json(json);
        }

        public IActionResult Create(string Tid)
        {
            return View();
        }
        public IActionResult Edit(string Id)
        {
            var model = _appcontext.Users.FirstOrDefault(m => m.Id == Id);
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(IdentityUser request)
        {
            JsonResponse response = new JsonResponse();
            request.Id = Guid.NewGuid().ToString();
            var model = (await _appcontext.Users.AddAsync(request)).Entity;
            await _appcontext.SaveChangesAsync();
            if (model != null)
            {
                response.status = 0;
                response.code = "200";
            }
            else
            {
                response.status = -1;
                response.code = "500";
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(IdentityUser request)
        {
            JsonResponse response = new JsonResponse();
            var model = _appcontext.Users.FirstOrDefault(m => m.Id == request.Id);
            model.UserName = request.UserName;
            model.PasswordHash = request.PasswordHash;
            model.Email = request.Email;
            model.PhoneNumber = request.PhoneNumber;
            _appcontext.Update(model);
            await _appcontext.SaveChangesAsync();
            response.status = 0;
            response.code = "200";
            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> Del(string Id)
        {
            JsonResponse response = new JsonResponse();
            var model = _appcontext.Users.FirstOrDefault(m => m.Id == Id);
            _appcontext.Users.Remove(model);
            await _appcontext.SaveChangesAsync();
            response.status = 0;
            response.code = "200";
            return Json(response);
        }

    }
}
