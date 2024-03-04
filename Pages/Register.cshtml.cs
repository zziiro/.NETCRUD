using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LogIn.DataAccess;

namespace LogIn.Pages;

public class RegisterModel : PageModel
{

    private readonly ILogger<RegisterModel> _logger;
    private readonly DatabaseAccess dataAccess = new DatabaseAccess();

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public RegisterModel(ILogger<RegisterModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }


    // Create a new Account Route 
    public IActionResult OnPost()
    {
        try
        {
            // call dataaccess 'create' method
            dataAccess.registerUser(Email, Username, Password);
        }catch(Exception ex){
            // log errors
            Console.WriteLine("error in creating new user account..");
            Console.WriteLine("[ERROR] - " + ex.ToString());
            RedirectToPage("/Error");
        }

        // log success
        Console.WriteLine("New user has been created!");

        // return back to index page if everything worked
        return RedirectToPage("/Index");
    }
}
