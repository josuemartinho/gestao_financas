(document).ready(function () {
    $('#Categoria').DataTable({
        language: {
            "decimal": ",",
            "emptyTable": "Nenhum registro encontrado na tabela",
            "info": "Mostrar _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Mostrar 0 a 0 de 0 registros",
            "infoFiltered": "(filtragem de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "Nenhum registro encontrado!",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Proximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    })
});