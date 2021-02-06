using Correios.Net;
using EmporioUP.Models;
using EmporioUP.CEPService;

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace EmporioUP.Controllers
{
    public class ClienteController : Controller
    {
        msdbEntities db = new msdbEntities();

        public ActionResult Clientes()
        {
            return View(db.Clientes.ToList());
        }

        public void Cliente_Atualiza(int id_cliente, String nome, String cpf, String cep,
            String uf, String cidade, String bairro, String logradouro, String numero, String complemento)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_cliente_atualiza @id_cliente, @nome, @cpf," +
                    "@cep, @uf, @cidade, @bairro, @logradouro, @numero, @complemento";

                cmd.Parameters.AddWithValue("@id_cliente", SqlDbType.Int).Value = id_cliente;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.AddWithValue("@cpf", SqlDbType.VarChar).Value = cpf;
                cmd.Parameters.AddWithValue("@cep", SqlDbType.Char).Value = cep;
                cmd.Parameters.AddWithValue("@uf", SqlDbType.Char).Value = uf;
                cmd.Parameters.AddWithValue("@cidade", SqlDbType.VarChar).Value = cidade;
                cmd.Parameters.AddWithValue("@bairro", SqlDbType.VarChar).Value = bairro;
                cmd.Parameters.AddWithValue("@logradouro", SqlDbType.VarChar).Value = logradouro;
                cmd.Parameters.AddWithValue("@numero", SqlDbType.VarChar).Value = numero;
                cmd.Parameters.AddWithValue("@complemento", SqlDbType.VarChar).Value = complemento;

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
                Response.Redirect("/Cliente/Clientes");
            }
        }

        public ActionResult ClientesEditar(int id)
        {
            Cliente clientes = db.Clientes.Single(func => func.id_cliente == id);
            return View(clientes);
        }

        public ActionResult ClientesDeletar(int id)
        {
            Cliente clientes = db.Clientes.Single(func => func.id_cliente == id);
            return View(clientes);
        }

        public void Cliente_Atualiza_2(int id_cliente, String nome, String cpf)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_cliente_atualiza_2 @id_cliente, @nome, @cpf";

                cmd.Parameters.AddWithValue("@id_cliente", SqlDbType.Int).Value = id_cliente;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.AddWithValue("@cpf", SqlDbType.VarChar).Value = cpf;

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
                Response.Redirect("/Cliente/Clientes");
            }
        }

        public void Cliente_Deleta(int id_cliente)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_cliente_deleta @id_cliente";

                cmd.Parameters.AddWithValue("@id_cliente", SqlDbType.Int).Value = id_cliente;

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
                Response.Redirect("/Cliente" +
                    "/Clientes");
            }
        }

        public ActionResult ClientesDetalhes(int id)
        {
            Cliente clientes = db.Clientes.Single(func => func.id_cliente == id);
            return View(clientes);
        }
    }

    
}