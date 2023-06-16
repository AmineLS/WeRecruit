$(".form-file").attr('data-before', 'Select File');

$('.file-input').change(function () {
    const fileName = $(this).val();
    const start = fileName.lastIndexOf("\\") + 1;
    $(".form-file").attr('data-before', fileName.slice(start));
});

$('form').submit(function (e) {
    e.preventDefault();

    const firstNameInput = $('#firstName');
    const lastNameInput = $('#lastName');
    const emailInput = $('#email');
    const phoneInput = $('#phoneNumber');
    const yoeInput = $('#yoe');
    const lastEmployerInput = $('#lastEmployer');
    const resumeInput = $('#resume');


    // Clear any previous error messages
    let isErrorsPresent = false;
    let errorClass = 'input-error';
    for (let input of [firstNameInput, lastNameInput, emailInput, phoneInput, yoeInput, lastEmployerInput, resumeInput]) {
        input.removeClass(errorClass);
        if (input.val().trim() === '') {
            input.addClass(errorClass)
            isErrorsPresent = true
        }
    }

    // If there are no error messages, submit the form
    if (!isErrorsPresent) {
        $('.error-message').remove()
        $('form')[0].submit()
    } else if ($('.error-message').length === 0)
        $('.form-description').after(`<p class="error-message">Please fill out all the fields.</p>`)
});