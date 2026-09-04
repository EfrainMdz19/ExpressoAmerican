<%@ Page Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="AccesoAdmin.aspx.cs"
    Inherits="ExpressoAmerican.AccesoAdmin" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<div class="form-box">

    <h1>🔒 Administración</h1>

    <p style="text-align:center;">
        Ingresa las credenciales de administrador.
    </p>

    <asp:Label ID="lblMensaje"
        runat="server"
        CssClass="message">
    </asp:Label>

    <asp:TextBox ID="txtUsuario"
        runat="server"
        CssClass="input"
        Placeholder="Usuario">
    </asp:TextBox>

    <asp:TextBox ID="txtContrasena"
        runat="server"
        CssClass="input"
        Placeholder="Contraseña"
        TextMode="Password">
    </asp:TextBox>

    <asp:Button ID="btnIngresar"
        runat="server"
        Text="Entrar"
        CssClass="btn"
        OnClick="btnIngresar_Click" />

</div>

</asp:Content>