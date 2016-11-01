using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Data.Entities;
using Magazin.Models;

namespace Magazin.App_Start
{
    public class MapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<ProductCategory, ProductCategoryDTO>()
                    .ForMember(z => z.File, z => z.MapFrom(t => t.File.FileName ?? "no-image.png"));

                x.CreateMap<Product, ProductDTO>()
                    .ForMember(z => z.File, z => z.MapFrom(t => t.ProductFiles.FirstOrDefault(y => y.IsMain).File.FileName ?? "no-image.png"));
            });
        }
    }
}