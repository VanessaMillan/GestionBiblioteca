let tablaData;
let idCategoriaEditar = 0;
$(document).ready(function () {

    tablaData = $('#tbGeneroLiterario').DataTable({
        responsive: true,
        "ajax": {
            "url": "/GeneroLiterario/Lista",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { title:"Nombre", "data": "nombre" },
            { title: "IdCategoria", "data": "idCategoria" },
            { title: "", "data": "idGenero", width:"100px", render: function (data, type, row) {
                    return `<button type="button" class="btn btn-sm btn-outline-primary me-1" onclick="tbEditarCategoria(${data});"><i class="fas fa-pen-to-square"></i></button>` + 
                        `<button type="button" class="btn btn-sm btn-outline-danger me-1" onclick="tbEliminarCategoria(${data});"><i class="fas fa-trash"></i></button>`
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});

function tbEditarGenero(idGenero) {

    fetch(`/GeneroLiterario/Obtener?IdGenero=${idGenero}`, {
        method: "GET",
        headers: { 'Content-Type': 'application/json;charset=utf-8' }
    }).then(response => {
        return response.ok ? response.json() : Promise.reject(response);
    }).then(responseJson => {
        if (responseJson.data.idGenero != 0) {
            idGeneroEditar = responseJson.data.idGenero;
            $("#txtNombre").val(responseJson.data.nombre);
            $('#md').modal('show');
        }
    }).catch((error) => {
        Swal.fire({
            title: "Error!",
            text: "No se encontraron coincidencias.",
            icon: "warning"
        });
    });

}


$("#btnNuevoGenero").on("click", function () {
    idGeneroEditar = 0;
    $("#txtNombre").val("")
    $('#md').modal('show');
});


function tbEliminarGenero(id) {

    Swal.fire({
        text: "¿Desea eliminar el género?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, continuar",
        cancelButtonText: "No, volver"
    }).then((result) => {
        if (result.isConfirmed) {

            fetch(`/GeneroLiterario/Eliminar?IdGenero=${id}`, {
                method: "DELETE",
                headers: { 'Content-Type': 'application/json;charset=utf-8' }
            }).then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            }).then(responseJson => {
                if (responseJson.data == 1) {
                    Swal.fire({
                        title: "Eliminado!",
                        text: "El género fue eliminado.",
                        icon: "success"
                    });
                    tablaData.ajax.reload();
                } else {
                    Swal.fire({
                        title: "Error!",
                        text: "No se pudo eliminar.",
                        icon: "warning"
                    });
                }
            }).catch((error) => {
                Swal.fire({
                    title: "Error!",
                    text: "No se pudo eliminar.",
                    icon: "warning"
                });
            })
        }
    });
}

$("#btnGuardar").on("click", function () {
    if ($("#txtNombre").val().trim() == "") {
        Swal.fire({
            title: "Error!",
            text: "Debe ingresar el nombre.",
            icon: "warning"
        });
        return;
    }

    let objeto = {
        IdGenero: idGeneroEditar,
        Nombre: $("#txtNombre").val().trim()
    };

    if (idGeneroEditar != 0) {

        fetch(`/GeneroLiterario/Editar`, {
            method: "PUT",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            body: JSON.stringify(objeto)
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.data == "") {
                idGeneroEditar = 0;
                Swal.fire({
                    text: "Se guardaron los cambios!",
                    icon: "success"
                });
                $('#md').modal('hide');
                tablaData.ajax.reload();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: responseJson.data,
                    icon: "warning"
                });
            }
        }).catch((error) => {
            Swal.fire({
                title: "Error!",
                text: "No se pudo editar.",
                icon: "warning"
            });
        });

    } else {
        fetch(`/GeneroLiterario/Guardar`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            body: JSON.stringify(objeto)
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.data == "") {
                Swal.fire({
                    text: "Género registrado!",
                    icon: "success"
                });
                $('#md').modal('hide');
                tablaData.ajax.reload();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: responseJson.data,
                    icon: "warning"
                });
            }
        }).catch((error) => {
            Swal.fire({
                title: "Error!",
                text: "No se pudo registrar.",
                icon: "warning"
            });
        });
    }
});
