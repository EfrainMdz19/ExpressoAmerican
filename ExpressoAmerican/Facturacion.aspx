<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Facturacion.aspx.cs"
    Inherits="ExpressoAmerican.Facturacion" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <div class="admin-box">

        <h1>🧾 Facturación</h1>

        <asp:Label ID="lblMensaje"
            runat="server"
            CssClass="message">
        </asp:Label>

        <label>Cliente</label>

        <asp:TextBox ID="txtCliente"
            runat="server"
            CssClass="input"
            Placeholder="Nombre del cliente">
        </asp:TextBox>


        <div style="display:grid;
                    grid-template-columns:1fr 1fr;
                    gap:30px;
                    align-items:start;">

            <div>

                <label>Seleccionar bebida</label>

                <asp:DropDownList
                    ID="ddlProducto"
                    runat="server"
                    CssClass="input"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                </asp:DropDownList>


                <label>Cantidad</label>

                <asp:TextBox
                    ID="txtCantidad"
                    runat="server"
                    CssClass="input"
                    Text="1">
                </asp:TextBox>


                <asp:Button
                    ID="btnAgregar"
                    runat="server"
                    Text="➕ Agregar producto"
                    CssClass="btn"
                    OnClick="btnAgregar_Click" />

            </div>


            <div style="text-align:center;">

                <asp:Image
                    ID="imgProducto"
                    runat="server"
                    Width="250px"
                    Height="200px"
                    ImageUrl="Images/no-image.jpg"
                    Style="object-fit:cover;
                           border-radius:15px;
                           box-shadow:0 4px 15px rgba(0,0,0,.15);" />

                <h3>
                    <asp:Label
                        ID="lblNombreProducto"
                        runat="server"
                        Text="Selecciona un producto">
                    </asp:Label>
                </h3>

                <div class="price">

                    <asp:Label
                        ID="lblPrecioProducto"
                        runat="server"
                        Text="L. 0.00">
                    </asp:Label>

                </div>

            </div>

        </div>


        <hr style="margin:30px 0;" />


        <h2>Productos de la factura</h2>


        <asp:GridView
            ID="gvDetalle"
            runat="server"
            CssClass="table"
            AutoGenerateColumns="False"
            DataKeyNames="IdProducto"
            OnRowCommand="gvDetalle_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="Nombre"
                    HeaderText="Producto" />

                <asp:BoundField
                    DataField="Cantidad"
                    HeaderText="Cantidad" />

                <asp:BoundField
                    DataField="Precio"
                    HeaderText="Precio"
                    DataFormatString="L. {0:N2}" />

                <asp:BoundField
                    DataField="Subtotal"
                    HeaderText="Subtotal"
                    DataFormatString="L. {0:N2}" />

                <asp:ButtonField
                    Text="✏ Editar"
                    CommandName="EditarCantidad"
                    ButtonType="Button" />

                <asp:ButtonField
                    Text="🗑 Eliminar"
                    CommandName="EliminarProducto"
                    ButtonType="Button" />

            </Columns>

        </asp:GridView>


        <div class="factura-total">

            <p>
                Subtotal:
                <asp:Label
                    ID="lblSubtotal"
                    runat="server"
                    Text="L. 0.00" />
            </p>

            <p>
                Impuesto:
                <asp:Label
                    ID="lblImpuesto"
                    runat="server"
                    Text="L. 0.00" />
            </p>

            <h2>
                Total:
                <asp:Label
                    ID="lblTotal"
                    runat="server"
                    Text="L. 0.00" />
            </h2>

        </div>


        <asp:Button
            ID="btnGuardarFactura"
            runat="server"
            Text="💾 Guardar factura"
            CssClass="btn btn-success"
            OnClick="btnGuardarFactura_Click" />


        <asp:Button
            ID="btnLimpiar"
            runat="server"
            Text="🧹 Limpiar factura"
            CssClass="btn btn-light"
            OnClick="btnLimpiar_Click" />


        <button
            type="button"
            class="btn btn-light"
            onclick="window.print();">

            🖨 Imprimir

        </button>

    </div>

</div>

</asp:Content>