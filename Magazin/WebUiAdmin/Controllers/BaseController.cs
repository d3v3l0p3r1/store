using BaseCore.Services;
using DataCore.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebUiAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {
        protected IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}