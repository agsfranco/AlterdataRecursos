function validarComentario(source, args) {
    //if ($("#<%= tbComentarioVoto.ClientID %>").val().length > 0) {
    if ($("#tbComentarioVoto").val().length > 0) {
        var d = new Date(); //Hora UTC em milisegundos.
        var n = d.getTimezoneOffset(); //Diferença entre UTC e o TimeZone local em minutos.
        var x = d - (n * 60 * 1000); //Hora local em milisegundos considerando o timezone.
        $('#HfDateTimeCliente').attr("Value", x);

        args.IsValid = true;
    }
    else {
        args.IsValid = false;
    }
}