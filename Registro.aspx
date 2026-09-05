<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Registro.aspx.cs"
    Inherits="ExpressoAmerican.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="form-box">

    <h1>Crear cuenta</h1>

    <p style="text-align:center;">
        Regístrate para comenzar a utilizar Expresso Americano.
    </p>

    <asp:Label ID="lblMensaje" runat="server"
        CssClass="message"></asp:Label>

    <asp:TextBox ID="txtNombre" runat="server"
        CssClass="input"
        Placeholder="Nombre completo">
    </asp:TextBox>

    <asp:TextBox ID="txtCorreo" runat="server"
        CssClass="input"
        Placeholder="Correo electrónico"
        TextMode="Email">
    </asp:TextBox>

    <asp:TextBox ID="txtContrasena" runat="server"
        CssClass="input"
        Placeholder="Contraseña"
        TextMode="Password">
    </asp:TextBox>

    <asp:Button ID="btnRegistrar" runat="server"
        Text="Crear cuenta"
        CssClass="btn"
        OnClick="btnRegistrar_Click" />

    <br />

    <a href="Login.aspx">
        Ya tengo una cuenta
    </a>

</div>

</asp:Content>