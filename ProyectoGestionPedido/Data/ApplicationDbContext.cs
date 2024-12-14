using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //CREANDO EL USER PARA AGREGAR LOS CAMPOS//
        public class Usuario: IdentityUser
        {
            public string nombre { get; set; }
       
            public string direccion { get; set; }
            public string celular { get; set; }

            public string TipoCliente { get; set; }
        }

        protected override void OnModelCreating(ModelBuilder builder)// al hacer esto tendremos que moficar
        {
            base.OnModelCreating(builder);
            builder.Entity<Usuario>(entityTypeBuilder =>
            {
                entityTypeBuilder.ToTable("AspNetUsers");
                entityTypeBuilder.Property(u => u.UserName).HasMaxLength(100).HasDefaultValue(0);
                entityTypeBuilder.Property(u => u.nombre).HasMaxLength(60);
                entityTypeBuilder.Property(u => u.direccion).HasMaxLength(60);
                entityTypeBuilder.Property(u => u.celular).HasMaxLength(9);
                entityTypeBuilder.Property(u => u.TipoCliente).HasMaxLength(60);


            });







        }


        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rutas> Rutas { get; set; }
        public virtual DbSet<MetodoTransporte> MetodoTransportes { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-86F1Q2M;User id=sa;Database=SISTEMAPEDIDO;" +
                "Password=123;MultipleActiveResultSets=True;Encrypt=False");
        }


    }

}
