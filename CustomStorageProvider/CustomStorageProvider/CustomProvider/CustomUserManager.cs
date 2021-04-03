//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace CustomStorageProvider.CustomProvider
//{
//    public class CustomUserManager : UserManager<ApplicationUser>
//    {
//        private readonly IUserPasswordStore<ApplicationUser> _passwordStore;

//        public CustomUserManager(
//            IUserStore<ApplicationUser> store,
//            IOptions<IdentityOptions> optionsAccessor,
//            IPasswordHasher<ApplicationUser> passwordHasher,
//            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
//            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
//            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
//            IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) :
//            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
//        {
//            _passwordStore = Store as IUserPasswordStore<ApplicationUser>;
//        }

//        public override async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
//        {
//            if (user is null) { return false; }

//            // Verify users password
//            PasswordVerificationResult result = await VerifyPasswordAsync(_passwordStore, user, password);

//            // if reshash is required, automatically rehash the password
//            if (result == PasswordVerificationResult.SuccessRehashNeeded)
//            {
//                await UpdatePasswordHash(user, password, validatePassword: false);
//                await UpdateUserAsync(user);
//            }

//            bool success = result != PasswordVerificationResult.Failed;
//            if (!success)
//            {
//                Logger.LogWarning(0, $"Invalid password for user {user.Id}.");
//            }
//            return success;
//        }

//    }
//}
