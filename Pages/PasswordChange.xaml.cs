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
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Policy;
using WpfAppAutorisation.Models;

namespace WpfAppAutorisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Page
    {
        private string cod = "";
        public string email = "daniilsukhachev@yandex.ru";
        private Models.Authorization _user;
        public PasswordChange(Models.Authorization user)
        {
            InitializeComponent();
            _user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cod = GenerateCode();
            //SendEmail(email, cod, receive, btcom).GetAwaiter();
            SendMail(email, cod, receive, btcom);
            
            
        }
        private static async Task SendEmail(string email, string cod, Button btn1, Button btn2)
        {
            
            MailAddress from = new MailAddress("danasuhacheva@yandex.ru", "Dana");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Test";
            m.Body = $"Testing sending code: " + cod;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("danasuhacheva@yandex.ru", "gykbgrmnzertqhfd");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            MessageBox.Show("Email sent");
            btn1.Visibility = Visibility.Hidden;
            btn2.Visibility = Visibility.Visible;

        }

        private static void SendMail(string email, string cod, Button btn1, Button btn2)
        {

            MailAddress from = new MailAddress("danasuhacheva@yandex.ru", "Dana");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Test";
            m.Body = $"Testing sending code: " + cod;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("danasuhacheva@yandex.ru", "gykbgrmnzertqhfd");
            
            smtp.Send(m);
            MessageBox.Show("Email sent");
            btn1.Visibility = Visibility.Hidden;
            btn2.Visibility = Visibility.Visible;
        }

        private void changeBtn()
        {
            receive.Visibility = Visibility.Hidden;
            btcom.Visibility = Visibility.Visible;
        }
        private string GenerateCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            return code.ToString();
        }

        private void tbcomfim_Click(object sender, RoutedEventArgs e)
        {
            SoundEntities db = SoundEntities.GetContext();
           
            string pass1 = tbrepeatpass.Text.Trim();
            string pass2 = tbnewpass.Text.Trim();
            if (pass1 == pass2)
            {
                _user.password = Services.Hash.HashPassword(pass2);
                
                db.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                NavigationService.Navigate(new Autho());
            }

        }

        private void btcom_Click(object sender, RoutedEventArgs e)
        {
            
            string code = tbcode.Text;
            if (code == cod)
            {
                tbcomfim.Visibility = Visibility.Visible;
                tbnewpass.Visibility = Visibility.Visible;
                tbrepeatpass.Visibility = Visibility.Visible;
                newpass.Visibility = Visibility.Visible;
                repeat.Visibility = Visibility.Visible;
            }
        }
    }
}
