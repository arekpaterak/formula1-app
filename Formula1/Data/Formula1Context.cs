using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Formula1.Models;

namespace Formula1.Data
{
    public class Formula1Context : DbContext
    {
        public Formula1Context(DbContextOptions<Formula1Context> options)
            : base(options)
        {
        }

        public DbSet<Formula1.Models.CircuitModel> CircuitModel { get; set; } = default!;
        public DbSet<Formula1.Models.DriverModel> DriverModel { get; set; } = default!;
        public DbSet<Formula1.Models.TeamModel> TeamModel { get; set; } = default!;
        public DbSet<Formula1.Models.RaceModel> RaceModel { get; set; } = default!;
        public DbSet<Formula1.Models.RaceResultsModel> RaceResultsModel { get; set; } = default!;
        public DbSet<Formula1.Models.UserModel> UserModel { get; set; } = default!;

    }
}
