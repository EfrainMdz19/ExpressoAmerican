<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs"
    Inherits="ExpressoAmerican.Inicio" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="container">

    <section class="hero">

        <div>

            <h1>Expresso Americano</h1>

            <p>
                El sabor que te acompaña todos los días.
            </p>

            <a href="Menu.aspx" class="btn btn-light">
                ☕ Ver nuestro menú
            </a>

        </div>

    </section>


    <h2 class="section-title">
        Productos destacados
    </h2>

    <div class="cards">

        <asp:Repeater ID="rptDestacados" runat="server">

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

<a href="AccesoAdmin.aspx" class="admin-small">
    ⚙ Administrar
</a>

</asp:Content>