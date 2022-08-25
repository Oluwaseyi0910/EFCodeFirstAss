using System;
using System.Linq;

namespace EFCodeFirstAss
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int operation;
            Console.WriteLine("Welcome To My Password Manager");
            Console.WriteLine("To Save a new Password - Press 1\n To Find Password - Press 2 \n To Change Password - Press 3");

            if (Int32.TryParse(Console.ReadLine(), out operation))
            {
                if (operation == 1)
                {
                    Console.WriteLine("Enter Site Name");
                    string siteName = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    string sitePassword = Console.ReadLine();
                    int siteId = InsertPassword(siteName, sitePassword);
                    Console.WriteLine($"Operation Successful!!\n Your Entry ID is {siteId}");

                }
                else if (operation == 2)
                {
                    Console.WriteLine("Do you remember the Entry ID?  [y/n]");
                    string isID = Console.ReadLine();
                    if (isID == null || isID.ToLower() == "n")
                    {
                        Console.WriteLine("Enter Site Name");
                        string siteName = Console.ReadLine();
                        var password = FindPassword(siteName);
                        Console.WriteLine($"Found Password: {password}");
                    }
                    else if (isID.ToLower() == "y")
                    {
                        Console.WriteLine("Enter Entry ID");
                        if (int.TryParse(Console.ReadLine(), out int siteId))
                        {
                            Console.WriteLine("Enter Site Name");
                            string siteName = Console.ReadLine();
                            var password = FindPassword(siteId, siteName);
                            Console.WriteLine($"Found Password: {password}");
                        }
                    }
                }
                else if (operation == 3)
                {
                    Console.WriteLine("Do you remember the Entry ID?  [y/n]");
                    string isID = Console.ReadLine();
                    if (isID == null || isID.ToLower() == "n")
                    {
                        Console.WriteLine("Enter Site Name");
                        string siteName = Console.ReadLine();

                        Console.WriteLine("\nEnter New Password");
                        string sitePassword = Console.ReadLine();
                        int issuccessful = UpdatePassword(siteName, sitePassword);
                        Console.WriteLine($"Successful!! Updated {issuccessful} passwords");
                    }
                    else if (isID.ToLower() == "y")
                    {
                        Console.WriteLine("Enter Entry ID");
                        if (int.TryParse(Console.ReadLine(), out int siteId))
                        {
                            Console.WriteLine("Enter Site Name");
                            string siteName = Console.ReadLine();
                            Console.WriteLine("\nEnter New Password");
                            string sitePassword = Console.ReadLine();
                            int issuccessful = UpdatePassword(siteId, siteName, sitePassword);
                            Console.WriteLine($"Successful!! Updated {issuccessful} passwords");
                        }
                    }
                }
            }
        }

        static int InsertPassword(string siteName, string sitePassword)
        {
            try
            {

                var db = new Models.PasswordDbContext();
                var passwords = new Models.Passwords();

                passwords.SiteName = siteName;
                passwords.Password = sitePassword;

                db.Passwords.Add(passwords);
                db.SaveChanges();
                int siteId = db.Passwords.Where(x => x.SiteName.Contains(siteName)).First().Id;
                return siteId;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        static string FindPassword(string siteName)
        {
            try
            {
                var db = new Models.PasswordDbContext();
                var wantedRow = db.Passwords.Where(x => x.SiteName.Contains(siteName)).Select(x => x.Password).Take(1).ToArray();
                return wantedRow[0];
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return "Error Occurred";
            }
        }
        static string FindPassword(int siteId, string siteName)
        {
            try
            {
                var db = new Models.PasswordDbContext();
                var wantedRow = db.Passwords.Where(x => x.SiteName.Contains(siteName) && x.Id == siteId).Select(x => x.Password).Take(1).ToArray();
                return wantedRow[0];
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return "Error Occurred";
            }
        }
        static int UpdatePassword(string siteName, string password)
        {
            try
            {
                var db = new Models.PasswordDbContext();
                var wantedRow = db.Passwords.Where(x => x.SiteName.Contains(siteName)).First();
                wantedRow.Password = password;
                db.Passwords.Update(wantedRow);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        static int UpdatePassword(int siteid, string siteName, string password)
        {
            try
            {
                var db = new Models.PasswordDbContext();
                var wantedRow = db.Passwords.Where(x => x.SiteName.Contains(siteName) && x.Id == siteid).First();
                wantedRow.Password = password;
                db.Passwords.Update(wantedRow);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }


}


