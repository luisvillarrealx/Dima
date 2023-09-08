function AgendarCita() {
    let correo = $("#asunto").val();
    let correo = $("#nombre").val();
    let correo = $("#fecha").val();
    let correo = $("#departamento").val();
    let correo = $("#psicologo").val();
    let correo = $("#numero").val();

    $.ajax({
        type: "POST",
        url: "/Home/ValidarCorreo",
        dataType: "json",
        data: {
            "correo": correo
        },
        success: function (res) {

            if (res != "ERROR") {
                if (res == "") {
                    $("#btnRegistrar").prop("disabled", false);
                }
                else {
                    alert(res);
                }
            }

        }
    });

}
