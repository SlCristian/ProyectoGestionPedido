using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGestionPedido.Models
{
    public class Rutas
    {
        [Key]
        public int IdRuta { get; set; }
        public double distanciaKilomentro { get; set; }
        public double CostoTotal { get; set; }

        public int IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public virtual Pedido pedido { get; set; }
        public int IdMetodoTransporte { get; set; }
        [ForeignKey("IdMetodoTransporte")]
        public virtual MetodoTransporte MetodoTransporte { get; set; }

    }
}
