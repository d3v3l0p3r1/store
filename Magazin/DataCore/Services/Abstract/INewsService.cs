using System;
using System.Collections.Generic;
using System.Text;
using BaseCore.Entities;
using BaseCore.Services.Abstract;
using News = DataCore.Entities.News;

namespace DataCore.Services.Abstract
{
    public interface INewsService : IBaseService<News>
    {

    }
}
