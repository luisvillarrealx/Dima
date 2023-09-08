function BuscarCorreo() {

    $("#btnRegistrar").prop("disabled", true);
    let correo = $("#correo").val();

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

function ValidarConfirmacionContrasenna() {

    let contrasenna = $("#contrasena").val();
    let confirmarContrasenna = $("#confirmarcontrasena").val();

    if (contrasenna.trim() != "" && confirmarContrasenna.trim()) {
        if (contrasenna.trim() != confirmarContrasenna.trim()) {
            alert("Las contraseñas no coinciden");
            $("#confirmarcontrasena").val("");
        }
    }
}