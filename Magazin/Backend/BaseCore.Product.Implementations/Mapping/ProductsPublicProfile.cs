using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models.Public;

namespace BaseCore.Products.Implementations.Mapping
{
    public class ProductsPublicProfile : AutoMapper.Profile
    {
        public ProductsPublicProfile()
        {
            CreateMap<Product, ProductDetailDto>()
                .ForMember(x => x.Package, x => x.MapFrom(z => z.Package.Value))
                .ForMember(x => x.Files, x => x.MapFrom(z => z.Images.Select(c => c.FileId)));
        }
    }
}
