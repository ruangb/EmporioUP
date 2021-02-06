using EmporioUP.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace EmporioUP.Controllers
{
    public class FuncionarioController : Controller
    {
        msdbEntities db = new msdbEntities();

        public ActionResult Funcionarios()
        {
            return View(db.Funcionarios.ToList());
        }

        public ActionResult FuncionariosDetalhes(int id)
        {
            Funcionario funcionarios = db.Funcionarios.Single(func => func.id_funcionario == id);
            return View(funcionarios);
        }

        public ActionResult FuncionariosEditar(int id)
        {
            Funcionario funcionarios = db.Funcionarios.Single(func => func.id_funcionario == id);
            return View(funcionarios);
        }

        public ActionResult FuncionariosDeletar(int id)
        {
            Funcionario funcionarios = db.Funcionarios.Single(func => func.id_funcionario == id);
            return View(funcionarios);
        }

        public void Funcionario_Atualiza(int id_funcionario, String nome, String email, String senha)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_funcionario_atualiza @id_funcionario, @nome," +
                    "@email, @senha";
                cmd.Parameters.AddWithValue("@id_funcionario", SqlDbType.Int).Value = id_funcionario;
                cmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.AddWithValue("@senha", SqlDbType.VarChar).Value = senha;

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
                Response.Redirect("/Funcionario/Funcionarios");
            }
        }

        public void Funcionario_Deleta(int id_funcionario)
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=RUAN\\SQLEXPRESS;initial catalog=msdb;integrated security=True;MultipleActiveResultSets=True;");
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Execute p_funcionario_deleta @id_funcionario";

                cmd.Parameters.AddWithValue("@id_funcionario", SqlDbType.Int).Value = id_funcionario;

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
                Response.Redirect("/Funcionario/Funcionarios");
            }
        }

    }
}