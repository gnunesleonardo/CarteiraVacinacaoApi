using CarteiraVacinacaoApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarteiraVacinacaoApi.Infrastructure.Persistence
{
    public class VaccineRecordDbContext(DbContextOptions<VaccineRecordDbContext> options) : DbContext(options)
    {
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<VaccineRecord> VaccineRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vaccine>(vaccineEntity =>
            {
                vaccineEntity.ToTable("Vaccine");
                
                vaccineEntity.HasKey(v => v.Id);
                vaccineEntity.Property(v => v.Id).ValueGeneratedOnAdd();

                vaccineEntity.Property(v => v.Name).IsRequired().HasMaxLength(128);
                vaccineEntity.Property(v => v.DosesRequired).IsRequired();
            });

            modelBuilder.Entity<Person>(personEntity =>
            {
                personEntity.ToTable("Person");
                personEntity.Property(p => p.Id).ValueGeneratedOnAdd();

                personEntity.Property(p => p.Name).IsRequired().HasMaxLength(128);
                personEntity.HasMany(p => p.VaccineRecords)
                    .WithOne(v => v.Person)
                    .HasForeignKey(v => v.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<VaccineRecord>(vaccineRecordEntity =>
            {
                vaccineRecordEntity.ToTable("VaccineRecord");
                vaccineRecordEntity.HasKey(vr => vr.Id);
                vaccineRecordEntity.Property(vr => vr.Id).ValueGeneratedOnAdd();

                vaccineRecordEntity.Property(vr => vr.DoseNumber).IsRequired();
                vaccineRecordEntity.Property(vr => vr.AppliedDate).IsRequired();

                vaccineRecordEntity.HasOne(vr => vr.Person)
                    .WithMany(p => p.VaccineRecords)
                    .HasForeignKey(vc => vc.PersonId);
            });

            modelBuilder.Entity<Vaccine>().HasData(
                new Vaccine { Id = 1, Name = "Covid-19", DosesRequired = 2 },
                new Vaccine { Id = 2, Name = "Febre Amarela", DosesRequired = 2 },
                new Vaccine { Id = 3, Name = "Gripe 2025", DosesRequired = 1 }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Leonardo Gomes" },
                new Person { Id = 2, Name = "Larissa Vancini" }
            );

            modelBuilder.Entity<VaccineRecord>().HasData(
                new VaccineRecord { Id = 1, PersonId = 1, VaccineId = 1, DoseNumber = 1, AppliedDate = new DateTime(2025, 02, 05) },
                new VaccineRecord { Id = 2, PersonId = 1, VaccineId = 1, DoseNumber = 2, AppliedDate = new DateTime(2025, 08, 05) },
                new VaccineRecord { Id = 3, PersonId = 2, VaccineId = 1, DoseNumber = 1, AppliedDate = new DateTime(2025, 02, 05) },
                new VaccineRecord { Id = 4, PersonId = 2, VaccineId = 1, DoseNumber = 2, AppliedDate = new DateTime(2025, 08, 05) },
                new VaccineRecord { Id = 5, PersonId = 1, VaccineId = 3, DoseNumber = 1, AppliedDate = new DateTime(2025, 08, 05) }
            );
        }
    }
}
