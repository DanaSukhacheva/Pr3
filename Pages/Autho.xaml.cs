using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
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
using System.Windows.Threading;
using WpfAppAutorisation.Models;
using WpfAppAutorisation.Services;

namespace WpfAppAutorisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private int click;       //jkj
        private static int failedAttempts;
        private DispatcherTimer lockTimer;
        private int lockTimeRemaining;
        public Autho()
        {
            InitializeComponent(); //
            click = 0;             //
            failedAttempts = 0;
            lockTimer = new DispatcherTimer();
            lockTimer.Interval = TimeSpan.FromSeconds(1);
            lockTimer.Tick += LockTimer_Tick;
            ClearFields();   //
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)   //
        {
            NavigationService.Navigate(new Client());
        }

        private void GenerateCaptcha()
        {
            txtBoxCaptcha.Visibility = Visibility.Visible;
            txtBlockCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            txtBlockCaptcha.Text = capctchaText;
            txtBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)   ///
        {
            click += 1;
            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Text.Trim();
            if(password == string.Empty || login == string.Empty) 
            {
                MessageBox.Show("Enter your data");
                return;
            }

            SoundEntities db = SoundEntities.GetContext();

            string hash = Hash.HashPassword(password);

            var user = db.Authorization.Where(x => x.login == login && x.password == hash).FirstOrDefault();
            if(click == 1)
            {
                if (user != null)
                {
                    if (user.Employees != null && !IsWorkingHours())
                    {
                        MessageBox.Show("Access denied. Working hours are from 10:00 to 19:00.");
                        return;
                    }

                    MessageBox.Show("You enter as: " + user.login.ToString());
                    LoadPage(user);

                }
                else
                {
                    
                      MessageBox.Show("Enter your data again");
                        GenerateCaptcha();
                    
                }
                
            }

            else if(click <=  2)
            {
               
                string enteredCaptcha = txtBoxCaptcha.Text.Trim();
                if (enteredCaptcha == txtBlockCaptcha.Text)
                {
                    if (user != null)
                    {
                        MessageBox.Show("You enter as: " + user.login.ToString());
                        LoadPage(user);
                    }
                    else
                    {
                        MessageBox.Show("Enter your data again");
                        GenerateCaptcha();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Invalid captcha. Try again.");
                    GenerateCaptcha(); 
                }
            }
            else
            {
                LockControls();

            }
        }
        private void LoadPage(Authorization user)
        {
            SoundEntities db = SoundEntities.GetContext();
            var emp = db.Employees.Where(x => x.ID_authorization == user.ID_authorization).FirstOrDefault();
            if (emp != null) 
            {
                
                var jobTitle = db.Jobtitles.Where(x=>x.ID_jobtitle == emp.ID_jobtitle).FirstOrDefault();
                if (jobTitle != null)
                {
                    string jobTitl = jobTitle.title.ToString();
                    switch (jobTitl)
                    {
                        case "менеджер студии":
                            NavigationService.Navigate(new Employee(emp, jobTitl, GetGreeting(emp)));
                            break;
                        case "помощник звукорежиссера":
                            NavigationService.Navigate(new Employee(emp, jobTitl, GetGreeting(emp)));
                            break;
                        case "администратор":
                            NavigationService.Navigate(new EmployeeList());
                            break;
                        default:
                            MessageBox.Show("Unknown role. Access denied.");
                            break;


                    }
                }
            }
            var producers = db.Producers.Where(x => x.ID_authorization == user.ID_authorization).FirstOrDefault();
            if (producers != null)
            {
                NavigationService.Navigate(new Produser(producers, GetGreeting(producers)));
            }
         
            
            click = 0;
            ClearFields();
        }

        private void ClearFields()
        {
            txtbLogin.Text = string.Empty;
            pswbPassword.Text = string.Empty;
            txtBoxCaptcha.Text = string.Empty;
            txtBoxCaptcha.Visibility = Visibility.Hidden;
            txtBlockCaptcha.Visibility = Visibility.Hidden;
        }
        private void LockControls()
        {
            lockTimeRemaining = 10;
            txtBlockTimeRemaining.Text = $"Too many failed attempts. Try again in {lockTimeRemaining} seconds.";
            txtBlockTimeRemaining.Visibility = Visibility.Visible;

            btnEnter.IsEnabled = false;
            btnEnterGuest.IsEnabled = false;
            txtbLogin.IsEnabled = false;
            pswbPassword.IsEnabled = false;
            txtBoxCaptcha.IsEnabled = false;

            lockTimer.Start();
        }
        private void LockTimer_Tick(object sender, EventArgs e)
        {
            lockTimeRemaining--;
            txtBlockTimeRemaining.Text = $"Too many failed attempts. Try again in {lockTimeRemaining} seconds.";

            if (lockTimeRemaining <= 0)
            {
                lockTimer.Stop();
                txtBlockTimeRemaining.Visibility = Visibility.Collapsed;
                btnEnter.IsEnabled = true;
                btnEnterGuest.IsEnabled = true;
                txtbLogin.IsEnabled = true;
                pswbPassword.IsEnabled = true;
                txtBoxCaptcha.IsEnabled = true;
               
            }
        }
        private string GetGreeting(Employees emp)
        {
            SoundEntities db = SoundEntities.GetContext();
            var persdata = db.Personal_data.Where(x => x.ID_personal_data ==  emp.ID_personal_data).FirstOrDefault();
            if (persdata != null)
            {
                string fullName = $"{persdata.surname} {persdata.name} {persdata.patronymic}";
                string timeOfDay = GetTimeOfDay();
                return $"{timeOfDay} {fullName}";
            }
            return null;
        }
        private string GetGreeting(Producers prod)
        {
            SoundEntities db = SoundEntities.GetContext();
            var proddata  = db.Personal_data.Where(p => p.ID_personal_data == prod.ID_personal_data).FirstOrDefault(); 
            if (proddata != null) 
            { 
                string fullNameProd = $"{proddata.surname} {proddata.name} {proddata.patronymic}";
                string timeOfDay = GetTimeOfDay();
                return $"{timeOfDay} {fullNameProd}";
            }
            return null;
        }
        private string GetTimeOfDay()
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 10 && hour < 12)
            {
                return "Добрый Утро";
            }
            else if (hour >= 12 && hour < 17)
            {
                return "Добрый День";
            }
            else if (hour >= 17 && hour < 19)
            {
                return "Добрый Вечер";
            }
            else
            {
                return "Доброй ночи";
            }
        }
        private bool IsWorkingHours()
        {
            int hour = DateTime.Now.Hour;
            return hour >= 10 && hour < 19;
        }
    }
}

