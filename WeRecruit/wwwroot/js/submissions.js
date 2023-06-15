$(".form-file").attr('data-before', 'Select File');
$('.file-input').change(function () {
    const fileName = $(this).val();
    const start = fileName.lastIndexOf("\\") + 1;
    $(".form-file").attr('data-before', fileName.slice(start));
});