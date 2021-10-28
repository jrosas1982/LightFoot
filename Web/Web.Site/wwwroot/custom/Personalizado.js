function MensajeError2(titulo, mensaje) {
    fire({
        title: titulo,
        text: mensaje,
        type: "warning",
        confirmButtonText: "Ok",
        closeOnConfirm: false
    });
}

function MensajeError(titulo, mensaje) {
    Swal.fire({
        icon: 'error',
        title: titulo,
        text: mensaje,
        type: "warning",
    });
}