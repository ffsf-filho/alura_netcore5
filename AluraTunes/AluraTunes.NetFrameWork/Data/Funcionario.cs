//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AluraTunes.NetFrameWork.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Funcionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Funcionario()
        {
            this.Clientes = new HashSet<Cliente>();
            this.Funcionario1 = new HashSet<Funcionario>();
        }
    
        public int FuncionarioId { get; set; }
        public string Sobrenome { get; set; }
        public string PrimeiroNome { get; set; }
        public string Titulo { get; set; }
        public Nullable<int> SeReportaA { get; set; }
        public Nullable<System.DateTime> DataNascimento { get; set; }
        public Nullable<System.DateTime> DataAdmissao { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionario> Funcionario1 { get; set; }
        public virtual Funcionario Funcionario2 { get; set; }
    }
}
