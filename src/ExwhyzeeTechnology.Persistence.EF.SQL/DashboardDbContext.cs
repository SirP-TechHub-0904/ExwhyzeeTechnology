﻿using ExwhyzeeTechnology.Domain;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Infrastructure.TenantSupport;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ExwhyzeeTechnology.Domain.Models.Data;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ExwhyzeeTechnology.Persistence.EF.SQL;

public class DashboardDbContext : IdentityDbContext<Profile, IdentityRole, string>
{
    private readonly ITenantProvider _tenantProvider;


    public DbSet<CareerTrainingJobRole> CareerTrainingJobRoles { get; set; } = null!;
    public DbSet<SelectedJobRole> SelectedJobRoles { get; set; } = null!;


    public DbSet<SmsMessage> SmsMessages { get; set; } = null!;
    public DbSet<SmsMessageCategory> SmsMessageCategories { get; set; } = null!;
    public DbSet<SenderId> SenderIds { get; set; } = null!;


    //public DbSet<--> -- { get; set; } = null!;
    public DbSet<EvaluationQuestion> EvaluationQuestions { get; set; } = null!;
    public DbSet<EvaluationQuestionCategory> EvaluationQuestionCategories { get; set; } = null!;
    public DbSet<SmsSetting> SmsSettings { get; set; } = null!;
    public DbSet<DialCode> DialCodes { get; set; } = null!;
    public DbSet<PriceSetting> PriceSettings { get; set; } = null!;
    public DbSet<SuperSetting> SuperSettings { get; set; } = null!;
    public DbSet<EnableDevice> EnableDevices { get; set; } = null!;
    public DbSet<TemplateCategory> TemplateCategories { get; set; } = null!;
    public DbSet<TemplateType> TemplateTypes { get; set; } = null!;
    public DbSet<MailingSystem> MailingSystems { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<WebPage> WebPages { get; set; }
    public DbSet<FAQ> FAQs { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogCategory> BlogCategories { get; set; }
    public DbSet<PageSection> PageSections { get; set; }
    public DbSet<PageSectionList> PageSectionLists { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<PageCategory> PageCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<ProductSample> ProductSamples { get; set; }
    public DbSet<Testimony> Testimonies { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<NewsLetter> NewsLetter { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }

    //project dbset
    public DbSet<ProjectCategory> ProjectCategories { get; set; }

    public DbSet<ProjectMain> ProjectMains { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<ProjectTeam> ProjectTeams { get; set; }
    public DbSet<ProjectPosition> ProjectPositions { get; set; }
    public DbSet<ProjectFile> ProjectFiles { get; set; }
    public DbSet<ProjectFeature> ProjectFeatures { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<GeneralFeature> GeneralFeatures { get; set; }
    public DbSet<PostModal> PostModal { get; set; }
    public DbSet<DataConfig> DataConfigs { get; set; }







    public DbSet<Message> Messages { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<ClientRequest> ClientRequest { get; set; }
    public DbSet<Report> Report { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingAttendance> TrainingAttendances { get; set; }
    public DbSet<TrainingSchedule> TrainingSchedules { get; set; }




    public DbSet<Plan> Plans { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<JobFeature> JobFeatures { get; set; }
    public DbSet<Job> Jobs { get; set; }

    public DbSet<FundSource> FundSources { get; set; }
    public DbSet<CompanyFund> CompanyFunds { get; set; }
    public DbSet<ExpensesCategory> ExpensesCategories { get; set; }
    public DbSet<CompayExpenses> CompayExpenses { get; set; }
    public DbSet<Salary> Salarys { get; set; }
    public DbSet<JobDesignation> JobDesignations { get; set; }


    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<ProposalFile> ProposalFiles { get; set; }
    public DbSet<ChatMessage> ChatMessage { get; set; }
    public DbSet<CareerFile> CareerFiles { get; set; }
    public DbSet<CareerPath> CareerPaths { get; set; }

    public DbSet<LocalGoverment> LocalGoverments { get; set; }
    public DbSet<State> States { get; set; }





    public DbSet<AdditionalProfile> AdditionalProfiles { get; set; }
    public DbSet<ProfileCategory> ProfileCategories { get; set; }
    public DbSet<ProfileCategoryList> ProfileCategoryLists { get; set; }
    public DbSet<ProfilePortfolio> ProfilePortfolios { get; set; }
    public DbSet<ProfilePortfolioSetting> ProfilePortfolioSetting { get; set; }
    public DbSet<PortfolioContactUs> PortfolioContactUses { get; set; }
    public DbSet<PortfolioTemplate> PortfolioTemplates { get; set; }


    public DbSet<OfficeActivityCategory> OfficeActivityCategories { get; set; }
    public DbSet<OfficeActivityDialy> OfficeActivityDialies { get; set; }



    public DbSet<FormSetting> FormSettings { get; set; }
    public DbSet<DashboardSetting> DashboardSettings { get; set; }
    public DbSet<DashboardData> DashboardDatas { get; set; }
    public DbSet<DashboardDataList> DashboardDataLists { get; set; }
    public DbSet<CompanyProgram> CompanyPrograms { get; set; }
    public DbSet<CompanyProgramCategory> CompanyProgramCategories { get; set; }
    public DbSet<ProgramUser> ProgramUsers { get; set; }
    public DbSet<UserCategory> UserCategories { get; set; }


    public DbSet<XProject> XProjects { get; set; }
    public DbSet<XProjectSection> XProjectSections { get; set; }
    public DbSet<XProjectTask> XProjectTasks { get; set; }
    public DbSet<XProjectMessage> XProjectMessages { get; set; }
    public DbSet<XProjectFile> XProjectFiles { get; set; }


    public DbSet<BudgetMainCategory> BudgetMainCategories { get; set; }
    public DbSet<BudgetSubCategory> BudgetSubCategories { get; set; }
    public DbSet<BudgetList> BudgetLists { get; set; }




    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<Facility> Facilities { get; set; }

    public DbSet<TrainingApplicationForm> TrainingApplicationForms { get; set; }



    public DbSet<Cohort> Cohorts { get; set; }
    public DbSet<CohortAttendance> CohortAttendances { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<DialyActivity> DialyActivities { get; set; }
    public DbSet<DialyEvaluationQuestion> DialyEvaluationQuestions { get; set; }
    public DbSet<DialyParticipantEvaluation> DialyParticipantEvaluations { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<TimeTable> TimeTables { get; set; }
    public DbSet<TrainingTest> TrainingTests { get; set; }
    public DbSet<TrainingTestOption> TrainingTestOptions { get; set; }
    public DbSet<UserTest> UserTests { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<CohortProject> CohortProjects { get; set; }    
    public DbSet<UserAssignment> UserAssignments { get; set; }    
    public DbSet<GroupAssignment> GroupAssignments { get; set; }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardDbContext(DbContextOptions options, ITenantProvider tenantProvider, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _tenantProvider = tenantProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            _tenantProvider.ConnectionString,
            o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, /*_tenantProvider.DbSchemaName*/null));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        //configurationBuilder.Properties<string>()
        //    .HaveMaxLength(255);
    }
    public override int SaveChanges()
    {
        LogActivities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        LogActivities();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void LogActivities()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var deviceInfo = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();


        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in entries)
        {
            var action = entry.State == EntityState.Added ? "Created" : "Edited";
            var entityName = entry.Entity.GetType().Name;

            // Optional: Serialize the property changes to JSON
            var changes = new Dictionary<string, object>();
            foreach (var prop in entry.Properties)
            {
                changes[prop.Metadata.Name] = prop.CurrentValue;
            }

            var log = new ActivityLog
            {
                EntityName = entityName,
                Action = action,
                Changes = JsonSerializer.Serialize(changes),
                Timestamp = DateTime.UtcNow,
                UserId = userId ?? "Unknown", // Handle cases where UserId may not be available
                DeviceInformation = deviceInfo ?? "Unknown" // Capture the device info
            };

            // Save the log to the database or another log destination
            ActivityLogs.Add(log);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<AccountModeratorEvaluation>(entity =>
        //{
        //    entity.HasKey(e => e.Id);

        //    entity.HasOne(e => e.Moderator)
        //        .WithMany()
        //        .HasForeignKey(e => e.ModeratorId);

        //    entity.HasOne(e => e.TimeTable)
        //        .WithMany()
        //        .HasForeignKey(e => e.TimeTableId);

        //    entity.HasOne(e => e.UserParticipant)
        //        .WithMany()
        //        .HasForeignKey(e => e.UserParticipantId); // Ensure this matches the property in your class

        //    entity.HasOne(e => e.EvaluationQuestion)
        //        .WithMany()
        //        .HasForeignKey(e => e.EvaluationQuestionId);
        //});



        //modelBuilder.HasDefaultSchema(_tenantProvider.DbSchemaName);   // set schema
        //modelBuilder.Entity<ParticipantList>()
        //   .HasOne(p => p.Accomodation)
        //   .WithOne(a => a.ParticipantList)
        //   .HasForeignKey<Accomodation>(a => a.ParticipantListId);
        base.OnModelCreating(modelBuilder);

        // ConfigureCustomer(modelBuilder);
    }

    //private static void ConfigureCustomer(ModelBuilder builder)
    //{
    //    builder.Entity<Customer>(b =>
    //    {
    //        var table = b.ToTable("Customers");

    //        table.Property(p => p.CustomerId).ValueGeneratedOnAdd();
    //        table.HasKey(p => p.CustomerId).HasName("PK_CustomerId");
    //    });
    //}
}
