$(document).ready(function () {
    $('#Trabajador_IdDepartamento').change(function () {
        var deptoId = $(this).val();
        var $provincia = $('#Trabajador_IdProvincia');
        var $distrito = $('#Trabajador_IdDistrito');
        $provincia.empty().append('<option value="">--Seleccione Provincia--</option>');
        $distrito.empty().append('<option value="">--Seleccione Distrito--</option>');

        if (deptoId) {
            $.getJSON('/Trabajadores/GetProvincias', { idDepartamento: deptoId }, function (data) {
                $.each(data, function (i, item) {
                    $provincia.append('<option value="' + item.id + '">' + item.nombreProvincia + '</option>');
                });
            });
        }
    });

    $('#Trabajador_IdProvincia').change(function () {
        var provId = $(this).val();
        var $distrito = $('#Trabajador_IdDistrito');
        $distrito.empty().append('<option value="">--Seleccione Distrito--</option>');

        if (provId) {
            $.getJSON('/Trabajadores/GetDistritos', { idProvincia: provId }, function (data) {
                $.each(data, function (i, item) {
                    $distrito.append('<option value="' + item.id + '">' + item.nombreDistrito + '</option>');
                });
            });
        }
    });
});
