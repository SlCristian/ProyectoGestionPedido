using System.ComponentModel.DataAnnotations;

namespace ProyectoGestionPedido.Models
{
    public class MetodoTransporte
    {
        [Key]
     public int IdMetodoTransporte {  get; set; }
      public string Nombre {  get; set; }
      public double CostoKilometro {  get; set; }
      public virtual ICollection<Rutas> Rutas { get; set; }

    }
}
