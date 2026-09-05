using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpressoAmerican
{
    public partial class Reportes : System.Web.UI.Page
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
                CargarFacturas();
            }
        }


        private void CargarFacturas()
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                string sql = @"
                    SELECT
                        IdFactura,
                        Fecha,
                        Cliente,
                        Subtotal,
                        Impuesto,
                        Total
                    FROM Facturas
                    WHERE 1 = 1";


                if (txtBuscarCliente.Text.Trim() != "")
                {
                    sql +=
                        " AND Cliente LIKE @Cliente";
                }


                if (txtFechaInicio.Text != "")
                {
                    sql +=
                        " AND CAST(Fecha AS DATE) >= @FechaInicio";
                }


                if (txtFechaFin.Text != "")
                {
                    sql +=
                        " AND CAST(Fecha AS DATE) <= @FechaFin";
                }


                sql +=
                    " ORDER BY Fecha DESC";


                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    if (txtBuscarCliente.Text.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue(
                            "@Cliente",
                            "%" +
                            txtBuscarCliente.Text.Trim() +
                            "%");
                    }


                    if (txtFechaInicio.Text != "")
                    {
                        cmd.Parameters.AddWithValue(
                            "@FechaInicio",
                            DateTime.Parse(
                                txtFechaInicio.Text));
                    }


                    if (txtFechaFin.Text != "")
                    {
                        cmd.Parameters.AddWithValue(
                            "@FechaFin",
                            DateTime.Parse(
                                txtFechaFin.Text));
                    }


                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);


                    gvFacturas.DataSource =
                        dt;

                    gvFacturas.DataBind();


                    decimal total = 0;


                    foreach (DataRow row
                        in dt.Rows)
                    {
                        total +=
                            Convert.ToDecimal(
                                row["Total"]);
                    }


                    lblTotalVentas.Text =
                        "L. " +
                        total.ToString("N2");
                }
            }
        }


        protected void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            CargarFacturas();
        }


        protected void btnMostrarTodas_Click(
            object sender,
            EventArgs e)
        {
            txtBuscarCliente.Text = "";

            txtFechaInicio.Text = "";

            txtFechaFin.Text = "";

            CargarFacturas();
        }


        protected void gvFacturas_RowCommand(
            object sender,
            System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int fila =
                Convert.ToInt32(
                    e.CommandArgument);


            int idFactura =
                Convert.ToInt32(
                    gvFacturas.DataKeys[fila].Value);


            if (e.CommandName ==
                "EliminarFactura")
            {
                EliminarFactura(idFactura);

                CargarFacturas();
            }


            if (e.CommandName ==
                "EditarFactura")
            {
                Response.Redirect(
                    "EditarFactura.aspx?id=" +
                    idFactura);
            }
        }


        private void EliminarFactura(
            int idFactura)
        {
            using (SqlConnection cn =
                new SqlConnection(conexion))
            {
                cn.Open();


                SqlTransaction trans =
                    cn.BeginTransaction();


                try
                {
                    string sqlDetalle = @"
                        DELETE FROM DetalleFactura
                        WHERE IdFactura=@IdFactura";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlDetalle,
                            cn,
                            trans))
                    {
                        cmd.Parameters.AddWithValue(
                            "@IdFactura",
                            idFactura);

                        cmd.ExecuteNonQuery();
                    }


                    string sqlFactura = @"
                        DELETE FROM Facturas
                        WHERE IdFactura=@IdFactura";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlFactura,
                            cn,
                            trans))
                    {
                        cmd.Parameters.AddWithValue(
                            "@IdFactura",
                            idFactura);

                        cmd.ExecuteNonQuery();
                    }


                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();

                    throw;
                }
            }
        }
    }
}