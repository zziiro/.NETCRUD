using LogIn.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogIn.Pages;

public class DeleteAccountModel : PageModel
{
    private readonly ILogger<DeleteAccountModel> _logger;
    private readonly DatabaseAccess dataAccess = new DatabaseAccess();

    [BindProperty]
    public string AccountToBeDeletedUsername { get; set; } = string.Empty;
    [BindProperty]
    public string AccountToBeDeletedPassword { get; set; } = string.Empty;

    public DeleteAccountModel(ILogger<DeleteAccountModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        try
        {
            // call 'delete' method
            dataAccess.deleteAccount(AccountToBeDeletedUsername, AccountToBeDeletedPassword);
        }catch(Exception ex)
        {
            Console.WriteLine("Account Could not be deleted");
            Console.WriteLine("[ERROR] - " + ex.ToString());
            return RedirectToPage("/Error");
        }
        return RedirectToPage("/Index");
    }

}