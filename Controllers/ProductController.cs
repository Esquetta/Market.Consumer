using AutoMapper;
using Market.Consumer.Dtos;
using Market.Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
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
                var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(reponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var model = mapper.Map<List<ProductListViewDto>>(products);
                return View(model);
            }
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var reponse = await httpClient.GetStringAsync(Url + "Product/GetProductById?id=" + id);
                var product = JsonSerializer.Deserialize<Product>(reponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (product != null)
                {
                    using (HttpClient deleteClient =new HttpClient())
                    {
                        var data =JsonSerializer.Serialize(product);
                        var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Url+"Product/DeleteProduct")
                        {
                            Content = new StringContent(data, Encoding.UTF8, "application/json")
                        };
                        var result = await deleteClient.SendAsync(httpMessage);
                        if (result.IsSuccessStatusCode)
                        {
                            TempData.Add("ItemSuccessfully Deleted","Item successfully deleted");
                            return RedirectToAction("index");
                        }
                    }
                    
                }
                return RedirectToAction("index");
            }
        }
    }
}
