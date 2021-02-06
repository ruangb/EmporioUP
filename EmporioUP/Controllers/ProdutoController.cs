using EmporioUP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmporioUP.Controllers
{
    public class ProdutoController : Controller
    {
        msdbEntities db = new msdbEntities();

        [HttpGet]
        public ActionResult NovoProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoProduto(Produto prod)
        {
            List<Produto> imgList = new List<Produto>();

            List<Produto> listProduto = db.Produtos.ToList();

            int maxId = (from item in listProduto
                           orderby item.id_produto descending
                           select item.id_produto).First();

            if (maxId == 0)
                maxId = 1;
            else
                maxId++;

            Produto produto = new Produto();
            produto = prod;

            prod.id_produto = maxId;
            string fileName = Path.GetFileNameWithoutExtension(produto.ImgFile.FileName);
            string extension = Path.GetExtension(produto.ImgFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            produto.img = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            prod.ImgFile.SaveAs(fileName);
            using (msdbEntities db = new msdbEntities())
            {
                db.Produtos.Add(prod);
                db.SaveChanges();
            }

            ModelState.Clear();

            return RedirectToAction("Produtos");
        }

        [HttpGet]
        public ActionResult ProdutosDetalhes(int id)
        {
            Produto prod = new Produto();
            prod = db.Produtos.Where(x => x.id_produto == id).FirstOrDefault();
            return View(prod);
        }

        public ActionResult Produtos()
        {
            return View(db.Produtos.ToList());
        }

        public ActionResult ProdutosDeletar(int id)
        {
            Produto prod = new Produto();
            prod = db.Produtos.Where(x => x.id_produto == id).FirstOrDefault();
            return View(prod);
        }

        public void Produto_Deleta(int id_produto)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_produto_deleta @id_produto";

                cmd.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = id_produto;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
            finally
            {
                Response.Redirect("/Produto/Produtos");
            }
        }

        public ActionResult ProdutosEditar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProdutosEditar([Bind(Include = "id_produto, descricao, valor, info, categoria, img")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Produtos");
            }
            return View(produto);
        }

    }
}