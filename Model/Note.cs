using System;
using System.Collections.Generic;

#nullable disable

namespace Saythanks
{
    public partial class Note
    {
        public Guid Uuid { get; set; }
        public string InboxesAuthId { get; set; }
        public string Body { get; set; }
        public string Byline { get; set; }
        public bool Archived { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Inbox InboxesAuth { get; set; }
    }
}
