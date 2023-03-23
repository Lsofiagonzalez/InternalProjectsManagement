namespace AppRecordatorio.Models
{
    using Core.Models.User;
    using System.Data.Entity;

    public class Context : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'Contexto' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'tmkhogares.Models.Contexto' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'Contexto'  en el archivo de configuración de la aplicación.
        public Context()
            : base("name=Context")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        #region TABLAS BASE

        public DbSet<Users> Users { get; set; }
        public DbSet<Rols> Rols { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Histories> Histories { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Groups> Groups { get; set; }

        public DbSet<Users_Groups> Users_Groups { get; set; }

        #endregion

    }

}