<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InicialRecursos.aspx.cs" Inherits="Recursos.InicialRecursos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validarComentario(source,args)
        {
            if ($("#<%= tbComentarioVoto.ClientID %>").val().length > 0)
            {
                var d = new Date(); //Hora UTC em milisegundos.
                var n = d.getTimezoneOffset(); //Diferença entre UTC e o TimeZone local em minutos.
                var x = d - (n * 60 * 1000); //Hora local em milisegundos considerando o timezone.
                $('#<%= HfDateTimeCliente.ClientID %>').attr("Value", x);

                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    </script>
    <h1>
        Recursos</h1>
                    <asp:GridView ID="GridRecursos" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridRecursos_RowDataBound" OnRowCommand="GridRecursos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="Id" />
                            <asp:BoundField DataField="titulo" HeaderText="Título" />
                            <asp:BoundField DataField="comentario" HeaderText="Descrição" />
                            <asp:BoundField DataField="data_cadastro" HeaderText="Data" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                            <asp:BoundField DataField="qtde_votos" HeaderText="Votos" />
                            <asp:BoundField DataField="votos_usr_atual" HeaderText="Seus Votos" ></asp:BoundField>
                            <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lbtnVerVotos" Text="Visualizar" runat="server" CommandName= "VerVotos" CommandArgument='<%#Eval("id")%>'></asp:LinkButton></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField><ItemTemplate><asp:LinkButton ID="lbtnVotar" Text="Votar" runat="server" CommandName= "Votar" CommandArgument='<%#Eval("id")%>'></asp:LinkButton></ItemTemplate></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
    <br/><br/>  
    <asp:Panel ID="PanelVotarRecurso" runat="server" Visible="False" >
        <h2><asp:Label ID="lblTituloComentarioVoto" runat="server" Text="Insira seu comentário para votar no recurso: "></asp:Label><br /></h2>
        <h3><asp:Label ID="lblTituloComentarioRecurso" runat="server"></asp:Label></h3>        
        <asp:TextBox ID="tbComentarioVoto" runat="server" Height="90px" MaxLength="500" Rows="10" TextMode="MultiLine" Width="600px" CausesValidation="True"></asp:TextBox>
        <h3>
        <asp:CustomValidator CssClass="error" ID="CustomValidatorComentario" runat="server" ErrorMessage="O preenchimento do comentário é obrigatório!" ClientValidationFunction="validarComentario"></asp:CustomValidator>
        </h3>
        <asp:LinkButton ID="btnVotar" runat="server" OnClick="btnVotar_Click" Text="Votar" />&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="btnCancelarVoto" runat="server" CausesValidation="False" OnClick="btnCancelarVoto_Click" Text="Cancelar" />
    </asp:Panel>
    <asp:Panel ID="PanelVisualizarVotacoes" runat="server" Visible="False">
        <h2><asp:Label ID="lblVotacao" runat="server" Text="Label" Visible="False">Vota&ccedil;&otilde;es</asp:Label></h2>    
            <h3><asp:Label ID="lblTituloVotacoes" runat="server"></asp:Label></h3>
            <asp:GridView ID="GridVotos" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="usr" HeaderText="Usuário" />
                    <asp:BoundField DataField="comentario" HeaderText="Comentário" />
                    <asp:BoundField DataField="data_local" HeaderText="Data Central" />
                    <asp:BoundField DataField="data_remota" HeaderText="Data Usuário" />
                </Columns>
            </asp:GridView><br/>
        <asp:LinkButton ID="btnlVisualizarVotacoesFechar" runat="server" OnClick="btnlVisualizarVotacoesFechar_Click" Text="Fechar" />        
    </asp:Panel>
    <asp:HiddenField ID="HfVotar" runat="server" />
    <asp:HiddenField ID="HfExibirVotos" runat="server" />
    <asp:HiddenField ID="HfDateTimeCliente" runat="server" />
</asp:Content>
