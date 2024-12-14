using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGestionPedido.Models
{
    public class Pedido
    {
        [Key]

        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; }
        //relaciones//
        //relacionar la tabla users//
        //personaliuzar tsadas//
        public string IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }   
        public virtual ICollection<DetallePedido> Detalle { get; set; }
        public virtual ICollection<Rutas> Rutas { get; set; }

    }
}
