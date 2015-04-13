using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MCS.LRMS.Framework.Web.MVC
{

    public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;

        protected static readonly string Success_Key = string.Format("{0}_{1}", typeof(ModelStateTempDataTransfer).FullName, "Success");
    }


    public class ExportModelStateToTempData : ModelStateTempDataTransfer
    {
        private string _successMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportModelStateToTempData"/> class.
        /// </summary>
        /// <param name="SucessMessage">The sucess message.</param>
        public ExportModelStateToTempData(string SuccessMessage)
        {
            this._successMessage = SuccessMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        public ExportModelStateToTempData()
        {

        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Only export when ModelState is not valid
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                //Export if we are redirecting
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                {
                    filterContext.Controller.TempData[Key] = filterContext.Controller.ViewData.ModelState;
                }
            }
            else
            {
                //Export the success message if we are redirecting
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult))
                {
                    if (!string.IsNullOrEmpty(this._successMessage))
                    {
                        filterContext.Controller.TempData[Success_Key] = this._successMessage;
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class ImportModelStateFromTempData : ModelStateTempDataTransfer
    {


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ModelStateDictionary modelState = filterContext.Controller.TempData[Key] as ModelStateDictionary;

            if (modelState != null)
            {
                //Only Import if we are viewing
                if (filterContext.Result is ViewResult)
                {
                    filterContext.Controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    //Otherwise remove it.
                    filterContext.Controller.TempData.Remove(Key);
                }
            }

            string successMessage = filterContext.Controller.TempData[Success_Key] as string;

            if (!string.IsNullOrEmpty(successMessage))
            {
                if (filterContext.Controller.ViewData.Model != null /*&&
                    filterContext.Controller.ViewData.Model is BaseViewModel*/)
                {
                    //((BaseViewModel)filterContext.Controller.ViewData.Model).SuccessMessage = successMessage;
                    filterContext.Controller.TempData.Remove(Success_Key);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
