using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGestionPedido.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public int Cantidad { get; set; }
        public double subtotal     { get; set; }
        public int IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public virtual  Pedido Pedido { get; set; }
        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
       public virtual Producto Producto { get; set; }

    }
}
