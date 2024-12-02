using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Contexte de base de données spécifique à l'application Atelier Onirium. 
    /// Il hérite de DbContext et intègre une interface IAtelierOniriumDBContext pour permettre une injection de dépendance.
    /// </summary>
public class AtelierOniriumContext : IdentityDbContext<User>, IAtelierOniriumDBContext
{
    public AtelierOniriumContext(DbContextOptions<AtelierOniriumContext> options)
        : base(options)
    {
    }

    public DbSet<Creation> Creations { get; set; }
    public DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Applique la longueur maximale de 191 caractères à toutes les colonnes de type string
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string))
                {
                    property.SetMaxLength(191);
                }
            }
        }

        builder.Entity<IdentityUser>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(128);  
            entity.Property(e => e.UserName).HasMaxLength(191);  
            entity.Property(e => e.NormalizedUserName).HasMaxLength(191);  
            entity.Property(e => e.Email).HasMaxLength(191);  
            entity.Property(e => e.NormalizedEmail).HasMaxLength(191);  
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(128);  
            entity.Property(e => e.Name).HasMaxLength(191);  
            entity.Property(e => e.NormalizedName).HasMaxLength(191);  
        });

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole {  Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole {  Name = "Admin", NormalizedName = "ADMIN" }
            );
    }
}}