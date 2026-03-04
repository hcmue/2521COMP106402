using BaiTapBuoi03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaiTapBuoi03.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult SyncDemo()
        {
            var sw = new Stopwatch();
            sw.Start();
            Demo.FuncA();
            Demo.FuncB();
            Demo.FuncC();
            sw.Stop();
            return Content($"Chạy hết {sw.ElapsedMilliseconds} ms");
        }

        public async Task<IActionResult> SyncDemoAS()
        {
            var sw = new Stopwatch();
            sw.Start();
            var a = Demo.FuncAAsync();
            var b = Demo.FuncBAsync();
            var c = Demo.FuncCAsync();
            //await Task.WhenAll(a, b, c);
            await a; await b; await c;
            sw.Stop();
            return Content($"Chạy hết {sw.ElapsedMilliseconds} ms");
        }
    }
}
