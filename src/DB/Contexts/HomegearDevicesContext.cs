using Microsoft.EntityFrameworkCore;
using DB.Models;


namespace DB.Contexts
{
    public class HomegearDevicesContext: DbContext
    {
        public DbSet<LightSwitch> LightSwitches { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./HomegearDevices.db");
        }
    }
}
