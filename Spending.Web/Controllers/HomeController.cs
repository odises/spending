using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spending.Data;
using Spending.Data.DataModel;

namespace Spending.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string order)
        {
            var entityContext = new EntityContext();
            var userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(order))
            {
                if (order == "terminal")
                {
                    return View(entityContext.Transactions.Where(p => p.TerminalId != null && p.UserId == userId).OrderByDescending(p => p.TerminalId).ToList());
                }
            }

            return View(entityContext.Transactions.Where(p => p.UserId == userId).OrderByDescending(p => p.CreationDateTime).ToList());
        }


        public ActionResult UpdateTerminal()
        {
            var entityContext = new EntityContext();

            var regex = new Regex("[0-9]{8}");
            foreach (var transaction in entityContext.Transactions.ToList())
            {
                var match = regex.Match(transaction.Description);
                if (match.Success)
                {
                    var terminal = match.Value;
                    transaction.TerminalId = terminal;
                }
            }

            entityContext.SaveChanges();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return View();
            }
            else
            {
                var result = ParseDocument(file.InputStream);

                var entityContext = new EntityContext();

                var userId = User.Identity.GetUserId();

                foreach (var transaction in result)
                {
                    if (!entityContext.Transactions.Any(p => p.DocumentId == transaction.DocumentId && p.UserId == userId))
                    {
                        transaction.UserId = userId;
                        entityContext.Transactions.Add(transaction);
                    }
                }

                entityContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public JsonResult Categories()
        {
            var entities = new EntityContext();
            var categories = entities.Categories.Select(p => new { text = p.Title, value = p.Id });

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Comment(string name, string value, int pk)
        {
            var entities = new EntityContext();
            var transaction = entities.Transactions.FirstOrDefault(p => p.Id == pk);
            transaction.Comment = value;
            entities.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Category(string name, string value, int pk)
        {
            var entities = new EntityContext();
            var transaction = entities.Transactions.FirstOrDefault(p => p.Id == pk);
            transaction.CategoryId = int.Parse(value);
            entities.SaveChanges();
            return Json("test", JsonRequestBehavior.AllowGet);
        }


        protected virtual List<Transaction> ParseDocument(Stream stream)
        {
            var transactions = new List<Transaction>();
            var i = 4;
            try
            {
                using (var reader = Excel.ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    var dataset = reader.AsDataSet();
                    var table = dataset.Tables[0];
                    for (; i < table.Rows.Count; i++)
                    {
                        var row = table.Rows[i];
                        if (!string.IsNullOrEmpty(row.ItemArray[2].ToString()))
                        {
                            var datePart = row[0].ToString();
                            var timePart = row[1].ToString();
                            var datePartSplitted = datePart.Split(new[] { '/' }, StringSplitOptions.None);
                            var timePartSplitted = timePart.Split(new[] { ':' }, StringSplitOptions.None);
                            var tempDate = new DateTime(int.Parse(datePartSplitted[0]),
                                int.Parse(datePartSplitted[1]),
                                int.Parse(datePartSplitted[2]),
                                new PersianCalendar());
                            var datetime = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day,
                                int.Parse(timePartSplitted[0]), int.Parse(timePartSplitted[1]), 0);

                            var transaction = new Transaction
                            {
                                Balance = long.Parse(row[7].ToString().Replace(",", string.Empty)),
                                CreationDateTime = datetime,
                                DepositValue = row[5].ToString() == "-" ? 0 : long.Parse(row[5].ToString().Replace(",", string.Empty)),
                                WithdrawValue = row[6].ToString() == "-" ? 0 : long.Parse(row[6].ToString().Replace(",", string.Empty)),
                                Description = row[4].ToString(),
                                DocumentId = int.Parse(row[2].ToString())
                            };

                            var regex = new Regex("[0-9]{8}");
                            var match = regex.Match(transaction.Description);
                            if (match.Success)
                            {
                                var terminal = match.Value;
                                transaction.TerminalId = terminal;
                            }

                            transactions.Add(transaction);
                        }
                    }

                    return transactions;
                }
            }
            catch (Exception)
            {
                throw new Exception(i.ToString());
            }
        }
    }
}