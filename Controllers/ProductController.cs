using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Market.Consumer.Controllers
{
    public class ProductController : Controller
    {

        private string Url = "https://localhost:44374/";
        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient=new HttpClient())
            {
                httpClient
            }
        }
    }
}
