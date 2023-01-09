<?php
 $servername = "localhost";
 $username = "marcrp5";
 $password = "azqTgT2U5V";
 $database = "marcrp5";

 $db = new mysqli($servername, $username, $password, $database);
 if($db->connection_error) {
     die("Connection failed: " . $conn->connect_error);
}
$query = "DELETE FROM `trackedpositions`";
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted =  $db->insert_id;
print($last_inserted);
?>