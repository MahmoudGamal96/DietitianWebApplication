// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DietitianWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Signing;

namespace DietitianWebApplication.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            //Extend needed data
            [Required, MaxLength(100, ErrorMessage = "Maximum Length (100 characters)")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "Birth Date")]
            public DateTime BirthDate { get; set; }

            [Required]
            public char Sex { get; set; }

            [Required, MaxLength(50, ErrorMessage = "Maximum Length (50 characters)")]
            public string Nationality { get; set; }

            [Phone]
            [Display(Name = "Mobile No.")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Civil ID.")]
            public string CivilID { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Sex = user.Sex,
                Nationality = user.Nationality,
                CivilID = user.CivilID,
                PhoneNumber = phoneNumber,
                ProfilePicture = user.ProfilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //loading user data
            var fullName = user.FullName;
            var birthDate = user.BirthDate;
            var sex = user.Sex;
            var nationality = user.Nationality;
            var civilID = user.CivilID;

            //Update user values if they are changed

            if(Input.FullName != fullName)
            {
                user.FullName = Input.FullName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.BirthDate != birthDate)
            {
                user.BirthDate = Input.BirthDate;
                await _userManager.UpdateAsync(user);
            }

            if (Input.Sex != sex)
            {
                user.Sex = Input.Sex;
                await _userManager.UpdateAsync(user);
            }

            if (Input.Nationality != nationality)
            {
                user.Nationality = Input.Nationality;
                await _userManager.UpdateAsync(user);
            }

            if (Input.CivilID != civilID)
            {
                user.CivilID = Input.CivilID;
                await _userManager.UpdateAsync(user);
            }

            if(Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //Check Image Size
                if(file.Length > 0 && file.Length <= 4 * 1051057)
                {
                    //Check Image Extension
                    var extension = Path.GetExtension(file.FileName.TrimStart('.'));
                    if(extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || 
                       extension.ToLower() == ".png" || extension.ToLower() == ".bmp")
                    {
                        //Copy Profile Picture To Database
                        using (var ProfilePicMemoryStream = new MemoryStream())
                        {
                            await Request.Form.Files.FirstOrDefault().CopyToAsync(ProfilePicMemoryStream);
                            user.ProfilePicture = ProfilePicMemoryStream.ToArray();
                        }
                        await _userManager.UpdateAsync(user);
                    }
                }
            }


            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
