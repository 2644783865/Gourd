using Gourd.IdentityService.Common;
using Gourd.IdentityService.Model;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Controllers
{

    [Authorize]
    public class ApiSecretsController : Controller
    {
        private readonly ConfigDbContext _context;

        public ApiSecretsController(ConfigDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetList(ApiSecret dto)
        {
            JsonResponse json = new JsonResponse();
            var query = _context.ApiSecrets.AsQueryable();
            if (!string.IsNullOrEmpty(dto.Description))
            {
                query = query.Where(m => m.Description == dto.Description);
            }
            if (dto.ApiResourceId != 0)
            {
                query = query.Where(m => m.ApiResourceId == dto.ApiResourceId);
            }
            var list = await query.ToListAsync();
            var total = list.Count();
            var list_secret = new List<SysApiSecret>();
            if (total > 0)
            {
                foreach (var item in list)
                {
                    var apiResource = await _context.ApiResources.FirstOrDefaultAsync(m => m.Id == item.ApiResourceId);
                    SysApiSecret m_secret = new SysApiSecret()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        ApiResourceId = item.ApiResourceId,
                        ApiResourceName = apiResource.Name,
                        Created = item.Created,
                        Value = item.Value
                    };
                    list_secret.Add(m_secret);
                }
            }
            json.status = 0;
            json.total = total;
            json.data = list_secret;
            return Json(json);
        }

        public async Task<IActionResult> Create()
        {
            await ApiResourceSelectList();
            return View();
        }
        public async Task<IActionResult> Edit(int Id)
        {
            await ApiResourceSelectList();
            var model = await _context.ApiSecrets.FirstOrDefaultAsync(m => m.Id == Id);
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(ApiSecret request)
        {
            JsonResponse response = new JsonResponse();
            var model = (await _context.ApiSecrets.AddAsync(request)).Entity;
            await _context.SaveChangesAsync();
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
        public async Task<JsonResult> Edit(ApiSecret request)
        {
            JsonResponse response = new JsonResponse();
            var model = _context.ApiSecrets.FirstOrDefault(m => m.Id == request.Id);
            model.Description = request.Description;
            model.ApiResourceId = request.ApiResourceId;
            if (model.Value != request.Value)
            {
                model.Value = request.Value.Sha256();
            }
            _context.Update(model);
            await _context.SaveChangesAsync();
            response.status = 0;
            response.code = "200";
            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> Del(int Id)
        {
            JsonResponse response = new JsonResponse();
            var model = _context.ApiSecrets.FirstOrDefault(m => m.Id == Id);
            _context.Remove(model);
            response.status = 0;
            response.code = "200";
            return Json(response);
        }


        private async Task ApiResourceSelectList()
        {
            var list = await _context.ApiResources.AsQueryable().ToListAsync();
            ViewData["ApiResourceSelectList"] = new SelectList(list, "Id", "DisplayName");
        }
    }
}
