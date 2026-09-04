using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Registro : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "" ||
                txtCorreo.Text.Trim() == "" ||
                txtContrasena.Text.Trim() == "")
            {
                lblMensaje.Text = "Completa todos los campos.";
                lblMensaje.ForeColor = System.Drawing.Color.DarkRed;
                return;
            }

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string verificar =
                    "SELECT COUNT(*) FROM Usuarios WHERE Correo=@Correo";

                using (SqlCommand cmd = new SqlCommand(verificar, cn))
                {
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());

                    int existe = Convert.ToInt32(cmd.ExecuteScalar());

                    if (existe > 0)
                    {
                        lblMensaje.Text = "Ese correo ya está registrado.";
                        lblMensaje.ForeColor = System.Drawing.Color.DarkRed;
                        return;
                    }
                }

                string sql = @"INSERT INTO Usuarios
                               (Nombre, Correo, Contrasena)
                               VALUES
                               (@Nombre, @Correo, @Contrasena)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contrasena", txtContrasena.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("Login.aspx");
        }
    }
}