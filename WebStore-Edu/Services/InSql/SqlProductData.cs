﻿using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain;
using WebStore_Edu.Domain.Entityes;
using WebStore_Edu.Services.Interfaces;

namespace WebStore_Edu.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDb _Db;

        public SqlProductData(WebStoreDb Db) => _Db = Db;

        public IEnumerable<Section> GetSections() => _Db.Sections;

        public IEnumerable<Brand> GetBrands() => _Db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter? filter = null)
        {
            IQueryable<Product> query = _Db.Products;

            if (filter?.SectionId is { } section)
            {
                query = query.Where(p => p.SectionId == section);
            }
            if (filter?.BrandId is { } brand)
            {
                query = query.Where(p => p.BrandId == brand);
            }

            return query;

        }
    }
}