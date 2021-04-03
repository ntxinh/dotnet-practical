using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CustomStorageProvider.CustomProvider
{
    public class CustomUserPasswordHasher : PasswordHasher<ApplicationUser>, IPasswordHasher<ApplicationUser>
    {
        public CustomUserPasswordHasher(
            IOptions<PasswordHasherOptions> optionsAccessor = null)
            : base(optionsAccessor) { }

        /// </summary>
        public override PasswordVerificationResult VerifyHashedPassword(
            ApplicationUser user, string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.Success;

            // pass as bytes
            //byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            //// we check the first byte
            ////    this byte indicates which hash algorithm is used
            //switch (decodedHashedPassword[0])
            //{
            //    // if it's old (old PHP platform), request rehash on success
            //    //case PortalUserHashHelper.PhpBBFormatMarker:
            //    //    if (LegacyPhpBBPasswordProvider.VerifyHashedPassword(
            //    //                        decodedHashedPassword, providedPassword))
            //    //    {
            //    //        return PasswordVerificationResult.SuccessRehashNeeded;
            //    //    }
            //    //    else
            //    //    {
            //    //        return PasswordVerificationResult.Failed;
            //    //    }

            //    // otherwise we use the default ASP.NET Core Identity behavior
            //    default:
            //        return base.VerifyHashedPassword(user, hashedPassword, providedPassword);
            //}
        }
    }
}
