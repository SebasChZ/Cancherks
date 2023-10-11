using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CancherksWebApp.Pages
{
    public class ErrorPageModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorPageModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Retrieve the Request ID from the current Activity or the HttpContext
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
