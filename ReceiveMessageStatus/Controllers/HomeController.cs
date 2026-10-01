using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReceiveMessageStatus.Models;

namespace ReceiveMessageStatus.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost("/webhooks/status")]
    public IActionResult HandleStatus([FromBody] MessageStatus status)
    {
        Console.WriteLine($"Message {status.MessageUuid} to {status.To}: {status.Status}");
        Console.WriteLine(status);

        if (status.Error is not null)
        {
            Console.WriteLine($"Error {status.Error.Title}: {status.Error.Detail}");
        }

        return NoContent();
    }

}
