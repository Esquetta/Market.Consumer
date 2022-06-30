using AutoMapper;
using Market.Consumer.Dtos;
using Market.Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    using (HttpClient deleteClient = new HttpClient())
                    {
                        var data = JsonSerializer.Serialize(product);
                        var httpMessage = new HttpRequestMessage(HttpMethod.Delete, Url + "Product/DeleteProduct")
                        {
                            Content = new StringContent(data, Encoding.UTF8, "application/json")
                        };
                        var result = await deleteClient.SendAsync(httpMessage);
                        if (result.IsSuccessStatusCode)
                        {
                            TempData.Add("ItemSuccessfully Deleted", "Item successfully deleted");
                            return RedirectToAction("index");
                        }
                    }

                }
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var reponse = await httpClient.GetStringAsync(Url + "Product/GetProductById?id=" + id);
                var product = JsonSerializer.Deserialize<Product>(reponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var catgoryRequest = await httpClient.GetStringAsync(Url + "Category/GetCategories");
                var categoires = JsonSerializer.Deserialize<List<Category>>(catgoryRequest, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                SelectList categoryList = new SelectList(categoires, "categoryId", "categoryName");
                ViewBag.categoryList = categoryList;
                return View(product);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var catgoryRequest = await httpClient.GetStringAsync(Url + "Category/GetCategories");
                    var categoires = JsonSerializer.Deserialize<List<Category>>(catgoryRequest, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    SelectList categoryList = new SelectList(categoires, "categoryId", "categoryName");
                    ViewBag.categoryList = categoryList;
                }
                return View();
            }
            using (HttpClient updateClient = new HttpClient())
            {
                var data = JsonSerializer.Serialize(product);
                var httpMessage = new HttpRequestMessage(HttpMethod.Put, Url + "Product/UpdateProduct")
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
                var result = await updateClient.SendAsync(httpMessage);
                if (result.IsSuccessStatusCode)
                {
                    TempData.Add("ItemSuccessfully Updated", "Item successfully deleted");
                    return RedirectToAction("index");
                }
            }
            return View();


        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var catgoryRequest = await httpClient.GetStringAsync(Url + "Category/GetCategories");
                var categoires = JsonSerializer.Deserialize<List<Category>>(catgoryRequest, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                SelectList categoryList = new SelectList(categoires, "categoryId", "categoryName");
                ViewBag.categoryList = categoryList;
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreationDto productCreationDto)
        {
            if (!ModelState.IsValid)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var catgoryRequest = await httpClient.GetStringAsync(Url + "Category/GetCategories");
                    var categoires = JsonSerializer.Deserialize<List<Category>>(catgoryRequest, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    SelectList categoryList = new SelectList(categoires, "categoryId", "categoryName");
                    ViewBag.categoryList = categoryList;
                }
                return View();
            }
            using (HttpClient updateClient = new HttpClient())
            {
                var product = mapper.Map<Product>(productCreationDto);
                var data = JsonSerializer.Serialize(product);
                var httpMessage = new HttpRequestMessage(HttpMethod.Post, Url + "Product/CreateProduct")
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
                var result = await updateClient.SendAsync(httpMessage);
                if (result.IsSuccessStatusCode)
                {
                    TempData.Add("ItemSuccessfully Created", "Item successfully created");
                    return RedirectToAction("index");
                }
            }
            return View(productCreationDto);
        }
    }
}
