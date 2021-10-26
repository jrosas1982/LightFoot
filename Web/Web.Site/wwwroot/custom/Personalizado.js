function MensajeError(titulo, mensaje) {
    fire({
        title: titulo,
        text: mensaje,
        type: "warning",
        confirmButtonText: "Ok",
        closeOnConfirm: falsensaje
    });
}