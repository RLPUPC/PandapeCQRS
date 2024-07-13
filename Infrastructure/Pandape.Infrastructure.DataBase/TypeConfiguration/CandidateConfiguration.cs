using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandape.Infrastructure.Domain.Dto;

namespace Pandape.Infrastructure.DataBase;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>, IEntityConfiguration
{
    public void Addconfiguration(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfiguration(this);
    }

    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(e => new { e.IdCandidate });
        //builder.HasAlternateKey(e => e.Email); Esta comentado dado que esto crea que la variable sea solo readonly y obligaría a utilizar un query sql
        //Se crea un indice y se especifica que debe ser único, esto cumple la misma funcion que un alternative key
        //De esta forma podemos hacer un Update sin utilizar querys SQL
        builder.HasIndex(e => e.Email)
            .IsUnique();
        builder.Property(e => e.IdCandidate)
            .HasColumnName("IdCandidate")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Surname)
            .HasColumnName("Surname")
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(e => e.Birthdate)
            .HasColumnName("Birthdate")
            .IsRequired();
        builder.Property(e => e.Email)
            .HasColumnName("Email")
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(e => e.InsertDate)
            .HasColumnName("InsertDate")
            .IsRequired();
        builder.Property(e => e.ModifyDate)
            .HasColumnName("ModifyDate");

        builder.ToTable("Candidates");
    }
}
