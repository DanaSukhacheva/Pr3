using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfAppAutorisation.Models;


namespace WpfAppAutorisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmInformation.xaml
    /// </summary>
    public partial class EmInformation : Page
    {
        private Personal_data currentEmployee;
        private SoundEntities db;
        private string _userRole; 

        
        public EmInformation(Personal_data employees)
        {
            InitializeComponent();
            currentEmployee = employees; 
            DataContext = currentEmployee;
            db = new SoundEntities();
            
                LoadUserRole();

            

        }
        private void LoadUserRole()
        {
            cmbJob.ItemsSource = db.Jobtitles.ToList();
            //var employee = db.Employees.FirstOrDefault(e => e.ID_personal_data == currentEmployee.ID_personal_data);
            //
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee == null)
            {
                string surname = TBname.Text;
                string name = TBname.Text;
                string patronomic = TBpatronymic.Text;
                string gmail = TBgmail.Text;
                long jobtitle = cmbJob.SelectedIndex;
                var newemployee = new Personal_data
                {
                    ID_personal_data = db.Personal_data.Max(x => x.ID_personal_data) + 1,
                    name = name,
                    surname = surname,
                    patronymic = patronomic,
                    telephone = "5345345",
                    gmail = gmail,
                };
                var context = new ValidationContext(newemployee);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                StringBuilder sb = new StringBuilder();
                if (!Validator.TryValidateObject(newemployee, context, results, true))
                {
                    foreach (var error in results)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                    if (sb.Length > 0)
                    {
                        MessageBox.Show(sb.ToString());
                    }
                    return;
                }
                db.Personal_data.Add(newemployee);
                db.SaveChanges();
                var empl = new Employees
                {
                    ID_employee = db.Employees.Max(x => x.ID_employee) + 1,
                    ID_personal_data = newemployee.ID_personal_data,
                    ID_jobtitle = jobtitle

                };
                db.Employees.Add(empl);
                db.SaveChanges();
                MessageBox.Show("supersucssess");
                try
                {


                }
                catch
                {

                }

                if (currentEmployee != null)
                {
                    try
                    {
                        var employeeindb = db.Personal_data.Find(currentEmployee.ID_personal_data);
                        if (employeeindb != null)
                        {
                            employeeindb.surname = currentEmployee.surname;
                            employeeindb.name = currentEmployee.name;
                            employeeindb.patronymic = currentEmployee.patronymic;
                            employeeindb.gmail = currentEmployee.gmail;
                            db.SaveChanges();
                            MessageBox.Show("supersucssess");
                        }
                        else
                        {
                            MessageBox.Show("Nichego ne poluchilos");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TBgmail.Clear();
            TBname.Clear();
            TBpatronymic.Clear();
            TBgmail.Clear();
            TBsurname.Clear();
        }
    }
}
