using ASWA.Core.Interfaces;

namespace ASWA.Core.Entities
{
    public class Customer : BaseEntityAudit, IAggregateRoot
    {
        private Customer()
        {
            // required by EF
        }

        public Customer(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}