using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppAutorisation.Models;
using WpfAppAutorisation.Pages;

namespace WpfAppAutorisation
{
    internal class EmpData
    {
        Employees emp;
        Personal_data data { get; set; }

        public EmpData(Employees emp, Personal_data data) 
        { 
            this.emp = emp;
            this.data = data;
        }


    }
}
