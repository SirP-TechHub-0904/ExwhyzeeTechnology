using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Domain.Models
{
    public class ActivityLog
    {
        public long Id { get; set; }

        public string? EntityName { get; set; }
        public string? Action { get; set; }  // "Created" or "Edited"
        public string? Changes { get; set; } // JSON or plain text of what changed
        public DateTime Timestamp { get; set; }
        public string? UserId { get; set; }  // ID of the user who made the change
        public string? DeviceInformation { get; set; }  // Nullable string for device info
    }
}
