using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE_DB;
using Microsoft.AspNet.Identity;
using System.Security;
using Microsoft.Win32;
using System.IO;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Windows;

namespace SAE_Program.Pages
{
    public class ProfilePageViewModel : NotifyPropertyChanged
    {
        public ProfilePageViewModel(
            Func<string> passwordHash,
            Func<string?, PasswordVerificationResult> verifyHashedPassword,
            Func<bool> passwordIsEmpty,
            Func<bool> isConfirmPassword)
        {
            PasswordHash = passwordHash;
            VerifyHashedPassword = verifyHashedPassword;
            PasswordIsEmpty = passwordIsEmpty;
            IsConfirmPassword = isConfirmPassword;
            SwitchingMode = new Command(delegate { IsRegistrationMode = !IsRegistrationMode; });
            
            Commit = new Command(delegate
            {
                if (IsRegistrationMode)
                {
                    SignIn();
                }
                else
                {
                    SignUp();
                }
            });
            LogOutCommand = new Command(delegate { AccountLogOut(); }, delegate { return isSuccessfulLogin; });
            var user = CurrentSession.GetUser();
            if (user != null)
            {
                SuccessfulLogin(user, false);
            }

            
        }

        Func<string> PasswordHash;
        Func<string?, PasswordVerificationResult> VerifyHashedPassword;
        Func<bool> PasswordIsEmpty;
        Func<bool> IsConfirmPassword;

        public Command SwitchingMode { get; set; }
        public Command Commit { get; set; }
        public Command LogOutCommand { get; set; }

        string userName = null!;
        string userEmail = null!;
        string enteredName = null!;
        string enteredEmail = null!;
        bool isRegistrationMode = true;
        bool isSuccessfulLogin = false;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string UserEmail 
        { 
            get => userEmail; 
            set
            {
                userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string EnteredName
        {
            get => enteredName;
            set
            {
                enteredName = value;
                OnPropertyChanged(nameof(EnteredName));
            }
        }
        public string EnteredEmail
        {
            get => enteredEmail;
            set
            {
                enteredEmail = value;
                OnPropertyChanged(nameof(EnteredEmail));
            }
        }
        public bool IsRegistrationMode
        {
            get => isRegistrationMode;
            set
            {
                isRegistrationMode = value;
                OnPropertyChanged(nameof(IsRegistrationMode));
            }
        }

        protected bool DataVerification()
        {
            if (IsRegistrationMode)
            {
                if (string.IsNullOrWhiteSpace(EnteredName))
                {
                    return false;
                }
                if (!IsConfirmPassword())
                {
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(EnteredEmail))
            {
                return false;
            }
            if (PasswordIsEmpty())
            {
                return false;
            }


            return true;
        }
        protected void SignIn()
        {
            if (!DataVerification())
            {
                return;
            }

            var user = new User()
            {
                Name = EnteredName,
                Email = EnteredEmail,
                PasswordHach = PasswordHash(),
                TupeUser = TypeUserEnum.None,
            };

            using var db = new SAEDBContext();
            db.Users.Add(user);
            db.SaveChanges();
            SuccessfulLogin(user);
        }
        protected void SignUp()
        {
            if (!DataVerification())
            {
                return;
            }
            using var db = new SAEDBContext();
            var user = db.Users.FirstOrDefault(x => x.Email == EnteredEmail);

            var res = VerifyHashedPassword(user?.PasswordHach);
            if (user == null || res != PasswordVerificationResult.Success)
            {
                return;
            }
            SuccessfulLogin(user);
        }
        protected void SuccessfulLogin(User user, bool isSessionAdd = true)
        {
            UserName = user.Name;
            UserEmail = user.Email;
            if (isSessionAdd)
            {
                CurrentSession.Set(user.Id);
            }
            isSuccessfulLogin = true;
        }
        protected void AccountLogOut()
        {
            UserName = string.Empty;
            UserEmail = string.Empty;
            CurrentSession.Remove();
            isSuccessfulLogin= false;
        }

    }

}
