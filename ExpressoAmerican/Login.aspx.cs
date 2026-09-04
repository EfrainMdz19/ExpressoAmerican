using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Login : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"SELECT IdUsuario, Nombre
                               FROM Usuarios
                               WHERE Correo=@Correo
                               AND Contrasena=@Contrasena";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contrasena", txtContrasena.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Session["IdUsuario"] = dr["IdUsuario"];
                            Session["NombreUsuario"] = dr["Nombre"].ToString();

                            Response.Redirect("Inicio.aspx");
                        }
                        else
                        {
                            lblMensaje.Text = "Correo o contraseña incorrectos.";
                            lblMensaje.ForeColor =
                                System.Drawing.Color.DarkRed;
                        }
                    }
                }
            }
        }
    }
}