﻿using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.DAL.Implementations.Entities;
using BaseCore.Products.Abstractions.Models;

namespace BaseCore.Products.Implementations.Mapping
{
    public class BrandsProfile : AutoMapper.Profile
    {
        public BrandsProfile()
        {
            CreateMap<Brand, BrandDetailDto>();
        }
    }
}
