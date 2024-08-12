document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();

    // Get the form values
    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    // Simple form validation
    if (username === "" || password === "") {
        alert("Please fill in all fields.");
    } else {
        // Handle the form submission, e.g., send data to the server
        alert("Login successful for user: " + username);
        // Perform actual login logic here
    }
});