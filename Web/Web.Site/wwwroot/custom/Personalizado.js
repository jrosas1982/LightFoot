function MensajeError(titulo, mensaje) {
    swal({
        title: titulo,
        text: mensaje,
        type: "warning",
        confirmButtonText: "Ok",
        closeOnConfirm: false
    });
}
