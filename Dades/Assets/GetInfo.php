<?php
$servername = "localhost";
$username = "marcrp5";
$password = "azqTgT2U5V";
$database = "marcrp5";

// Create connection
$conn = new mysqli($servername, $username, $password, $database);

// Check connection
if($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT x, y, z, timer, damagetype, damager FROM death";

$result = $conn->query($sql);

if($result->num_rows > 0)
{
    // Output data from each row.
    while($row = $result->fetch_assoc()) {
        echo "*".$row["x"]."/".$row["y"]."/".$row["z"]."/".$row["timer"]."/".$row["damagetype"]."/".$row["damager"];
    }
} else {
    echo "0 results";
}

$conn->close();
?>