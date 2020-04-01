using Gourd.IdentityService.Common;
using Gourd.IdentityService.Model;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
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
    public class ClientController : Controller
    {
        private readonly ConfigurationDbContext _context;

        public ClientController(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetList(IdentityServer4.Models.Client dto)
        {
            JsonResponse json = new JsonResponse();
            var query = _context.Clients.AsQueryable();
            if (!string.IsNullOrEmpty(dto.ClientId))
            {
                query = query.Where(m => m.ClientId == dto.ClientId);
            }
            var list = await query.ToListAsync();

            json.status = 0;
            json.total = list.Count();
            json.data = list;
            return Json(json);
        }

        public IActionResult Create(int Id)
        {
            var model = new IdentityServer4.Models.Client();
            if (Id == 1)
            {
                //webapi
                model = new IdentityServer4.Models.Client
                {
                    ClientId = "webapi",
                    Description = "webapi示例模型",
                    ClientSecrets = new[] { new IdentityServer4.Models.Secret("123123") },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireConsent = false,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new[] { "AAA" }
                };
            }
            else
            {
                //mvc
                model = new IdentityServer4.Models.Client
                {
                    ClientId = "mvc",
                    Description = "mvc示例模型",
                    RequireConsent = false,
                    ClientSecrets = new[] { new IdentityServer4.Models.Secret("111111") },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                };
            }
            return View(model);
        }
        //public IActionResult Edit(int Id)
        //{
        //    //转换模型后,依旧没办法获取里面的对象,尴尬
        //    var model = _context.Clients.FirstOrDefault(m => m.Id == Id).ToModel();
        //    return View(model);
        //}



        [HttpPost]
        public async Task<JsonResult> Create(IdentityServer4.Models.Client request)
        {
            JsonResponse response = new JsonResponse();
            request.ClientSecrets.ToList().ForEach(m => 
            {
                m.Value = m.Value.Sha256();
            });
            var model = (await _context.Clients.AddAsync(request.ToEntity())).Entity;
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

        //[HttpPost]
        //public async Task<JsonResult> Edit(IdentityServer4.Models.Client request)
        //{
        //    JsonResponse response = new JsonResponse();
        //    var model = _context.Clients.FirstOrDefault(m => m.ClientId == request.ClientId);
        //    _context.Update(model);
        //    await _context.SaveChangesAsync();
        //    response.status = 0;
        //    response.code = "200";
        //    return Json(response);
        //}


        [HttpPost]
        public async Task<JsonResult> Del(int Id)
        {
            JsonResponse response = new JsonResponse();
            var model = await _context.Clients.FirstOrDefaultAsync(m => m.Id == Id);
            _context.Clients.Remove(model);
            _context.SaveChanges();
            response.status = 0;
            response.code = "200";
            return Json(response);
        }
    }
}
