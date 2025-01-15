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
using WpfAppAutorisation.Models;

namespace WpfAppAutorisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {
        public EmployeeList()
        {
            InitializeComponent();
            var db = SoundEntities.GetContext();
            var emps = db.Employees.ToList();
            List<EmpData> empDatas = new List<EmpData>();
            List<Personal_data> employees = new List<Personal_data>();
            foreach (var emp in emps) 
            {
                EmpData empData = new EmpData(emp, emp.Personal_data);
                empDatas.Add(empData);
                employees.Add(emp.Personal_data);
            }

            
            lViewEmployees.ItemsSource = employees;
          
            
        }

        private void lViewEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Personal_data employees = btn.DataContext as Personal_data;
            NavigationService.Navigate(new EmInformation(employees));
        }

        private void Button_newempl_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmInformation(null));
        }
    }
}
