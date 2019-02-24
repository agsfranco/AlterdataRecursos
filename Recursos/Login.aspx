<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Recursos.Login" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
    <asp:TextBox ID="tbEmail" runat="server" TextMode="Email"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="Senha"></asp:Label>
    <asp:TextBox ID="tbSenha" runat="server" TextMode="Password"></asp:TextBox>
    <asp:LinkButton ID="Button1" runat="server" Text="Entrar" OnClick="Button1_Click" /></br></br>
    <asp:Label ID="lblMsgErro" runat="server" Text="Email ou Senha incorretos." Visible="False"></asp:Label>
</asp:Content>
