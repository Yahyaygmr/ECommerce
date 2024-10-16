﻿using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Domain.Entities.Common;
using ECommerce.Api.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Context
{
    public class ECommerceApiDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ECommerceApiDbContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CFile> Cfiles { get; set; }
        DbSet<ProductImageFile> ProductImageFiles { get; set; }
        DbSet<InvoinceFile> InvoinceFiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    //EntityState.Deleted =>null
                    _ => DateTime.UtcNow

                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
