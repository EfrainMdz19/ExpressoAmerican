using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class EditarFactura : System.Web.UI.Page
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
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("Reportes.aspx");
                    return;
                }


                int idFactura =
                    Convert.ToInt32(
                        Request.QueryString["id"]);


                CargarFactura(idFactura);
            }
        }


        private void CargarFactura(
            int idFactura)
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();


                string sqlFactura = @"
                    SELECT IdFactura, Cliente
                    FROM Facturas
                    WHERE IdFactura=@IdFactura";


                using (SqlCommand cmd =
                    new SqlCommand(
                        sqlFactura,
                        cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@IdFactura",
                        idFactura);


                    using (SqlDataReader dr =
                        cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            lblIdFactura.Text =
                                dr["IdFactura"].ToString();

                            txtCliente.Text =
                                dr["Cliente"].ToString();
                        }
                    }
                }


                CargarDetalle(idFactura);
            }
        }


        private void CargarDetalle(
            int idFactura)
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                string sql = @"
                    SELECT
                        DF.IdProducto,
                        P.Nombre,
                        DF.Cantidad,
                        DF.Precio,
                        DF.Subtotal
                    FROM DetalleFactura DF
                    INNER JOIN Productos P
                        ON DF.IdProducto = P.IdProducto
                    WHERE DF.IdFactura=@IdFactura";


                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@IdFactura",
                        idFactura);


                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);


                    gvDetalle.DataSource =
                        dt;

                    gvDetalle.DataBind();
                }
            }
        }


        protected void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            int idFactura =
                Convert.ToInt32(
                    Request.QueryString["id"]);


            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();


                SqlTransaction trans =
                    cn.BeginTransaction();


                try
                {
                    decimal subtotal = 0;


                    foreach (
                        System.Web.UI.WebControls.GridViewRow row
                        in gvDetalle.Rows)
                    {
                        int idProducto =
                            Convert.ToInt32(
                                gvDetalle.DataKeys[
                                    row.RowIndex].Value);


                        System.Web.UI.WebControls.TextBox txtCantidad =
                            row.FindControl(
                                "txtCantidad")
                            as System.Web.UI.WebControls.TextBox;


                        int cantidad;


                        if (!int.TryParse(
                            txtCantidad.Text,
                            out cantidad) ||
                            cantidad <= 0)
                        {
                            lblMensaje.Text =
                                "Todas las cantidades deben ser válidas.";

                            trans.Rollback();

                            return;
                        }


                        decimal precio = 0;


                        string precioSql = @"
                            SELECT Precio
                            FROM Productos
                            WHERE IdProducto=@IdProducto";


                        using (SqlCommand cmd =
                            new SqlCommand(
                                precioSql,
                                cn,
                                trans))
                        {
                            cmd.Parameters.AddWithValue(
                                "@IdProducto",
                                idProducto);

                            precio =
                                Convert.ToDecimal(
                                    cmd.ExecuteScalar());
                        }


                        decimal subtotalProducto =
                            precio * cantidad;


                        subtotal +=
                            subtotalProducto;


                        string updateSql = @"
                            UPDATE DetalleFactura
                            SET Cantidad=@Cantidad,
                                Precio=@Precio,
                                Subtotal=@Subtotal
                            WHERE IdFactura=@IdFactura
                            AND IdProducto=@IdProducto";


                        using (SqlCommand cmd =
                            new SqlCommand(
                                updateSql,
                                cn,
                                trans))
                        {
                            cmd.Parameters.AddWithValue(
                                "@Cantidad",
                                cantidad);

                            cmd.Parameters.AddWithValue(
                                "@Precio",
                                precio);

                            cmd.Parameters.AddWithValue(
                                "@Subtotal",
                                subtotalProducto);

                            cmd.Parameters.AddWithValue(
                                "@IdFactura",
                                idFactura);

                            cmd.Parameters.AddWithValue(
                                "@IdProducto",
                                idProducto);

                            cmd.ExecuteNonQuery();
                        }
                    }


                    decimal impuesto =
                        subtotal * 0.15m;


                    decimal total =
                        subtotal + impuesto;


                    string facturaSql = @"
                        UPDATE Facturas
                        SET Cliente=@Cliente,
                            Subtotal=@Subtotal,
                            Impuesto=@Impuesto,
                            Total=@Total
                        WHERE IdFactura=@IdFactura";


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

                        cmd.Parameters.AddWithValue(
                            "@IdFactura",
                            idFactura);

                        cmd.ExecuteNonQuery();
                    }


                    trans.Commit();


                    Response.Redirect(
                        "Reportes.aspx");
                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    {
                        // Evita otro error si la transacción ya fue cerrada
                    }

                    lblMensaje.Text = "ERROR: " + ex.Message;
                }
            }
        }
    }
}