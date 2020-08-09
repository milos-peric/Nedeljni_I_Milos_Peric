using Nedeljni_I_Milos_Peric.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Milos_Peric
{
    class DataBaseService
    {

        internal vwAdmin AddAdministrator(vwAdmin admin)
        {
            try
            {
                using (CompanyDBEntities context = new CompanyDBEntities())
                {
                    tblUser newUser = new tblUser();
                    newUser.FirstName = admin.FirstName;
                    newUser.LastName = admin.LastName;                  
                    newUser.JMBG = admin.JMBG;                  
                    newUser.Gender = admin.Gender;
                    newUser.Residence = admin.Residence;
                    newUser.MarriageStatus = admin.MarriageStatus;
                    newUser.Username = admin.Username;
                    newUser.UserPassword = admin.UserPassword;
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    admin.UserID = newUser.UserID;
                    tblAdmin newAdmin = new tblAdmin();
                    newAdmin.ExpirationDate = admin.ExpirationDate;
                    newAdmin.AdminType = admin.AdminType;
                    newAdmin.UserID = admin.UserID;
                    context.tblAdmins.Add(newAdmin);
                    context.SaveChanges();
                    admin.AdminID = newAdmin.AdminID;
                    return admin;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwManager AddManager(vwManager manager)
        {
            try
            {
                using (CompanyDBEntities context = new CompanyDBEntities())
                {
                    tblUser newUser = new tblUser();
                    newUser.FirstName = manager.FirstName;
                    newUser.LastName = manager.LastName;
                    newUser.JMBG = manager.JMBG;
                    newUser.Gender = manager.Gender;
                    newUser.Residence = manager.Residence;
                    newUser.MarriageStatus = manager.MarriageStatus;
                    newUser.Username = manager.Username;
                    newUser.UserPassword = manager.UserPassword;
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    manager.UserID = newUser.UserID;
                    tblManager newManager = new tblManager();
                    newManager.Email = manager.Email;
                    newManager.ReservedPassword = manager.ReservedPassword;
                    newManager.SuccessfulProjects = manager.SuccessfulProjects;
                    newManager.OfficeNumber = manager.OfficeNumber;
                    newManager.UserID = manager.UserID;
                    context.tblManagers.Add(newManager);
                    context.SaveChanges();
                    manager.ManagerID = newManager.ManagerID;
                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwAdmin FindAdminCredentials(string userName, string password)
        {
            try
            {
                using (CompanyDBEntities context = new CompanyDBEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    vwAdmin adminToFind = (from a in context.vwAdmins where a.Username == userName && a.UserPassword == encryptedPassword select a).First();
                    return adminToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Doctor not found!" + ex.Message.ToString());
                return null;
            }
        }

        internal vwManager FindManagerCredentials(string userName, string password)
        {
            try
            {
                using (CompanyDBEntities context = new CompanyDBEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    vwManager managerToFind = (from m in context.vwManagers where m.Username == userName && m.UserPassword == encryptedPassword select m).First();
                    return managerToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Doctor not found!" + ex.Message.ToString());
                return null;
            }
        }

    }
}
