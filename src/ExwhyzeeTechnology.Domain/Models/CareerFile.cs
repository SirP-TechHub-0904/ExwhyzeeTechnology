using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class CareerFile
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactAddress { get; set; }
        public string Position { get; set; }
        public string DOB { get; set; }
        public string CvUrl { get; set; }
        public string CvKey { get; set; }

        public string CertificateUrl { get; set; }
        public string CertificateKey { get; set; }

        public string ApplicationUrl { get; set; }
        public string ApplicationKey { get; set; }


        public string PassportUrl { get; set; }
        public string PassportKey { get; set; }

        public DateTime Date { get; set; }

        //public List<long> SelectedJobRoleIds { get; set; } = new List<long>();

        //public List<long> SelectedJobRoles { get; set; } = new List<long>();
        public ICollection<SelectedJobRole> SelectedJobRoles { get; set; }
    }
}
