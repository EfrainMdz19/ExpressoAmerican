<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Reportes.aspx.cs"
    Inherits="ExpressoAmerican.Reportes" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <div class="admin-box">

        <h1>📊 Reportes de ventas</h1>

        <div style="display:grid;
                    grid-template-columns:1fr 1fr 1fr;
                    gap:15px;">

            <div>

                <label>Cliente</label>

                <asp:TextBox
                    ID="txtBuscarCliente"
                    runat="server"
                    CssClass="input"
                    Placeholder="Buscar cliente">
                </asp:TextBox>

            </div>


            <div>

                <label>Fecha inicial</label>

                <asp:TextBox
                    ID="txtFechaInicio"
                    runat="server"
                    CssClass="input"
                    TextMode="Date">
                </asp:TextBox>

            </div>


            <div>

                <label>Fecha final</label>

                <asp:TextBox
                    ID="txtFechaFin"
                    runat="server"
                    CssClass="input"
                    TextMode="Date">
                </asp:TextBox>

            </div>

        </div>


        <asp:Button
            ID="btnBuscar"
            runat="server"
            Text="🔎 Buscar"
            CssClass="btn"
            OnClick="btnBuscar_Click" />


        <asp:Button
            ID="btnMostrarTodas"
            runat="server"
            Text="Mostrar todas"
            CssClass="btn btn-light"
            OnClick="btnMostrarTodas_Click" />


        <asp:GridView
            ID="gvFacturas"
            runat="server"
            CssClass="table"
            AutoGenerateColumns="False"
            DataKeyNames="IdFactura"
            OnRowCommand="gvFacturas_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="IdFactura"
                    HeaderText="Factura #" />

                <asp:BoundField
                    DataField="Fecha"
                    HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                <asp:BoundField
                    DataField="Cliente"
                    HeaderText="Cliente" />

                <asp:BoundField
                    DataField="Subtotal"
                    HeaderText="Subtotal"
                    DataFormatString="L. {0:N2}" />

                <asp:BoundField
                    DataField="Impuesto"
                    HeaderText="Impuesto"
                    DataFormatString="L. {0:N2}" />

                <asp:BoundField
                    DataField="Total"
                    HeaderText="Total"
                    DataFormatString="L. {0:N2}" />

                <asp:ButtonField
                    Text="✏ Editar"
                    CommandName="EditarFactura"
                    ButtonType="Button" />

                <asp:ButtonField
                    Text="🗑 Eliminar"
                    CommandName="EliminarFactura"
                    ButtonType="Button" />

            </Columns>

        </asp:GridView>


        <div class="factura-total">

            <h2>
                Total vendido:
                <asp:Label
                    ID="lblTotalVentas"
                    runat="server"
                    Text="L. 0.00" />
            </h2>

        </div>


        <button
            type="button"
            class="btn btn-light"
            onclick="window.print();">

            🖨 Imprimir reporte

        </button>

    </div>

</div>

</asp:Content>