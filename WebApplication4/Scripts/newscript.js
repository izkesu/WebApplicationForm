document.addEventListener('DOMContentLoaded', function () {
    $('#imageModal').on('show.bs.modal', function (e) {
        // Change border color
        $('.modal-content').css('border-color', '#dc3545'); // Red border

        // Change background color of the modal header
        $('.modal-header').css('background-color', '#dc3545'); // Red background
    });

    $('#imageModal').on('hide.bs.modal', function (e) {
        // Reset border color
        $('.modal-content').css('border-color', '#007bff'); // Blue border

        // Reset background color of the modal header
        $('.modal-header').css('background-color', '#007bff'); // Blue background
    });
});
s