using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExwhyzeeTechnology.Domain.Enum.Enum;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class TrainingApplicationForm
    {
        public long Id { get; set; }

        public string? ProfileId { get; set; }
        public Profile Profile { get; set; }

        [Display(Name = "Highest Level of Education")]
        public string? HighestLevelOfEducation { get; set; }

        [Display(Name = "Field of Study (if applicable)")]
        public string? FieldOfStudy { get; set; }

        [Display(Name = "Current Educational Status (e.g., in school, graduated, etc.)")]
        public string? CurrentEducationalStatus { get; set; }

        [Display(Name = "Rate your proficiency in Computer Literacy (e.g., MS Office, Google Suite)")]
        public string? ComputerLiteracy { get; set; }

        [Display(Name = "Rate your proficiency in Web Development (e.g., HTML, CSS, JavaScript)")]
        public string? WebDevelopment { get; set; }

        [Display(Name = "Rate your proficiency in Graphic Design (e.g., Adobe Creative Suite)")]
        public string? GraphicDesign { get; set; }

        [Display(Name = "Rate your proficiency in Digital Marketing (e.g., social media, SEO, content creation)")]
        public string? DigitalMarketing { get; set; }

        [Display(Name = "Rate your proficiency in Programming (e.g., Python, Java, C++)")]
        public string? Programming { get; set; }

        [Display(Name = "Rate your proficiency in Data Analysis (e.g., Excel, SQL, data visualization)")]
        public string? DataAnalysis { get; set; }

        [Display(Name = "Other relevant digital skills")]
        public string? OtherRelevant { get; set; }

        [Display(Name = "Describe your previous experience with digital tools and technologies (if any)")]
        public string? ExperienceWithDigitalTools { get; set; }

        [Display(Name = "Why are you interested in this digital skill training and empowerment program?")]
        public string? WhyAreYouInterestedInThisDigitalSkill { get; set; }

        [Display(Name = "How do you believe this program can help you achieve your personal and/or career goals?")]
        public string? HowDoYouBelieveThisProgramCanHelpYouAchieveYourPersonalGoal { get; set; }

        [Display(Name = "What do you hope to gain from participating in this program?")]
        public string? WhatDoYouHopeToGainFromParticipatingInThisProgram { get; set; }

        [Display(Name = "Do you have any special needs or accommodations we should be aware of?")]
        public string? DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf { get; set; }

        [Display(Name = "How did you hear about this program?")]
        public string? HowDidYouHearAboutThisProgram { get; set; }

        [Display(Name = "Are you currently employed or self-employed? If so, please provide details.")]
        public string? AreYouCurrentlyEmployedOrSelfEmployed { get; set; }

        [Display(Name = "Employment Status")]
        public string? EmploymentStatus { get; set; }
        [Display(Name = "Employment details")]
        public string? EmploymentDetails { get; set; }
        [Display(Name = "Nature of Job")]
        public string? NatureOfJob { get; set; }

        [Display(Name = "Monthly Income")]
        public string? MonthlyIncome { get; set; }

        [Display(Name = "Do you have any other relevant information you would like to share?")]
        public string? DoYouHaveAnyOtherRelevantInformationYouWouldLikeToShare { get; set; }

        public long? CareerTrainingJobRoleId { get; set; }
        public CareerTrainingJobRole CareerTrainingJobRole { get; set; }

        public bool AcceptTerms {  get; set; }
        public AdmissionStatus AdmissionStatus { get; set; }
    }
}
