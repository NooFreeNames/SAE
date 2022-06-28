using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SAE_DB;
using SAE_Program.Properties;
using Microsoft.AspNet.Identity;

namespace SAE_Program
{
    public class CurrentSession
    {
        public static bool Set(uint UserId)
        {
            using var db = new SAEDBContext();
            var user = db.Users.FirstOrDefault(u => u.Id == UserId);
            if (user == null)
            {
                return false;
            }

            var session = new Session
            {
                User = UserId,
                DeviceIdHash = new PasswordHasher().HashPassword(GetMachineId()),
            };
            db.Sessions.Add(session);
            db.SaveChanges();
            SessionSettings.Default.SessionId = session.Id;
            SessionSettings.Default.Save();
            UserType = user.TupeUser;
            return true;
        }
        
        public static bool Remove()
        {
            if (SessionSettings.Default.SessionId == 0)
            {
                return false;
            }

            using var db = new SAEDBContext();
            var session = db.Sessions.FirstOrDefault(s => s.Id == SessionSettings.Default.SessionId);
            if (session != null)
            {
                db.Sessions.Remove(session);
            }
            SessionSettings.Default.SessionId = 0;
            SessionSettings.Default.Save();

            UserType = TypeUserEnum.None;
            return true;
        }

        static Action? onUserTypeChanged;
        public static event Action? OnUserTypeChanged 
        { 
            add 
            {
                onUserTypeChanged += value;
                value?.Invoke();
            } 
            remove 
            {
                onUserTypeChanged -= value;
            } 
        }

        static TypeUserEnum userType = TypeUserEnum.None;
        public static TypeUserEnum UserType 
        { 
            get
            {
                return userType;
            } 
            private set 
            {
                userType = value;
                onUserTypeChanged?.Invoke();
            } 
        }

        public static User? GetUser()
        {
            if (SessionSettings.Default.SessionId == 0)
            {
                return null;
            }

            using var db = new SAEDBContext();

            var session = db.Sessions.Include(s => s.UserNavigation).FirstOrDefault(s => s.Id == SessionSettings.Default.SessionId);
            if (session != null)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == session.User);
                var verify = new PasswordHasher().VerifyHashedPassword(session.DeviceIdHash, GetMachineId());
                if (verify == PasswordVerificationResult.Success)
                {
                    UserType = user != null ? user.TupeUser : TypeUserEnum.None;
                    return user;
                }
            }
            return null;
        }

        protected static string GetMachineId()
        {
            string path = Path.Combine(Registry.LocalMachine.Name, @"SOFTWARE\Microsoft\SQMClient");
            var MachineId = new Guid((string)Registry.GetValue(path, "MachineId", null)).ToString();
            var UserSid = WindowsIdentity.GetCurrent().User.AccountDomainSid.Value.Remove(0, 1);
            return (MachineId + UserSid).Replace("-", string.Empty);
        }
    }
}
