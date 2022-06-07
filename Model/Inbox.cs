using System;
using System.Collections.Generic;

#nullable disable

namespace Saythanks
{
    public partial class Inbox
    {
        public Inbox()
        {
            Notes = new HashSet<Note>();
        }

        public string Slug { get; set; }
        public string AuthId { get; set; }
        public bool? Enabled { get; set; }
        public bool? EmailEnabled { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
