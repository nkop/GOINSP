using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Net.Mail;
using System.Collections;

namespace GOINSP.ViewModel
{
    public class ForgottenPasswordVM : ViewModelBase
    {
        private Models.Account account;
        private Models.Context context;
        private string _username;
        private string _email;
        public ICommand SubmitPassword { get; set; }
        public ObservableCollection<AccountVM> Users { get; set; }

        public ForgottenPasswordVM()
        {
            this.account = new Models.Account();
            this.context = new Models.Context();

            SubmitPassword = new RelayCommand(askNewPassword);
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private void askNewPassword()
        {
            try
            {
                IEnumerable<Models.Account> account = context.Account;
                IEnumerable<AccountVM> AccountViewModel = account.Select(a => new AccountVM(a)).Where(p => p.UserName == _username && p.Email == _email);

                SendMail(AccountViewModel.First().Email, AccountViewModel);
                MessageBox.Show("Uw nieuwe wachtwoord is naar uw emailadres verzonden");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dit account bestaat niet");
            }
        }

        private void SendMail(string mail, IEnumerable<AccountVM> account)
        {
            string password = CreateRandomPassword(6);

            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("goinspgroepb@gmail.com", "GoInsp123");

                MailMessage mm = new MailMessage("goinspgroepb@gmail.com", mail, "Uw nieuwe wachtwoord", "Uw nieuwe wachtwoord is: " + password);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is iets fout gegaan probeer het later nog eens");
                return;
            }

            try
            {
                account.First().Password = password;
                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan probeer het later nog eens");
            }
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}