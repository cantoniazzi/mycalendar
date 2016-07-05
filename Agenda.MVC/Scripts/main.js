    $(document).ready(function () {
        $('.link-modal').attr("href", "javascript:void(0);");

        //open modal with font-awesome icons
        $('#Icone-link').click(function () {
            $('#modal-content').load('/Agendas/ShowIcons', function () {
                $('#modal-window').modal({
                    keyboard: true
                }, 'show');
            });
        });

        //open modal details
        $('.link-modal').click(function () {
            var id = $(this).attr("data-id");
            var view = $(this).attr("data-view");
            var width = $(this).attr("data-width");
            var height = $(this).attr("data-height");

            $('#modal-window').find(".modal-dialog").width(width);
            $('#modal-window').find(".modal-content").height(height);

            console.log($('#modal-window').find(".modal-content").height());

            $('#content-window-details').load(view + id, function () {
                $('#modal-window').modal({keyboard: true,}, 'show');
            });
        });

        // show or hide agenda´s compromissos
        $('.link-open-compromissos').click(function () {
            var agendaId = $(this).attr('data-agenda-id');

            if ($('#compromissos-' + agendaId + ':visible').length == 0) {
                $(this).find('span').html('esconder compromissos');
                $(this).find('i').attr('class', 'fa fa-minus-circle');

                $('#content-compromissos-' + agendaId).load('/Agendas/GetCompromissos/' + agendaId, function () {
                    $('#compromissos-' + agendaId).slideDown();
                });
            } else {
                $(this).find('span').html('exibir compromissos');
                $(this).find('i').attr('class', 'fa fa-plus-circle');
                $('#compromissos-' + agendaId).slideUp();
            }
            
        });


    });

    //close modal
    closeModalIcone = function () {
        $('#modal-window').modal('hide');
    };

    closeModal = function () {
        $('#modal-window').modal('hide');
        window.location.reload(true);
    };
