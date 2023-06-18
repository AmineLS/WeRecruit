let modal = $('#modal-dialog');
let modalTitle = $('.modal-title');
let showButton = $(".show-button")
let closeButton = $(".close-button");
let resumePreviewObject = $("#resume-preview")
let resumeDownloadButton = $("#resume-download")

$(document).ready(function(){
    showButton.on('click', function() {
        resumePreviewObject.attr("data", $(this).data("resume-url"))
        resumeDownloadButton.attr("href", $(this).data("resume-url"))
        modalTitle.text($(this).data("resume-owner"))
        modal.fadeIn();
    });

    closeButton.on('click', function() {
        modal.fadeOut();
    });
});

$('body').bind('click', function(e){
    if($(e.target).hasClass("modal")) modal.fadeOut();
});