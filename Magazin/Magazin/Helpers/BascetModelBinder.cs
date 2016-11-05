using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entities;

namespace Magazin.Helpers
{
    public class BascetModelBinder : IModelBinder
    {
        private const string sessionKey = "Bascet";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Bascet bascet = null;

            if (controllerContext.HttpContext.Session != null)
            {
                bascet = (Bascet) controllerContext.HttpContext.Session[sessionKey];
            }

            if (bascet == null)
            {
                bascet = new Bascet();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = bascet;
                }
            }
            
            return bascet;
        }
    }
}