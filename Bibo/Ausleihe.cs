//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bibo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ausleihe
    {
        public int Id { get; set; }
        public int NutzerId { get; set; }
        public int BuchId { get; set; }
        public System.DateTime Von { get; set; }
        public System.DateTime Bis { get; set; }
        public Nullable<System.DateTime> Rueckgabe { get; set; }
    }
}
