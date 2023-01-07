<?php
$servername = "localhost";
$username = "marcrp5";
$password = "azqTgT2U5V";
$db = "marcrp5";

// Create connection
$conn = new mysqli($servername, $username, $password, $db);

// Check connection
if($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected Successfully"."<br>";

$sql = "SELECT HitNum, PlayerPosX, PlayerPosY, PlayerPosZ, EnemyName, EnemyPosX, EnemyPosY, EnemyPosZ, GameTime FROM Hits";

$result = $conn->query($sql);

if($result->num_rows > 0)
{
    // Output data from each row.
    while($row = $result->fetch_assoc()) {
        echo "*".$row["HitNum"]."/".$row["PlayerPosX"]."/".$row["PlayerPosY"]."/".$row["PlayerPosZ"]."/".$row["EnemyName"]."/".$row["EnemyPosX"]."/".$row["EnemyPosY"]."/".$row["EnemyPosZ"]."/".$row["GameTime"];
    }
} else {
    echo "0 results";
}

$conn->close();
?>