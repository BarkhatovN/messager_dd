using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messager.Model
{
    public class Chat
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public User Creater { get; set; }
    }
}
