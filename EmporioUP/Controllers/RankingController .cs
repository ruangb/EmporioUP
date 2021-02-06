using EmporioUP.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace EmporioUP.Controllers
{
    public class RankingController : Controller
    {
        msdbEntities db = new msdbEntities();

        public ActionResult Ranking()
        {
            var newList = db.Vendas.ToList();

            //double? total = newList.Sum(x => x.fk_id_produto.Value);

            var teste = newList.GroupBy(x => x.fk_id_produto);

            newList = (from item in newList
                       orderby item.fk_id_produto descending
                       select item).ToList();

            //newList = (from item in newList
            //          orderby item.dt_venda descending
            //          select item).ToList();

            return View(teste);
        }

    }
}