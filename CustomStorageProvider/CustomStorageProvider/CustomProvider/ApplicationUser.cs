using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;

namespace CustomStorageProvider.CustomProvider
{
    public class ApplicationUser : IIdentity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual String PasswordHash { get; set; }
        public string NormalizedUserName { get; internal set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
    }
}
