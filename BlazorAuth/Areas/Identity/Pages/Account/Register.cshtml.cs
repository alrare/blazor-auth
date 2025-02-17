﻿
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace BlazorAuth.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    public string ReturnUrl { get; set; } 

    public void OnGet() 
    { 
        
    }

    public async Task<ActionResult> OnPostAsync()
    {
        ReturnUrl = Url.Content("~/");
        if(ModelState.IsValid)
        {
            var identity = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(identity, Input.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(identity, isPersistent: false);
                return LocalRedirect(ReturnUrl);
            }
        }
        return Page();
    } 

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}