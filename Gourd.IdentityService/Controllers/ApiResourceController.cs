using Gourd.IdentityService.Common;
using Gourd.IdentityService.Model;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
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
    public class ApiResourceController : Controller
    {
        private readonly ConfigDbContext _context;

        public ApiResourceController(ConfigDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetList(ApiResource dto)
        {
            JsonResponse json = new JsonResponse();
            var query = _context.ApiResources.AsQueryable();
            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(m => m.Name == dto.Name);
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
        public IActionResult Edit(int Id)
        {
            var model = _context.ApiResources.FirstOrDefault(m => m.Id == Id);
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(ApiResource request)
        {
            JsonResponse response = new JsonResponse();


            using (var db = _context)
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var model = (await db.ApiResources.AddAsync(request)).Entity;
                        await db.SaveChangesAsync();
                        await db.ApiScopes.AddAsync(new ApiScope()
                        {
                            Name = request.Name,
                            ApiResourceId = model.Id,
                            DisplayName = model.DisplayName,
                            ShowInDiscoveryDocument = true
                        });
                        await db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        response.status = 0;
                        response.code = "200";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        response.status = -1;
                        response.code = "500";
                    }
                }
            }
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(ApiResource request)
        {
            JsonResponse response = new JsonResponse();
            var model = _context.ApiResources.FirstOrDefault(m => m.Id == request.Id);
            model.Name = request.Name;
            model.DisplayName = request.DisplayName;


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
            using(var db = _context)
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    //先移除对应的密钥
                    var list = await db.ApiSecrets.AsQueryable().Where(m => m.ApiResourceId == Id).ToListAsync();
                    db.ApiSecrets.RemoveRange(list);
                    //移除范围
                    var list_scopes = await db.ApiScopes.AsQueryable().Where(m => m.ApiResourceId == Id).ToListAsync();
                    db.ApiScopes.RemoveRange(list_scopes);
                    var model = db.ApiResources.FirstOrDefault(m => m.Id == Id);
                    db.ApiResources.Remove(model);
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            response.status = 0;
            response.code = "200";
            return Json(response);
        }
    }
}
