using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Facturacion : System.Web.UI.Page
    {
        string conexion =
            ConfigurationManager.ConnectionStrings["ExpressoDB"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CrearTabla();
                CargarProductos();

                if (ddlProducto.Items.Count > 0)
                {
                    MostrarProductoSeleccionado();
                }

                MostrarDetalle();
            }
        }


        private void CrearTabla()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdProducto", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Subtotal", typeof(decimal));
            dt.Columns.Add("Imagen", typeof(string));

            Session["DetalleFactura"] = dt;
        }


        private DataTable ObtenerDetalle()
        {
            DataTable dt =
                Session["DetalleFactura"] as DataTable;

            if (dt == null)
            {
                CrearTabla();

                dt =
                    Session["DetalleFactura"] as DataTable;
            }

            return dt;
        }


        private void CargarProductos()
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                string sql = @"
                    SELECT IdProducto, Nombre
                    FROM Productos
                    WHERE Estado = 1
                    ORDER BY Nombre";

                SqlDataAdapter da =
                    new SqlDataAdapter(sql, cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                ddlProducto.DataSource = dt;

                ddlProducto.DataTextField =
                    "Nombre";

                ddlProducto.DataValueField =
                    "IdProducto";

                ddlProducto.DataBind();
            }
        }


        protected void ddlProducto_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            MostrarProductoSeleccionado();
        }


        private void MostrarProductoSeleccionado()
        {
            if (ddlProducto.Items.Count == 0)
                return;

            int idProducto =
                Convert.ToInt32(
                    ddlProducto.SelectedValue);

            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"
                    SELECT Nombre, Precio, Imagen
                    FROM Productos
                    WHERE IdProducto = @IdProducto";

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@IdProducto",
                        idProducto);

                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string nombre =
                                dr["Nombre"].ToString();

                            decimal precio =
                                Convert.ToDecimal(
                                    dr["Precio"]);

                            string imagen =
                                dr["Imagen"].ToString();

                            lblNombreProducto.Text =
                                nombre;

                            lblPrecioProducto.Text =
                                "L. " +
                                precio.ToString("N2");

                            if (string.IsNullOrWhiteSpace(imagen))
                            {
                                imgProducto.ImageUrl =
                                    "Images/no-image.jpg";
                            }
                            else
                            {
                                imgProducto.ImageUrl =
                                    "Images/" + imagen;
                            }
                        }
                    }
                }
            }
        }


        protected void btnAgregar_Click(
            object sender,
            EventArgs e)
        {
            int cantidad;

            if (!int.TryParse(
                txtCantidad.Text,
                out cantidad) ||
                cantidad <= 0)
            {
                lblMensaje.Text =
                    "La cantidad debe ser un número mayor que 0.";

                lblMensaje.ForeColor =
                    System.Drawing.Color.DarkRed;

                return;
            }


            int idProducto =
                Convert.ToInt32(
                    ddlProducto.SelectedValue);


            DataTable detalle =
                ObtenerDetalle();


            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();

                string sql = @"
                    SELECT Nombre, Precio, Imagen
                    FROM Productos
                    WHERE IdProducto = @IdProducto";


                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@IdProducto",
                        idProducto);


                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string nombre =
                                dr["Nombre"].ToString();

                            decimal precio =
                                Convert.ToDecimal(
                                    dr["Precio"]);

                            string imagen =
                                dr["Imagen"].ToString();


                            DataRow filaExistente = null;

                            foreach (DataRow row
                                in detalle.Rows)
                            {
                                if (Convert.ToInt32(
                                    row["IdProducto"])
                                    == idProducto)
                                {
                                    filaExistente = row;
                                    break;
                                }
                            }


                            if (filaExistente != null)
                            {
                                int cantidadActual =
                                    Convert.ToInt32(
                                        filaExistente["Cantidad"]);

                                int nuevaCantidad =
                                    cantidadActual + cantidad;

                                filaExistente["Cantidad"] =
                                    nuevaCantidad;

                                filaExistente["Subtotal"] =
                                    precio * nuevaCantidad;
                            }
                            else
                            {
                                detalle.Rows.Add(
                                    idProducto,
                                    nombre,
                                    cantidad,
                                    precio,
                                    precio * cantidad,
                                    imagen);
                            }
                        }
                    }
                }
            }


            Session["DetalleFactura"] =
                detalle;

            MostrarDetalle();

            txtCantidad.Text = "1";

            lblMensaje.Text =
                "Producto agregado correctamente.";

            lblMensaje.ForeColor =
                System.Drawing.Color.DarkGreen;
        }


        private void MostrarDetalle()
        {
            DataTable detalle =
                ObtenerDetalle();

            gvDetalle.DataSource =
                detalle;

            gvDetalle.DataBind();


            decimal subtotal = 0;


            foreach (DataRow row
                in detalle.Rows)
            {
                subtotal +=
                    Convert.ToDecimal(
                        row["Subtotal"]);
            }


            decimal impuesto =
                subtotal * 0.15m;

            decimal total =
                subtotal + impuesto;


            lblSubtotal.Text =
                "L. " +
                subtotal.ToString("N2");

            lblImpuesto.Text =
                "L. " +
                impuesto.ToString("N2");

            lblTotal.Text =
                "L. " +
                total.ToString("N2");
        }


        protected void gvDetalle_RowCommand(
            object sender,
            System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int fila =
                Convert.ToInt32(
                    e.CommandArgument);

            DataTable detalle =
                ObtenerDetalle();

            if (fila < 0 ||
                fila >= detalle.Rows.Count)
            {
                return;
            }


            if (e.CommandName ==
                "EliminarProducto")
            {
                detalle.Rows.RemoveAt(fila);

                Session["DetalleFactura"] =
                    detalle;

                MostrarDetalle();
            }


            if (e.CommandName ==
                "EditarCantidad")
            {
                int cantidadActual =
                    Convert.ToInt32(
                        detalle.Rows[fila]["Cantidad"]);

                txtCantidad.Text =
                    cantidadActual.ToString();

                ddlProducto.SelectedValue =
                    detalle.Rows[fila]
                    ["IdProducto"]
                    .ToString();

                MostrarProductoSeleccionado();

                detalle.Rows.RemoveAt(fila);

                Session["DetalleFactura"] =
                    detalle;

                MostrarDetalle();

                lblMensaje.Text =
                    "Modifica la cantidad y presiona 'Agregar producto'.";
            }
        }


        protected void btnLimpiar_Click(
            object sender,
            EventArgs e)
        {
            CrearTabla();

            txtCliente.Text = "";

            txtCantidad.Text = "1";

            lblMensaje.Text =
                "Factura limpiada.";

            MostrarDetalle();
        }


        protected void btnGuardarFactura_Click(
            object sender,
            EventArgs e)
        {
            DataTable detalle =
                ObtenerDetalle();


            if (txtCliente.Text.Trim() == "")
            {
                lblMensaje.Text =
                    "Escribe el nombre del cliente.";

                lblMensaje.ForeColor =
                    System.Drawing.Color.DarkRed;

                return;
            }


            if (detalle.Rows.Count == 0)
            {
                lblMensaje.Text =
                    "Agrega al menos un producto.";

                lblMensaje.ForeColor =
                    System.Drawing.Color.DarkRed;

                return;
            }


            decimal subtotal = 0;


            foreach (DataRow row
                in detalle.Rows)
            {
                subtotal +=
                    Convert.ToDecimal(
                        row["Subtotal"]);
            }


            decimal impuesto =
                subtotal * 0.15m;

            decimal total =
                subtotal + impuesto;


            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();


                SqlTransaction trans =
                    cn.BeginTransaction();


                try
                {
                    string facturaSql = @"
                        INSERT INTO Facturas
                        (Cliente, Subtotal, Impuesto, Total)
                        OUTPUT INSERTED.IdFactura
                        VALUES
                        (@Cliente, @Subtotal,
                         @Impuesto, @Total)";


                    int idFactura;


                    using (SqlCommand cmd =
                        new SqlCommand(
                            facturaSql,
                            cn,
                            trans))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Cliente",
                            txtCliente.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Subtotal",
                            subtotal);

                        cmd.Parameters.AddWithValue(
                            "@Impuesto",
                            impuesto);

                        cmd.Parameters.AddWithValue(
                            "@Total",
                            total);


                        idFactura =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());
                    }


                    foreach (DataRow row
                        in detalle.Rows)
                    {
                        string detalleSql = @"
                            INSERT INTO DetalleFactura
                            (IdFactura, IdProducto,
                             Cantidad, Precio, Subtotal)
                            VALUES
                            (@IdFactura, @IdProducto,
                             @Cantidad, @Precio,
                             @Subtotal)";


                        using (SqlCommand cmd =
                            new SqlCommand(
                                detalleSql,
                                cn,
                                trans))
                        {
                            cmd.Parameters.AddWithValue(
                                "@IdFactura",
                                idFactura);

                            cmd.Parameters.AddWithValue(
                                "@IdProducto",
                                row["IdProducto"]);

                            cmd.Parameters.AddWithValue(
                                "@Cantidad",
                                row["Cantidad"]);

                            cmd.Parameters.AddWithValue(
                                "@Precio",
                                row["Precio"]);

                            cmd.Parameters.AddWithValue(
                                "@Subtotal",
                                row["Subtotal"]);

                            cmd.ExecuteNonQuery();
                        }
                    }


                    trans.Commit();


                    lblMensaje.Text =
                        "Factura #" +
                        idFactura +
                        " guardada correctamente.";

                    lblMensaje.ForeColor =
                        System.Drawing.Color.DarkGreen;


                    CrearTabla();

                    txtCliente.Text = "";

                    txtCantidad.Text = "1";

                    MostrarDetalle();
                }
                catch
                {
                    trans.Rollback();

                    lblMensaje.Text =
                        "Ocurrió un error al guardar la factura.";

                    lblMensaje.ForeColor =
                        System.Drawing.Color.DarkRed;
                }
            }
        }
    }
}