using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace ProyectoGestionPedido.Models
{
    public class Producto
    {
        [Key]
        [Display(Name ="IdProducto")]
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int stock { get; set; }
        //relacionar con una tabla//
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }


    }
}
