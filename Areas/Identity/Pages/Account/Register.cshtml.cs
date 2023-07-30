using Azure.Identity;
using CardsCustomers.Models;
using CardsCustomers.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CardsCustomers.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly DbCoreloginContext _context;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            DbCoreloginContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        #region Properties

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public string MsgError { get; set; }
        [BindProperty]
        public List<SelectListItem> AllUserRoles { get; set; }
        public UserRoleService userrole { get; set; }
        #endregion


        #region Events
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = Url.Content("~/");
            var request = Request.Query;
            int id = 0;
            if (Request.Query.ContainsKey("id"))
            {
                id =Convert.ToInt32(Request.Query["id"]);
                UserAdminService useradminservice = new UserAdminService(_context);
                UserAdmin useradmin = new UserAdmin();
                Input = new InputModel();
                useradmin = useradminservice.GetUserAdminById(id);
                Input.Name = useradmin.Name;
                Input.Surname = useradmin.Surname;
                Input.DateOfBirth = useradmin.DateOfBirth;
                Input.Email = useradmin.Email;
                Input.Password = useradmin.Password;
                Input.ConfirmPassword = useradmin.ConfirmPassword;
                Input.IdUserRole = useradmin.IdUserRole;
            }
            if (AllUserRoles is null)
            {

                AllUserRoles = new List<SelectListItem>();

                //you can refer to the following code to get the select options from the database.  
                AllUserRoles = (from d in _context.UserRole
                                orderby d.IdUserRole
                                select new SelectListItem
                                {
                                    Text = d.Role,
                                    Value = Convert.ToString(d.IdUserRole)
                                }).OrderBy(x => x.Value).ToList();
                AllUserRoles.Insert(0, new SelectListItem { Text = "<- Select User Role->", Value = "-1" });

            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var identity = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(identity, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);

                    try
                    {
                        UserAdmin useradmin = new UserAdmin();
                        useradmin.Name = Input.Email;
                        useradmin.Email = Input.Email;
                        useradmin.Password = Input.Password;
                        useradmin.ConfirmPassword = Input.ConfirmPassword;
                        useradmin.Name = Input.Name;
                        useradmin.Surname = Input.Surname;
                        useradmin.DateOfBirth = Input.DateOfBirth;
                        useradmin.InsertDate = DateTime.Now;
                        useradmin.IdUserRole = Input.IdUserRole;
                        _context.Add(useradmin);
                        _context.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        MsgError = "Error in saving the new admin User.";
                        return Page();
                        //throw ex;
                    }
                    return LocalRedirect(ReturnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    //MsgError += error.Description.ToString();
                }
            }
            else
            {
                MsgError = "Invalid register attempt";

                if (AllUserRoles.Count <= 0)
                {

                    AllUserRoles = new List<SelectListItem>();

                    //you can refer to the following code to get the select options from the database.  
                    AllUserRoles = (from d in _context.UserRole
                                    orderby d.IdUserRole
                                    select new SelectListItem
                                    {
                                        Text = d.Role,
                                        Value = Convert.ToString(d.IdUserRole)
                                    }).OrderBy(x => x.Value).ToList();
                    AllUserRoles.Insert(0, new SelectListItem { Text = "<- Select User Role->", Value = "-1" });

                }
            }
            return Page();
        }
        #endregion

        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public int IdUserRole { get; set; }
        }
    }
}
