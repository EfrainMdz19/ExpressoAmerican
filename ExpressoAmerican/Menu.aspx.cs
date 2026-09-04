using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Menu : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string sql;

                if (ddlCategoria.SelectedValue == "Todas")
                {
                    sql = @"SELECT *
                            FROM Productos
                            WHERE Estado=1";
                }
                else
                {
                    sql = @"SELECT *
                            FROM Productos
                            WHERE Estado=1
                            AND Categoria=@Categoria";
                }

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    if (ddlCategoria.SelectedValue != "Todas")
                    {
                        cmd.Parameters.AddWithValue(
                            "@Categoria",
                            ddlCategoria.SelectedValue);
                    }

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    rptProductos.DataSource = dt;
                    rptProductos.DataBind();
                }
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            CargarProductos();
        }
    }
}