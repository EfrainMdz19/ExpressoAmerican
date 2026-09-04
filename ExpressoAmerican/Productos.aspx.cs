using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ExpressoAmerican
{
    public partial class Productos : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Administrador"] == null)
            {
                Response.Redirect("AccesoAdmin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string sql = @"SELECT *
                               FROM Productos
                               ORDER BY IdProducto DESC";

                SqlDataAdapter da =
                    new SqlDataAdapter(sql, cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvProductos.DataSource = dt;
                gvProductos.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal precio;

            if (!decimal.TryParse(
                txtPrecio.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out precio))
            {
                lblMensaje.Text = "Escribe un precio válido.";
                lblMensaje.ForeColor =
                    System.Drawing.Color.DarkRed;

                return;
            }

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                if (string.IsNullOrEmpty(hfIdProducto.Value))
                {
                    string sql = @"INSERT INTO Productos
                                   (Nombre, Categoria, Descripcion,
                                    Precio, Imagen, Destacado, Estado)
                                   VALUES
                                   (@Nombre, @Categoria, @Descripcion,
                                    @Precio, @Imagen, @Destacado, @Estado)";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd, precio);

                        cmd.ExecuteNonQuery();
                    }

                    lblMensaje.Text =
                        "Producto agregado correctamente.";
                }
                else
                {
                    string sql = @"UPDATE Productos
                                   SET Nombre=@Nombre,
                                       Categoria=@Categoria,
                                       Descripcion=@Descripcion,
                                       Precio=@Precio,
                                       Imagen=@Imagen,
                                       Destacado=@Destacado,
                                       Estado=@Estado
                                   WHERE IdProducto=@IdProducto";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd, precio);

                        cmd.Parameters.AddWithValue(
                            "@IdProducto",
                            hfIdProducto.Value);

                        cmd.ExecuteNonQuery();
                    }

                    lblMensaje.Text =
                        "Producto actualizado correctamente.";
                }
            }

            Limpiar();
            CargarProductos();
        }

        private void AgregarParametros(
            SqlCommand cmd,
            decimal precio)
        {
            cmd.Parameters.AddWithValue(
                "@Nombre",
                txtNombre.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@Categoria",
                ddlCategoria.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@Descripcion",
                txtDescripcion.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@Precio",
                precio);

            cmd.Parameters.AddWithValue(
                "@Imagen",
                txtImagen.Text.Trim());

            cmd.Parameters.AddWithValue(
                "@Destacado",
                chkDestacado.Checked);

            cmd.Parameters.AddWithValue(
                "@Estado",
                chkEstado.Checked);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            hfIdProducto.Value = "";

            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtImagen.Text = "";

            chkDestacado.Checked = false;
            chkEstado.Checked = true;

            btnGuardar.Text = "Guardar";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                string sql = @"SELECT *
                               FROM Productos
                               WHERE Nombre LIKE @Buscar
                               ORDER BY IdProducto DESC";

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Buscar",
                        "%" + txtBuscar.Text.Trim() + "%");

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    gvProductos.DataSource = dt;
                    gvProductos.DataBind();
                }
            }
        }

        protected void gvProductos_RowCommand(
            object sender,
            System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int fila =
                Convert.ToInt32(e.CommandArgument);

            int id =
                Convert.ToInt32(
                    gvProductos.DataKeys[fila].Value);

            if (e.CommandName == "EditarProducto")
            {
                CargarProducto(id);
            }

            if (e.CommandName == "EliminarProducto")
            {
                EliminarProducto(id);
            }
        }

        private void CargarProducto(int id)
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();

                string sql =
                    "SELECT * FROM Productos WHERE IdProducto=@Id";

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            hfIdProducto.Value =
                                dr["IdProducto"].ToString();

                            txtNombre.Text =
                                dr["Nombre"].ToString();

                            ddlCategoria.SelectedValue =
                                dr["Categoria"].ToString();

                            txtDescripcion.Text =
                                dr["Descripcion"].ToString();

                            txtPrecio.Text =
                                dr["Precio"].ToString();

                            txtImagen.Text =
                                dr["Imagen"].ToString();

                            chkDestacado.Checked =
                                Convert.ToBoolean(
                                    dr["Destacado"]);

                            chkEstado.Checked =
                                Convert.ToBoolean(
                                    dr["Estado"]);

                            btnGuardar.Text =
                                "Actualizar";
                        }
                    }
                }
            }
        }

        private void EliminarProducto(int id)
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();

                string sql =
                    "DELETE FROM Productos WHERE IdProducto=@Id";

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }

            CargarProductos();
        }
    }
}