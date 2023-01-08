<?php
 $servername = "localhost";
 $username = "marcrp5";
 $password = "azqTgT2U5V";
 $database = "marcrp5";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $conn->connect_error);
}
$positionx = $_POST["positionx"];
$positiony = $_POST["positiony"];
$positionz = $_POST["positionz"];
$num = $_POST["num"];
$query = "INSERT INTO trackedpositions
        SET
        positionx = '$positionx',
        positiony = '$positiony',
        positionz = '$positionz'";
$result = mysqli_query($db,$query) or die('just  died');
print($last_inserted);
?>