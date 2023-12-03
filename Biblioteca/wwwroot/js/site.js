$(document).ready(function () {

    $('#Biblioteca').DataTable({
        language:
        {
            "decimal": "",
            "emptyTable": "Nenhum dado registrado na tabela",
            "info": "Mostrando _START_ registro de _END_ em um total de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 registros",
            "infoFiltered": "(filtrando de _MAX_ entradas no total)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Buscando...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "Nenhum Registro Encontrado",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    }
    );


    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        })
    }, 3000)
})