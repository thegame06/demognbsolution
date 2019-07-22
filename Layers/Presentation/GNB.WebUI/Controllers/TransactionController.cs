using GNB.ApplicationCore.DTOs;
using GNB.ApplicationCore.Exception;
using GNB.ApplicationCore.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GNB.WebUI.Controllers
{
    [CurrentException(Class = "ControllerException", NameSpace = "GNB.WebUI.Controllers", Assembly = "GNB.WebUI")]
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index(string searchString)
        {
            ViewBag.SearchString = String.IsNullOrEmpty(searchString) ? string.Empty : searchString;
            GetTransactionListDto trans = new GetTransactionListDto { BaseCurrency = "---" };

            IResult<string> result = this.UsingCatch(c => {

                var component = Infrastructure.Common.FactoryContainer.Instance.Container.Resolve<IGetTransactions>();

                ViewBag.Result = component.Execute(messageOut =>
                {
                    if (messageOut != null)
                    {
                        trans = messageOut;
                    }
                });

            });


            if (!result.Successfully)
                ViewBag.Result = result;


            if (!String.IsNullOrEmpty(searchString))
                trans.TransactionList = trans.TransactionList.Where(w => w.SKU == searchString).ToList();


            if (trans.TransactionList != null)
                trans.TotalAmount = trans.TransactionList.Where(w => w.Currency == trans.BaseCurrency).Sum(s => s.Amount);


            return View(trans);
        }
    }
}