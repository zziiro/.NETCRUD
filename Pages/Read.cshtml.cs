using LogIn.DataAccess;
using LogIn.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogIn.Pages;

public class ReadModel : PageModel
{
    private readonly DatabaseAccess dataAccess = new DatabaseAccess();
    public List<string> userList = new List<string>();

    // default constructor
    public ReadModel() { }

    
    // default page controller
    public void OnGet()
    {
        try
        {
            userList = dataAccess.AllUsers();

            // check to see if username exists
            if(userList.Count == 0)
            {
                RedirectToPage("/Error");
            }
        }catch(Exception ex)
        {
            Console.WriteLine("Error in fetching users");
            Console.WriteLine("[ERROR] - " + ex.ToString());
            RedirectToPage("/Error");
        }
    }
}