document.getElementById('ageForm').addEventListener('submit', function (event) {
    event.preventDefault();
    alert("You have entered your age");
    showMessage("Calculating age...");
    setTimeout(calculateAge, 1000); // Simulating a delay for demonstration
});

function showMessage(message) {
    document.getElementById('message').innerHTML = message;
}

function calculateAge() {
    var dob = new Date(document.getElementById('dob').value);
    var now = new Date();
    var age = now.getFullYear() - dob.getFullYear();
    var monthDiff = now.getMonth() - dob.getMonth();
    if (monthDiff < 0 || (monthDiff === 0 && now.getDate() < dob.getDate())) {
        age--;
    }
    document.getElementById('result').innerHTML = "Your age is: " + age;
    alert("Your age is Calculating");
    showMessage("Age calculated successfully!"); // Displaying a message
}
