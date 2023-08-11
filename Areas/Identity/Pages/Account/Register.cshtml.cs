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
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            DbCoreloginContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }
        #region Properties

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public string MsgError { get; set; }
        [BindProperty]
        public List<SelectListItem> AllUserRoles { get; set; }
        public UserRoleService userrole { get; set; }
        [BindProperty]
        public int IdUserAdmin { get; set; }
        #endregion


        #region Events
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = Url.Content("~/");
            var request = Request.Query;

            if (Request.Query.ContainsKey("id"))
            {
                IdUserAdmin = Convert.ToInt32(Request.Query["id"]);
                UserAdminService useradminservice = new UserAdminService(_context);
                UserAdmin useradmin = new UserAdmin();
                Input = new InputModel();
                useradmin = useradminservice.GetUserAdminById(IdUserAdmin);
                Input.Name = useradmin.Name;
                Input.Surname = useradmin.Surname;
                Input.DateOfBirth = useradmin.DateOfBirth;
                Input.Email = useradmin.Email;
                Input.Password = useradmin.Password;
                Input.ConfirmPassword = useradmin.ConfirmPassword;
                Input.IdUserRole = useradmin.IdUserRole;
            }
            DataBind();

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = Url.Content("~/");

            if (IdUserAdmin == 0)
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(Input.Password) || string.IsNullOrEmpty(Input.ConfirmPassword))
                    {
                        MsgError = "Password are required fields.";
                        return Page();
                    }
                    var identity = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                    var result = await _userManager.CreateAsync(identity, Input.Password);

                    UserRoleService userroleservice = new UserRoleService(_context);
                    string rolename = userroleservice.GetUserRoleNameById(Input.IdUserRole);

                    var role = new IdentityRole(rolename);
                    var DoExistRole = await _roleManager.GetRoleIdAsync(role);
                    if (string.IsNullOrEmpty(DoExistRole))
                    {
                        var addRoleResult = await _roleManager.CreateAsync(role);
                        if (!addRoleResult.Succeeded)
                        {
                            foreach (var error in addRoleResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);

                                //MsgError += error.Description.ToString();
                            }
                            return Page();
                        }

                    }

                    var addUserRoleResult = await _userManager.AddToRoleAsync(identity, rolename);


                    if (result.Succeeded && addUserRoleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(identity, isPersistent: false);

                        try
                        {
                            UserAdmin useradmin = new UserAdmin();
                            useradmin.Name = Input.Name;
                            useradmin.Surname = Input.Surname;
                            useradmin.DateOfBirth = Input.DateOfBirth;
                            useradmin.Email = Input.Email;
                            string pass = userroleservice.HashPassword(Input.Password);
                            useradmin.Password = pass;
                            useradmin.ConfirmPassword = pass;
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

                    foreach (var error in addUserRoleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        //MsgError += error.Description.ToString();
                    }
                }
                else
                {
                    MsgError = "Error in saving the new admin User.";
                    return Page();
                }
               

            }
            else
            {
                try
                {
                  
                    UserAdminService useradminservice = new UserAdminService(_context);
                    UserAdmin useradmin = new UserAdmin();
                    useradmin = useradminservice.GetUserAdminById(IdUserAdmin);
                    useradmin.Name = Input.Name;
                    useradmin.Surname = Input.Surname;
                    useradmin.DateOfBirth = Input.DateOfBirth;
                    useradmin.Email = Input.Email;
                    useradmin.InsertDate = DateTime.Now;
                    useradmin.IdUserRole = Input.IdUserRole;
                    _context.Update(useradmin);
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
           
            DataBind();

            return Page();
        }

        #endregion

        #region DataBind
        public void DataBind()
        {
            AllUserRoles = new List<SelectListItem>();
            if (AllUserRoles.Count <= 0 || AllUserRoles is null)
            {

                //AllUserRoles = new List<SelectListItem>();

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
        #endregion
        public class InputModel
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Surname { get; set; }
            [Required]
            public DateTime? DateOfBirth { get; set; }
            [Required]
            public int IdUserRole { get; set; }
        }
    }
}
