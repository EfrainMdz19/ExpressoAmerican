<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="EditarFactura.aspx.cs"
    Inherits="ExpressoAmerican.EditarFactura" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <div class="admin-box">

        <h1>✏ Editar factura</h1>

        <asp:Label
            ID="lblMensaje"
            runat="server"
            CssClass="message">
        </asp:Label>


        <h2>
            Factura #<asp:Label
                ID="lblIdFactura"
                runat="server" />
        </h2>


        <label>Cliente</label>

        <asp:TextBox
            ID="txtCliente"
            runat="server"
            CssClass="input">
        </asp:TextBox>


        <asp:GridView
    ID="gvDetalle"
    runat="server"
    CssClass="table"
    AutoGenerateColumns="False"
    DataKeyNames="IdProducto">

            <Columns>

                <asp:BoundField
                    DataField="Nombre"
                    HeaderText="Producto" />

                <asp:TemplateField
                    HeaderText="Cantidad">

                    <ItemTemplate>

                        <asp:TextBox
                            ID="txtCantidad"
                            runat="server"
                            CssClass="input"
                            Text='<%# Eval("Cantidad") %>'>
                        </asp:TextBox>

                    </ItemTemplate>

                </asp:TemplateField>


                <asp:BoundField
                    DataField="Precio"
                    HeaderText="Precio"
                    DataFormatString="L. {0:N2}" />

                <asp:BoundField
                    DataField="Subtotal"
                    HeaderText="Subtotal"
                    DataFormatString="L. {0:N2}" />

            </Columns>

        </asp:GridView>


        <asp:Button
            ID="btnGuardar"
            runat="server"
            Text="💾 Guardar cambios"
            CssClass="btn btn-success"
            OnClick="btnGuardar_Click" />


        <a href="Reportes.aspx"
           class="btn btn-light">

            ↩ Volver

        </a>

    </div>

</div>

</asp:Content>