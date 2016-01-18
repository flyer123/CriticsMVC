$(document).ready(function () {
        $("#AjaxPost").click(function () {
            var dataObject = {
                LabelText: $("#LabelText").val(),
                E_mail: $("#E_mail").val(),
                Name: $("#Name").val(),
                ReviewID: "@ViewBag.ID"
            };
            $.ajax({
                url: "@Url.Action("SaveLabel", "Home", id)",
                type: "POST",
                data: dataObject,
                dataType: "json",
                success: function (data) {
                    if (data.toString() == "Successfully Saved!") {
                        $("ul#lists").prepend('<li><div class="feedback-info"><div class="feedback-name">' +
                                        $("#Name").val() +
                                    '</div><div class="feedback-date"><time datetime="2015-10-10">' +
                                        new Date().toLocaleString() +
                                        '</time></div></div><div class="feedback-text"><p>' +
                                    $("#LabelText").val() +
                                    '</p></div></li>');
                    }
                    else {
                        $("#name").html();
                    }
                },
                error: function () {
                    alert("<div class='failed'>Error! Please try again</div>");
                }
            });
        })
    })