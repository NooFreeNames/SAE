using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;


using Microsoft.AspNet.Identity;

namespace SAE_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage2.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            DataContext = new ProfilePageViewModel(PasswordHash, VerifyHashedPassword, PasswordIsEmpty, IsConfirmPassword);
        }

        private string PasswordHash()
        {
            return new PasswordHasher().HashPassword(PasswordBarEl.Password);
        }

        private PasswordVerificationResult VerifyHashedPassword(string? hash)
        {
            return new PasswordHasher().VerifyHashedPassword(hash, PasswordBarEl.Password);
        }

        private bool PasswordIsEmpty()
        {
            return PasswordBarEl.SecurePassword.Length <= 0;
        }

        private bool IsConfirmPassword()
        {
            return PasswordBarEl.Password == ConfirmPasswordBarEl.Password;
        }
    }
}
