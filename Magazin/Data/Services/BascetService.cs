﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Services;
using Data.Entities;

namespace Data.Services
{
    public interface IBascetService
    {
        void AddItem(Bascet bascet, Product product, int count);
        void RemoveItem(Bascet bascet, int productId);
    }

    public class BascetService : IBascetService
    {
        public BascetService()
        {

        }

        public void AddItem(Bascet bascet, Product product, int count)
        {
            if (bascet.Products.Any(x => x.Product.Id == product.Id))
            {
                var bascetItem = bascet.Products.First(x => x.Product.Id == product.Id);
                bascetItem.Count += count;
            }
            else
            {
                bascet.Products.Add(new BascetProduct() { Count = count, Product = product });
            }
        }

        public void RemoveItem(Bascet bascet, int productId)
        {
            var products = bascet.Products.Where(x => x.Product.Id == productId).ToList();
            if (products.Any())
            {
                foreach (var bascetProduct in products)
                {
                    bascet.Products.Remove(bascetProduct);
                }
            }
        }
    }
}
