//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppAutorisation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.WindowsRuntime;

    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.Orders_Servises = new HashSet<Orders_Servises>();
        }
    
        public long ID_employee { get; set; }
        public long ID_personal_data { get; set; }
        public long ID_jobtitle { get; set; }
        public Nullable<long> ID_authorization { get; set; }
    
        public virtual Authorization Authorization { get; set; }
        public virtual Jobtitles Jobtitles { get; set; }
        public virtual Personal_data Personal_data { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders_Servises> Orders_Servises { get; set; }

        public string RoleFromList 
        {
            get {

                return "hfhgfhfg";
            }
        }
    }
}
