using EmporioUP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmporioUP.Controllers
{
    public class VendaController : Controller
    {
        msdbEntities db = new msdbEntities();
        VendaViewModel vvm = new VendaViewModel();

        public ActionResult Vendas()
        {
            vvm.valor = null;
            vvm.qtde  = null;
            vvm.total = null;

            return View(db.Vendas.ToList());
        }

        [HttpGet]
        public void GetComponent()
        {

        }

        public ActionResult NovaVenda()
        {
            Venda venda = new Venda();
            Cliente cliente = new Cliente();
            Produto produto = new Produto();

            vvm.id_venda = venda.id_venda;
            vvm.qtde = venda.qtde;
            vvm.total = venda.total;
            vvm.dt_venda = venda.dt_venda;
            vvm.id_cliente = cliente.id_cliente;
            vvm.nome = cliente.nome;
            vvm.id_produto = produto.id_produto;
            vvm.descricao = produto.descricao;
            vvm.valor = produto.valor;

            ViewBag.ListaClie = new SelectList(db.Clientes, "id_cliente", "nome");
            ViewBag.ListaProd = new SelectList(db.Produtos, "id_produto", "descricao");
            ViewBag.ListaProdVal = new SelectList(db.Produtos, "id_produto", "valor");

            return View(vvm);
        }

        public ActionResult VendasDetalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        public ActionResult VendasDeletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        public ActionResult VendasEditar(int id)
        {
            Venda venda = db.Vendas.SingleOrDefault(x => x.id_venda == id);
            Cliente cliente = new Cliente();
            Produto produto = new Produto();

            vvm.id_venda = venda.id_venda;
            vvm.qtde = venda.qtde;
            vvm.total = venda.total;
            vvm.dt_venda = venda.dt_venda;
            vvm.id_cliente = cliente.id_cliente;
            vvm.nome = cliente.nome;
            vvm.id_produto = produto.id_produto;
            vvm.descricao = produto.descricao;
            vvm.valor = venda.valor;

            ViewBag.id_cliente = new SelectList(db.Clientes, "id_cliente", "nome", venda.fk_id_cliente);
            ViewBag.id_produto = new SelectList(db.Produtos, "id_produto", "descricao", venda.fk_id_produto);

            return View(vvm);
        }

        public void Venda_Atualiza(int id_venda, int id_cliente,
            int id_produto, decimal valor, int qtde, decimal total)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_venda_atualiza @id_venda, @fk_id_cliente," +
                    " @fk_id_produto, @valor, @qtde, @total";

                total = qtde * valor;

                cmd.Parameters.AddWithValue("@id_venda", SqlDbType.Int).Value = id_venda;
                cmd.Parameters.AddWithValue("@fk_id_cliente", SqlDbType.Int).Value = id_cliente;
                cmd.Parameters.AddWithValue("@fk_id_produto", SqlDbType.Int).Value = id_produto;
                cmd.Parameters.AddWithValue("@valor", SqlDbType.Decimal).Value = valor;
                cmd.Parameters.AddWithValue("@qtde", SqlDbType.Int).Value = qtde;
                cmd.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = total;

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
                Response.Redirect("/Venda/Vendas");
            }
        }

        public void Venda_Deleta(int id_venda)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_venda_deleta @id_venda";

                cmd.Parameters.AddWithValue("@id_venda", SqlDbType.Int).Value = id_venda;

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
                Response.Redirect("/Venda/Vendas");
            }
        }

    }
}