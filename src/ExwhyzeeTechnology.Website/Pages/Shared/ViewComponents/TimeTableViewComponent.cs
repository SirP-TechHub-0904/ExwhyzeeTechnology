﻿using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace ExwhyzeeTechnology.Website.Pages.Shared.ViewComponents
{
    public class TimeTableViewComponent : ViewComponent
    {
        private readonly UserManager<Profile> _userManager;

        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;


        public TimeTableViewComponent(
            UserManager<Profile> userManager,
            ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context

            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string date)
        {

            var datet = await _context.TimeTables 
                .Where(x => x.Date.Date.ToString() == date).ToListAsync();
            var datex = await _context.TimeTables.FirstOrDefaultAsync(x => x.Date.Date.ToString() == date);
            ViewBag.datete = datex.Date.ToString("dddd dd MMM, yyyy");
            ViewBag.module = datex.Module;
            ViewBag.subtitle = datex.SubTitle;
            return View(datet);
        }
    }

}
