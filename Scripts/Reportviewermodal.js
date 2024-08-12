function showReport() {
    var modal = document.getElementById('myModal');
    if (modal) {
        modal.style.display = 'block';
    }
}

function closeModal() {
    var modal = document.getElementById('myModal');
    if (modal) {
        modal.style.display = 'none';
    }
}

// Ensure the modal is hidden when the page loads
document.addEventListener('DOMContentLoaded', function () {
    var closeButton = document.querySelector('.close');
    if (closeButton) {
        closeButton.addEventListener('click', closeModal);
    }
});


$(document).ready(function () {
    $('#GridView1').on('click', '.page-link', function (e) {
        e.preventDefault();
        var pageIndex = $(this).data('page-index');
        __doPostBack('GridView1', pageIndex);
    });
});

function WebForm_OnSubmit() {
    // Ensure `bobj` is defined
    if (typeof bobj !== 'undefined') {
        // Use `bobj` here
    } else {
        console.error('bobj is not defined');
    }
}