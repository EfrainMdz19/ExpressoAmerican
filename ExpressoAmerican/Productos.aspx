<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Productos.aspx.cs"
    Inherits="ExpressoAmerican.Productos" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <div class="admin-box">

        <h1>☕ Administrar productos</h1>

        <asp:Label ID="lblMensaje"
            runat="server"
            CssClass="message">
        </asp:Label>

        <asp:HiddenField
            ID="hfIdProducto"
            runat="server" />

        <label>Nombre</label>

        <asp:TextBox ID="txtNombre"
            runat="server"
            CssClass="input">
        </asp:TextBox>


        <label>Categoría</label>

        <asp:DropDownList ID="ddlCategoria"
            runat="server"
            CssClass="input">

            <asp:ListItem>Café</asp:ListItem>
            <asp:ListItem>Bebidas Frías</asp:ListItem>
            <asp:ListItem>Bebidas Calientes</asp:ListItem>
            <asp:ListItem>Postres</asp:ListItem>

        </asp:DropDownList>


        <label>Descripción</label>

        <asp:TextBox ID="txtDescripcion"
            runat="server"
            CssClass="input"
            TextMode="MultiLine">
        </asp:TextBox>


        <label>Precio</label>

        <asp:TextBox ID="txtPrecio"
            runat="server"
            CssClass="input">
        </asp:TextBox>


        <label>Nombre de imagen</label>

        <asp:TextBox ID="txtImagen"
            runat="server"
            CssClass="input"
            Placeholder="ejemplo.jpg">
        </asp:TextBox>


        <asp:CheckBox ID="chkDestacado"
            runat="server"
            Text=" Producto destacado" />

        <br /><br />

        <asp:CheckBox ID="chkEstado"
            runat="server"
            Text=" Producto activo"
            Checked="true" />

        <br /><br />


        <asp:Button ID="btnGuardar"
            runat="server"
            Text="Guardar"
            CssClass="btn btn-success"
            OnClick="btnGuardar_Click" />

        <asp:Button ID="btnLimpiar"
            runat="server"
            Text="Limpiar"
            CssClass="btn btn-light"
            OnClick="btnLimpiar_Click" />


        <hr />

        <h2>Buscar producto</h2>

        <asp:TextBox ID="txtBuscar"
            runat="server"
            CssClass="input"
            Placeholder="Escribe el nombre del producto">
        </asp:TextBox>

        <asp:Button ID="btnBuscar"
            runat="server"
            Text="Buscar"
            CssClass="btn"
            OnClick="btnBuscar_Click" />


   <asp:GridView ID="gvProductos"
    runat="server"
    CssClass="table"
    AutoGenerateColumns="False"
    DataKeyNames="IdProducto"
    OnRowCommand="gvProductos_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="IdProducto"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="Nombre"
                    HeaderText="Producto" />

                <asp:BoundField
                    DataField="Categoria"
                    HeaderText="Categoría" />

                <asp:BoundField
                    DataField="Precio"
                    HeaderText="Precio"
                    DataFormatString="L. {0:N2}" />

                <asp:BoundField
                    DataField="Imagen"
                    HeaderText="Imagen" />

                <asp:CheckBoxField
                    DataField="Destacado"
                    HeaderText="Destacado" />

                <asp:CheckBoxField
                    DataField="Estado"
                    HeaderText="Activo" />

                <asp:ButtonField
                    Text="Editar"
                    CommandName="EditarProducto"
                    ButtonType="Button" />

                <asp:ButtonField
                    Text="Eliminar"
                    CommandName="EliminarProducto"
                    ButtonType="Button" />

            </Columns>

        </asp:GridView>

    </div>

</div>

</asp:Content>