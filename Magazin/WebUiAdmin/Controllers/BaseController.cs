using BaseCore.Services;
using BaseCore.Services.Abstract;
using DataCore.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{    
    public class BaseController : Controller
    {
        private readonly DataContext _dataContext;

        public BaseController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(_dataContext);
        }
    }
}