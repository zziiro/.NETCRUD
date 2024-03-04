using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LogIn.Pages;

public class CrudModel : PageModel
{

    private readonly ILogger<CrudModel> _logger;

    public CrudModel(ILogger<CrudModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
    