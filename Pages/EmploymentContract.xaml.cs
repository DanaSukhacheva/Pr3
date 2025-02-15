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
using Xceed.Words.NET;

namespace WpfAppAutorisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmploymentContract.xaml
    /// </summary>
    public partial class EmploymentContract : Page

    {
        private Personal_data _employee;
        public EmploymentContract(Personal_data employee)
        {
            InitializeComponent();
            _employee = employee;
            EmployeeNameTextBlock.Text = _employee.name + " " + _employee.surname;
            EmployeePositionTextBlock.Text = _employee.jobTitle;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.GoBack();
        }

        [Obsolete]
        private void CreateContractButton_Click(object sender, RoutedEventArgs e)
        {
            string contractNumber = ContractNumberTextBox.Text.Trim();
            string city = CityTextBox.Text.Trim();
            string contractDay = ContractDayTextBox.Text.Trim();
            string contractMonth = ContractMonthTextBox.Text.Trim();
            string contractYear = ContractYearTextBox.Text.Trim();
            string employerName = EmployerNameTextBox.Text.Trim();
            string directorName = DirectorNameTextBox.Text.Trim();
            string testPeriod = TestPeriodTextBox.Text.Trim();
            string employeeSalary = EmployeeSalaryTextBox.Text.Trim();
            var testPath = new Uri("Resources/blank-trudovogo-dogovora.docx", UriKind.Relative);
            string templatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "blank-trudovogo-dogovora.docx");
            string outputPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),$"Трудовой_договор_{_employee.surname}.docx");
            using (var document = DocX.Load(templatePath))
            {
                document.ReplaceText("{ContractNumber}", contractNumber);
                document.ReplaceText("{City}", city);
                document.ReplaceText("{ContractDay}", contractDay);
                document.ReplaceText("{ContractMonth}", contractMonth);
                document.ReplaceText("{ContractYear}", contractYear);
                document.ReplaceText("{EmployerName}", employerName);
                document.ReplaceText("{DirectorName}", directorName);
                document.ReplaceText("{EmployeeName}", _employee.name + " " + _employee.surname);
                document.ReplaceText("{EmployeePosition}", _employee.jobTitle);
                document.ReplaceText("{TestPeriod}", testPeriod);
                document.ReplaceText("{EmployeeSalary}", employeeSalary);

                document.SaveAs(outputPath);

            }
            MessageBox.Show($"Договор успешно создан и сохранен по пути:\n{outputPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void ContractNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
