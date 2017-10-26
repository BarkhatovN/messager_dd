using System;

namespace Messager.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
    }
}
