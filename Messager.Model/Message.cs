using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messager.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public String Text { get; set; }
        public ICollection<Byte[]> Attachments { get; set; }
        public Boolean IsSelfDestructing { get; set; }
        public DateTime Date { get; set; }
    }
}
