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
    
    public partial class Recordings
    {
        public long ID_recording { get; set; }
        public string title { get; set; }
        public long ID_album { get; set; }
        public long ID_produser { get; set; }
        public long ID_singer { get; set; }
        public System.DateTime recording_date { get; set; }
        public string recording_link { get; set; }
    
        public virtual Albums Albums { get; set; }
        public virtual Producers Producers { get; set; }
        public virtual Singers Singers { get; set; }
    }
}
