<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Menu.aspx.cs"
    Inherits="ExpressoAmerican.Menu" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <h1 class="section-title">
        Nuestro Menú
    </h1>

    <div style="text-align:center; margin-bottom:25px;">

        <asp:DropDownList ID="ddlCategoria"
            runat="server"
            CssClass="input"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">

            <asp:ListItem Text="Todas las categorías"
                Value="Todas" />

            <asp:ListItem Text="Café"
                Value="Café" />

            <asp:ListItem Text="Bebidas Frías"
                Value="Bebidas Frías" />

            <asp:ListItem Text="Bebidas Calientes"
                Value="Bebidas Calientes" />

            <asp:ListItem Text="Postres"
                Value="Postres" />

        </asp:DropDownList>

    </div>

    <div class="cards">

        <asp:Repeater ID="rptProductos"
            runat="server">

            <ItemTemplate>

                <div class="card">

                    <img src='<%# "Images/" + Eval("Imagen") %>'
                         alt="Producto" />

                    <div class="card-content">

                        <h3>
                            <%# Eval("Nombre") %>
                        </h3>

                        <p>
                            <%# Eval("Descripcion") %>
                        </p>

                        <div class="price">
                            L. <%# Eval("Precio", "{0:N2}") %>
                        </div>

                    </div>

                </div>

            </ItemTemplate>

        </asp:Repeater>

    </div>

</div>

</asp:Content>