
$(document).ready(function () {
    $("#modDynamicModal").on('show.bs.modal', function (e) {
        var $btn = $(e.relatedTarget); //Calling DOM Element
        if (!$btn || $btn.length === 0) {
            e.preventDefault();
            return false;
        }
        var id = $btn.data("id"); //DB Model Id
        var action = $btn.data("action");
        var $mod = $(e.target);
        var url = $mod.data("modalaction");

        $(".modal-title", $mod).html("");
        $(".modal-body", $mod).html("");
        $(".modal-dialog", $mod).removeClass("modal-lg");

        var caller = $btn.attr("id"); //DOM Element Id 
        if (caller) {
            $mod.data("caller", "#" + caller);
        }
        var trgcon = $btn.data("trgcon"); //targetcontainer
        if (trgcon) {
            $mod.data("trgcon", trgcon);
        } else {
            $mod.removeData("trgcon");
        }

        $.ajax({
            url: url,
            type: "POST",
            data: { MAId: id, MAAction: action },
            dataType: "json",
            success: function (json) {
                if (json) {
                    $(".modal-title", $mod).html(json.Title);
                    $(".modal-body", $mod).html(json.Body);
                    if (json.PageMessage) $frm.prepend(GenerateAlertMessage(json.PageMessage, "danger"));
                    if (json.ModalMessage) $(".modal-body", $mod).prepend(GenerateAlertMessage(json.ModalMessage, "danger"));
                    if (json.Wide) $(".modal-dialog", $mod).addClass("modal-lg");
                } else {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage("An internal occured, please contact your administrator", "danger"));
                }
            },
            error: function (jqXHR, error, errorThrown) {
                //$(".modal-title", $mod).html(error);
                if (jqXHR.status && jqXHR.status === 400) {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage(jqXHR.responseText, "danger"));
                    alert();
                } else {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage("An internal occured, please contact your administrator", "danger"));
                }
            }
        });
    });
    $(".staticModalContent").on('show.bs.modal', function (e) {
        var $btn = $(e.relatedTarget); //Calling DOM Element
        if (!$btn || $btn.length === 0) {
            e.preventDefault();
            return false;
        }
        var $mod = $(e.target);
        var caller = $btn.attr("id"); //DOM Element Id 
        if (caller) {
            $mod.data("caller", "#" + caller);
        }
        var trgcon = $btn.data("trgcon"); //targetcontainer
        if (trgcon) {
            $mod.data("trgcon", trgcon);
        } else {
            $mod.removeData("trgcon");
        }
        if ($mod) {
            $(".alert-container", $mod).remove();
            var id = $btn.data("id"); //DB Model Id
            $("[name=\"Id\"]", $mod).val(id);
        }
    });
    $(".dynamicModalContent, .staticModalContent").delegate("form", "submit", function (e) {
        e.preventDefault();
        console.log(e);
        var $frm = $(this);
        var $mod = $frm.parents(".modal");
        var caller = $mod.data("caller");
        var trgcon = $mod.data("trgcon");
        $.ajax({
            url: e.target.action,
            type: "POST",
            data: $frm.serialize(),
            dataType: "json",
            success: function (json) {
                if (json) {
                    if (json.ReloadPage) {
                        benhof_isloading = true;
                        location.reload();
                    } else if (json.Redirect) {
                        benhof_isloading = true;
                        window.location.href = json.Redirect;
                    } else {
                        if (json.UpdateCaller && caller) $(caller).replaceWith(json.UpdateCaller);
                        if (json.CloseModal) $mod.modal("hide");
                        if (json.PageMessage) AddPageMessage(json.PageMessage);
                        if (json.Title) $(".modal-title", $mod).html(json.Title);
                        if (json.Body) $(".modal-body", $mod).html(json.Body);
                        if (json.ModalMessage) $(".modal-body", $mod).prepend(GenerateAlertMessage(json.ModalMessage, "danger"));
                        if (json.TargetContainer && trgcon) $(trgcon).replaceWith(json.TargetContainer);
                    }
                } else {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage("An internal occured, please contact your administrator", "danger"));
                }
            },
            error: function (jqXHR, error, errorThrown) {
                //$(".modal-title", $mod).html(error);
                if (jqXHR.status && jqXHR.status === 400) {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage(jqXHR.responseText, "danger"));
                    alert();
                } else {
                    $(".modal-body", $mod).prepend(GenerateAlertMessage("An internal occured, please contact your administrator", "danger"));
                }
            }
        });
    });
});