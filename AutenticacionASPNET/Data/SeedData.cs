using AutenticacionASPNET.Models;

namespace AutenticacionASPNET.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Verificar si ya hay usuarios
            if (context.Usuarios.Any())
            {
                return; // La BD ya tiene datos
            }

            // Crear usuarios de prueba
            var usuarios = new Usuario[]
            {
                new Usuario
                {
                    Email = "admin@test.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    NombreCompleto = "Administrador Sistema",
                    FechaNacimiento = new DateTime(1995, 1, 1),
                    Rol = "Admin",
                    Activo = true
                },
                new Usuario
                {
                    Email = "menor@test.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Menor123!"),
                    NombreCompleto = "Juan Menor",
                    FechaNacimiento = DateTime.Now.AddYears(-15),
                    Rol = "Usuario",
                    Activo = true
                },
                new Usuario
                {
                    Email = "mayor@test.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Mayor123!"),
                    NombreCompleto = "María Mayor",
                    FechaNacimiento = DateTime.Now.AddYears(-25),
                    Rol = "Usuario",
                    Activo = true
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            // Crear productos de prueba
            if (!context.Productos.Any())
            {
                var productos = new Producto[]
                {
                    new Producto
                    {
                        Nombre = "Laptop HP",
                        Descripcion = "Laptop HP 15.6\" Intel Core i5",
                        Precio = 799.99m,
                        Stock = 10,
                        Activo = true
                    },
                    new Producto
                    {
                        Nombre = "Mouse Logitech",
                        Descripcion = "Mouse inalámbrico ergonómico",
                        Precio = 19.99m,
                        Stock = 50,
                        Activo = true
                    },
                    new Producto
                    {
                        Nombre = "Teclado Mecánico",
                        Descripcion = "Teclado mecánico RGB con switches Blue",
                        Precio = 89.99m,
                        Stock = 30,
                        Activo = true
                    },
                    new Producto
                    {
                        Nombre = "Monitor Samsung",
                        Descripcion = "Monitor 24\" Full HD IPS",
                        Precio = 199.99m,
                        Stock = 15,
                        Activo = true
                    },
                    new Producto
                    {
                        Nombre = "Audífonos Sony",
                        Descripcion = "Audífonos inalámbricos con cancelación de ruido",
                        Precio = 149.99m,
                        Stock = 25,
                        Activo = true
                    }
                };

                context.Productos.AddRange(productos);
                context.SaveChanges();
            }
        }
    }
}
