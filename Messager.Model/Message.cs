using System;
using System.Collections.Generic;

namespace Messager.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public String Text { get; set; }
        public List<Byte[]> Attachments { get; set; }
        public Boolean IsSelfDestructing { get; set; }
        public DateTime Date { get; set; }
    }
}
