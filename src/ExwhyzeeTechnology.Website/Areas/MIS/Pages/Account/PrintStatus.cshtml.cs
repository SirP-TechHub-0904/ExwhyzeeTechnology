using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExwhyzeeTechnology.Website.Areas.Account.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.Authorize]
     
    public class PrintStatusModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public PrintStatusModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Profile UserDatas { get; set; }
        public IList<AdditionalProfile> AdditionalProfile { get; set; }
        public IList<ProfileCategory> ProfileCategories { get; set; }
        public string ProfileLink { get; set; } 
        public SuperSetting SuperSetting { get; set; } 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(id == null) { 
            var user = await _userManager.GetUserAsync(User);
            UserDatas = await _userManager.FindByIdAsync(user.Id);
            }
            else
            {
                UserDatas = await _userManager.FindByIdAsync(id);

            }

            if (UserDatas == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(UserDatas.UniqueId))
            {
                while (UserDatas.UniqueId == null)
                {
                    string randomAlphaNumeric = GenerateRandomAlphaNumericUniqueId(8);
                    UserDatas.UniqueId = randomAlphaNumeric.ToString();

                    // Check if the generated IdDigit already exists in the database
                    var check = await _userManager.Users.FirstOrDefaultAsync(x => x.UniqueId == UserDatas.UniqueId);

                    if (check == null)
                    {
                        // If not found, update the UserDatas and exit the loop
                        await _userManager.UpdateAsync(UserDatas);
                        break;
                    }

                }
            }
            AdditionalProfile = await _context.AdditionalProfiles.Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            ProfileCategories = await _context.ProfileCategories
                .Include(x => x.ProfileCategoryLists).Where(x => x.ProfileId == UserDatas.Id).ToListAsync();
            
            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();

            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try

            {
                ProfileLink = "https://" + SuperSetting.CompanyWebsiteLink + "/uap/" + UserDatas.UniqueId;
                System.Drawing.Image img = barcode.Draw(ProfileLink, 100);

                byte[] imgBytes = turnImageToByteArray(img);
                //

                string imgString = Convert.ToBase64String(imgBytes);
                BarcodeImage = imgBytes;
            }
            catch (Exception c)
            {

            }

            return Page();
        }
        [BindProperty]
        public byte[] BarcodeImage { get; set; }
        private byte[] turnImageToByteArray(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
        static string GenerateRandomAlphaNumericUniqueId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
