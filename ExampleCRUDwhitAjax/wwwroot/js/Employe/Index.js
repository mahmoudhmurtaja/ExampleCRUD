var employes = function () {
    const controller = 'Employe';
    var dt;

    var initDataTable = function () {
        dt = $('#Employes').DataTable({
            processing: true,
            serverSide: true,
            ajax: {
                url: `/${controller}/GetAll`,
                type: "POST",
                datatype: "json",
                data: { "search[value]": serializeArrayToObject("SearchForm") }
            },
            order: [[1, 'asc']],
            columnDefs: [{
                targets: [0],
                visible: true,
                searchable: false
            }],
            columns: [
                {
                    width: "5%",
                    render: function (data, type, row, meta) {
                        return meta.settings._iDisplayStart + meta.row + 1;
                    },
                    orderable: false
                },
                { data: "name", name: "Name", autowidth: true },
                { data: "phoneNumber", name: "PhoneNumber", autowidth: true },
                { data: "email", name: "Email", autowidth: true },
                { data: "address", name: "Address", autowidth: true },
                {
                    width: "10%",
                    render: function (data, type, row) {
                        return `<div class="dropdown">
                                    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                                        <i class="bi bi-gear-fill fs-4"></i>
                                    </button>
                                    <ul class="dropdown-menu fs-4" aria-labelledby="dropdownMenuButton1">
                                        <li><a class="dropdown-item btn btnEdit" element-id="${row.id}"><i class="bi bi-pencil-square"></i>${Messages.Edit}</a></li>
                                        <li><a class="dropdown-item btn btnDelete" element-id="${row.id}"><i class="bi bi-trash-fill"></i>${Messages.Delete}</a></li>
                                    </ul>
                                </div>`
                    },
                    orderable: false
                }
            ],
            rowCallback: function (row, data, index) {
                $(row).on('dblclick', function () {
                    getModal(data.id);
                });
            },
            language: Language
        });

        dt.on('draw', function () {
            deleteElement();
            openModal();
        });
    }

    // delete function
    var deleteElement = function () {
        $(".btnDelete").off("click").click(function () {
            const elementId = $(this).attr("element-id");

            deleteFunction(`/${controller}/Delete/${elementId}`)
                .then(function () {
                    dt.destroy();
                    initDataTable();
                });
        });
    };

    // open create or edit modal
    var openModal = function () {
        $(".openModal, .btnEdit").off("click").click(function () {
            const elementId = $(this).attr("element-id") || 0;

            getModal(elementId);
        });
    }

    // get modal
    var getModal = function (elementId) {
        $.ajax({
            url: `/${controller}/CreateEdit/${elementId}`,
            type: 'GET',
            success: function (result) {
                $('#modal .modal-content').html(result);
                $('#modal').modal('show');

                $('#modal').on('shown.bs.modal', function () {
                    $('input[type="text"]:first', this).focus();
                });

                submitForm();
            }
        });
    }

    // submit form to create or edit
    var submitForm = function () {
        $("#form").on("submit", function (e) {
            e.preventDefault();
            const form = $(this);
            const data = form.serialize();

            saveOrUpdate(`/${controller}/CreateEdit/`, data, form)
                .then(function () {
                    $('#modal').modal('hide');
                    dt.destroy();
                    initDataTable();
                })
                .catch(function () {
                    return;
                });
        });

        $(".btnSubmit").off("click").click(function () {
            $("#form").submit();
        });
    }

    var searchForm = function () {
        $(".btnSearch").off("click").click(function () {
            dt.destroy();
            initDataTable();
        });
    };

    return {
        init: function () {
            initDataTable();
            searchForm();
            openModal();
        }
    }
}();