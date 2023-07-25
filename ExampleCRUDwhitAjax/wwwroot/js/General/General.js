var serializeArrayToObject = function (form) {
    var array = $("#" + form).serializeArray();
    function assignByPath(obj, path, value) {
        if (path.length == 1) {
            obj[path[0]] = value;
            return obj;
        } else if (obj[path[0]] === undefined) {
            obj[path[0]] = {};
        }
        return assignByPath(obj[path.shift()], path, value);
    }

    var obj = {};

    $.each(array, function (i, o) {
        var n = o.name,
            v = o.value;
        path = n.replace('[', '.').replace('][', '.').replace(']', '').split('.');

        assignByPath(obj, path, v);
    });

    return JSON.stringify(obj);
};

var saveOrUpdate = function (url, data, form) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (result) {
                form.find(".validation").text("");
                form.find(".validation").removeClass("d-block").addClass("d-none");
                if (result.success) {
                    toastr.success(Messages.AlertMessage, result.message);
                    resolve();
                } else {
                    var error = "<div>" + result.message +"</div>";
                    form.find(".validation").append(error);
                    form.find(".validation").removeClass("d-none").addClass("d-block");
                    reject();
                }
            },
            error: function (error) {
                reject(error);
            }
        });
    });
};

var deleteFunction = function (url) {
    return new Promise(function (resolve, reject) {
        Swal.fire({
            html: '<h4>' + Messages.DeleteConfirmation + '</h4>',
            icon: "warning",
            buttonsStyling: false,
            showCancelButton: true,
            confirmButtonText: Messages.Delete,
            cancelButtonText: Messages.Cancel,
            customClass: {
                confirmButton: "btn btn-warning",
                cancelButton: 'btn btn-secondary'
            },
            preConfirm: (login) => {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (result) {
                        if (result)
                            toastr.success(Messages.AlertMessage, Messages.OprationSuccessfully);
                        else
                            toastr.error(Messages.AlertMessage, Messages.OperationFailed);
                    
                        resolve();
                    },
                    error: function (error) {
                        reject(error);
                    }
                });
            }
        });
    });
};

var general = function () {

    // show shortcutsModal modal
    var shortcutsModal = function () {
        $("#btnshortcutsModal").off("click").click(function () {
            $('#shortcutsModal').modal('show');
        });
    }
   
    //********** HotKeys keyboard shortcuts ***************

    // shortcuts F1 => f1
    var hk_f1 = function () {
        hotkeys('f1', function (event) {
            event.preventDefault();
            $('#btnshortcutsModal').click();
        });
    }

    // close widnow => Esc
    var hk_esc = function () {
        hotkeys('Esc', function () {
            
        });
    }

    // add => +
    var hk_add = function () {
        hotkeys('num_add', function () {
            $('.btnAdd').click();
        });
    }

    // search => ctrl+f
    var hk_search = function () {
        hotkeys('ctrl+f', function (event) {
            event.preventDefault();
            $('#searchInput').focus();
        });
    }

    // up
    var hk_up = function () {
        hotkeys('ctrl+up', function (event, handler) {
            alert('press ctrl + up');
        });
    }    

    return {
        init: function () {
            shortcutsModal();
            hk_f1();
            //hk_esc();
            hk_add();
            hk_search();
            hk_up();
            
        }
    }
}();

general.init();
