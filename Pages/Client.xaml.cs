using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        public Client()
        {
            InitializeComponent();
            var servise = SoundEntities.GetContext().Services.ToList();

            
            string filepath = "servise.txt";
            using(StreamWriter sw = new StreamWriter(filepath))
            {
                foreach (var entity in servise)
                {
                    sw.WriteLine($"{entity.name} - {entity.price} руб.");
                }
            }
           
            lViewServise.ItemsSource = servise;
        }

        private void PrintListButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = "servise.txt";
            if (!File.Exists(filepath))
            {
                MessageBox.Show("document not found");
                return;
            }
            FlowDocument doc = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            using(StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    paragraph.Inlines.Add(new Run(line));
                    paragraph.Inlines.Add(new LineBreak());
                }
            }
            doc.Blocks.Add(paragraph);
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Список сотрудников");
            }
        }
    }
}
