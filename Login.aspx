<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="ExpressoAmerican.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="form-box">

    <h1>Iniciar sesión</h1>

    <asp:Label ID="lblMensaje" runat="server"
        CssClass="message"></asp:Label>

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

    <asp:Button ID="btnLogin" runat="server"
        Text="Ingresar"
        CssClass="btn"
        OnClick="btnLogin_Click" />

    <br />

    <a href="Registro.aspx">
        Crear una cuenta
    </a>

</div>

</asp:Content>