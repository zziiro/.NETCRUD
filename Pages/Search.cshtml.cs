using Microsoft.AspNetCore.Mvc.RazorPages;
using LogIn.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace LogIn.Pages;

public class SearchModel : PageModel
{

    private readonly DatabaseAccess dataAccess = new DatabaseAccess();
    public List<string> searchResults = new List<string>();
    [BindProperty]
    public string Username { get; set; } = string.Empty;

    

    public void OnGet()
    {

    }

    public void OnPostSearch()
    {
        try
        {
            // check if user input is valid
            if(Username == string.Empty)
            {
                RedirectToPage("/Error");
            }

            // if yes than
            searchResults = dataAccess.searchByUsername(Username);

            // check if the username exists
            if(searchResults == null)
            {
                RedirectToPage("/Error");
            }

        }catch(Exception ex)
        {
            Console.WriteLine("There has been an error searching for user with the username: " + Username);
            Console.WriteLine("[ERROR] - " + ex.ToString());
            RedirectToPage("/Error");
        }

    }
}