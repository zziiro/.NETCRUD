using LogIn.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogIn.Pages;

public class UpdateAccountModel : PageModel
{
    private readonly ILogger<UpdateAccountModel> _logger;
    private readonly DatabaseAccess dataAccess = new DatabaseAccess();

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string OldUsername { get; set; } = string.Empty;
    [BindProperty]
    public string OldPassword { get; set; } = string.Empty;

    [BindProperty]
    public string UpdatedUsername { get; set; } = string.Empty;
    [BindProperty]
    public string UpdatedPassword { get; set; } = string.Empty;

    public UpdateAccountModel(ILogger<UpdateAccountModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPostUpdateUsername()
    {
        try
        {
            dataAccess.updateUsername(OldUsername, UpdatedUsername, Password);
            // maybe add username here for validation purposes
            //dataAccess.updatePassword(OldPassword, UpdatedPassword);
        }catch(Exception ex)
        {
            Console.WriteLine("Unable to update information..");
            Console.WriteLine("[ERROR] - " + ex.ToString());
        }
        
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostUpdatePassword()
    {
        try
        {
            dataAccess.updatePassword(OldPassword, UpdatedPassword);
        }catch(Exception ex)
        {
            Console.WriteLine("Unable to update Information");
            Console.WriteLine("[ERROR] - " + ex.ToString());
            return RedirectToPage("/Error");
        }
        return RedirectToPage("/Index");
    }
}