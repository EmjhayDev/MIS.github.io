// Dark Mode Toggle Script (Darkmodescript.js)
document.addEventListener('DOMContentLoaded', function () {
    const darkModeToggle = document.getElementById('darkModeToggle');
    const darkModeCss = document.getElementById('darkmode-css');

    // Check the user's saved preference and set the initial state
    if (localStorage.getItem('darkMode') === 'enabled') {
        darkModeToggle.checked = true;
        darkModeCss.removeAttribute('disabled');
    } else {
        darkModeToggle.checked = false;
        darkModeCss.setAttribute('disabled', 'true');
    }

    // Add event listener to the toggle switch
    darkModeToggle.addEventListener('change', function () {
        if (darkModeToggle.checked) {
            darkModeCss.removeAttribute('disabled');
            localStorage.setItem('darkMode', 'enabled');
        } else {
            darkModeCss.setAttribute('disabled', 'true');
            localStorage.setItem('darkMode', 'disabled');
        }
    });
});