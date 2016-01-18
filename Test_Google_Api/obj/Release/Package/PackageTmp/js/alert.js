$.extend({ alert: function (message, title) {
  $("<div></div>").dialog( {
    buttons: { "Ok": function () { $(this).dialog("close"); } },
	 open: function() {
	 $('.ui-dialog-buttonset').find('button:contains("Ok")').addClass('btn');
	  $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-button');
                            $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-widget');
                            $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-state-default');
                            $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-button-text-only');
                            $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-corner-all');
                            $('.ui-dialog-buttonset').find('button:contains("Ok")').removeClass('ui-state-focus');
	 },
    close: function (event, ui) { $(this).remove(); },
    resizable: false,
    title: title,
    modal: true
  }).text(message);
}
});