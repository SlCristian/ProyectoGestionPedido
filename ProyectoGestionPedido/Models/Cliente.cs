using System.ComponentModel.DataAnnotations;

namespace ProyectoGestionPedido.Models
{
    public class Cliente
    {
     [Key]
     public string IdCliente {  get; set; }
     public string nombre { get; set; }
    public string correo { get; set; }
    public string direccion {  get; set; }
     public string celular { get; set; }
        //relacion//
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
