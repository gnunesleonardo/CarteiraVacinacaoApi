using CarteiraVacinacaoApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraVacinacaoApi.Infrastructure.Persistence
{
    public class VaccineRecordDbContext(DbContextOptions<VaccineRecordDbContext> options) : DbContext(options)
    {
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<VaccineRecord> VacinneRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vaccine>(vaccineEntity =>
            {
                vaccineEntity.ToTable("Vaccine");
                
                vaccineEntity.HasKey(v => v.Id);
                vaccineEntity.Property(v => v.Id).ValueGeneratedOnAdd();

                vaccineEntity.Property(v => v.Name).IsRequired().HasMaxLength(128);
                vaccineEntity.Property(v => v.DosesRequired).HasDefaultValue(null);
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
        }
    }
}
