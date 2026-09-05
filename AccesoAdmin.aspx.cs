using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class AccesoAdmin : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"SELECT IdAdministrador
                               FROM Administradores
                               WHERE Usuario=@Usuario
                               AND Contrasena=@Contrasena";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Usuario",
                        txtUsuario.Text.Trim());

                    cmd.Parameters.AddWithValue(
                        "@Contrasena",
                        txtContrasena.Text);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        Session["Administrador"] = true;

                        Response.Redirect("Productos.aspx");
                    }
                    else
                    {
                        lblMensaje.Text =
                            "Usuario o contraseña incorrectos.";

                        lblMensaje.ForeColor =
                            System.Drawing.Color.DarkRed;
                    }
                }
            }
        }
    }
}