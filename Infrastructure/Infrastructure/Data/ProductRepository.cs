﻿using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext context) : IProductRepository
    {
        public void AddProduct(Product product)
        {
           context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await context.Products.Select(x => x.Brand)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            var query = context.Products.AsQueryable();
            if(!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(x => x.Brand == brand);
            }
            if(!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.Type == type);
            }
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Price)
            };
            return await query.ToListAsync(); // skip and take for pagination
            //return await context.Products.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
           return await context.Products.Select(x =>x.Type)
                .Distinct().
                ToListAsync();
        }

        public bool ProductExist(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            // Tell entity framework to modify the product
            context.Entry(product).State = EntityState.Modified;
        }
    }
}
