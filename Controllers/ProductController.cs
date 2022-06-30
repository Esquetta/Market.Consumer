using AutoMapper;
using Market.Consumer.Dtos;
using Market.Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Market.Consumer.Controllers
{
    public class ProductController : Controller
    {

        private readonly string Url = "https://localhost:44374/";
        private IMapper mapper;
        public ProductController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var reponse = await httpClient.GetStringAsync(Url + "Product/GetAllProducts");
                var products = JsonSerializer.Deserialize<List<Product>>(reponse);
                var model = mapper.Map<List<ProductListViewDto>>(products);
                return View(model);
            }
            return View();

        }
    }
}
