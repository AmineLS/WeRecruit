let modal = $('#modal-dialog');
let modalTitle = $('.modal-title');
let showButton = $(".show-button")
let closeButton = $(".close-button");
let modalBody = $(".modal-body")
let filterInput = $("#filter-input")
let tableRows = $("#table tbody tr");

$(document).ready(function () {
    showButton.on('click', function () {
        modalBody.empty()
        modalBody.append(
            `<object id="resume-preview" data="${$(this).data("resume-url")}">
                <p>Unable to display file. 
                    <a href="${$(this).data("resume-url")}" target="_blank">
                        Download
                    </a> 
                instead.</p>
            </object>`
        )
        modalTitle.text($(this).data("resume-owner"))
        modal.fadeIn();
    });

    closeButton.on('click', function () {
        modal.fadeOut();
    });

    filterInput.on("keyup", function () {
        let value = $(this).val().toLowerCase();
        console.log(value)
        tableRows.filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$('body').bind('click', function (e) {
    if ($(e.target).hasClass("modal")) modal.fadeOut();
});
