function toggleNav() {
    document.body.classList.add("sidebar-open");
    document.body.classList.remove("sidebar-closed");
}

function closeNav() {
    document.body.classList.add("sidebar-closed");
    document.body.classList.remove("sidebar-open");
}


function toggleSubmenu(id) {
    var submenu = document.getElementById(id);
    var arrow = document.querySelector('.hris-menu .arrow');
    if (submenu.style.display === "block") {
        submenu.style.display = "none";
        arrow.classList.remove('submenu-open');
    } else {
        submenu.style.display = "block";
        arrow.classList.add('submenu-open');
    }
}

function loadContent(page) {
    // Example of how you might dynamically load content
    // You can use AJAX or similar methods to load content
    if (page === 'CashAdvances') {
        window.location.href = 'CashAdvances.aspx';
    } else if (page === 'dashboard') {
        window.location.href = 'Default.aspx'; // or appropriate dashboard page
    } else if (page === 'Liquidations') {
        window.location.href = 'Liquidations.aspx';
    } else if (page === 'Leave') {
        window.location.href = 'Leave.aspx?source=Leave';
    } else if (page === 'OT') {
        window.location.href = 'OT.aspx'; 
    } else if (page === 'OBT') {
        window.location.href = 'OBT.aspx'; 
    } else if (page === 'Offset') {
        window.location.href = 'Leave.aspx?source=Offset';
    }
    // Close the nav when a link is clicked
    closeNav();
}

function confirmLogout() {
    return confirm("Are you sure you want to logout?");
}

// Ensure the sidenav is closed by default
document.addEventListener("DOMContentLoaded", function() {
    closeNav();
});
