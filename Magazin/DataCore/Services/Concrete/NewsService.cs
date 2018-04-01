using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using BaseCore.Services.Concrete;
using DataCore.Services.Abstract;
using News = DataCore.Entities.News;

namespace DataCore.Services.Concrete
{
    public class NewsService : BaseService<News> , INewsService
    {
        public NewsService(IRepository<News> repository) : base(repository)
        {
        }
    }
}
