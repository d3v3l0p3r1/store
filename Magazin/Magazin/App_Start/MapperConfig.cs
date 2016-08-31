using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                x.CreateMap<ProductCategory, ProductCategoryDTO>();
            });
        }
    }
}