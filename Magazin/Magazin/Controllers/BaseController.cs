using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Base.Services;
using Data.DAL;

namespace Magazin.Controllers
{
    public class BaseController : ApiController
    {
        protected IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}