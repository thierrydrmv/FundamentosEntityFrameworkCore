using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCore.Domain
{
    [Table("Clientes")]
    public class Cliente
    {
        // Data Annotations
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        // Nome da propriedade diferente do nome na tabela do banco de dados
        [Column("Phone")]
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
