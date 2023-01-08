<?php
 $servername = "localhost";
 $username = "marcrp5";
 $password = "azqTgT2U5V";
 $database = "marcrp5";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $conn->connect_error);
}
$x = $_POST["x"];
$y = $_POST["y"];
$z = $_POST["z"];
$timer = $_POST["timer"];
$damagetype = $_POST["damagetype"];
$damager = $_POST["damager"];
$type = $_POST["type"];

$query = "INSERT INTO death
        SET
        x = '$x',
        y = '$y',
        z = '$z',
        timer = '$timer',
        damagetype = '$damagetype',
        damager = '$damager',
        type = '$type'";
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted =  $db->insert_id;
print($last_inserted);
?>