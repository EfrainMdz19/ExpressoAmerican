using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Inicio : System.Web.UI.Page
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

                CargarDestacados();
            }
        }

        private void CargarDestacados()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string sql = @"SELECT IdProducto, Nombre, Categoria,
                                      Descripcion, Precio, Imagen
                               FROM Productos
                               WHERE Destacado=1
                               AND Estado=1";

                SqlDataAdapter da =
                    new SqlDataAdapter(sql, cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                rptDestacados.DataSource = dt;
                rptDestacados.DataBind();
            }
        }
    }
}