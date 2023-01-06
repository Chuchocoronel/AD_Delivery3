<?php
    // //printf( "Hello world! Today is ". date("Y-m-d H:i:s"));
    // //printf("Hola Ignasi ets bobo");
    // $servername = "localhost";
    // $username = "marcrp5";
    // $password = "azqTgT2U5V";
    // $database = "marcrp5";

    // //Create connection
    // $db = new mysqli($servername, $username,$password,$database);

    // //Check connection
    // if($db->connect_error)
    // {
    //     die("Connection failed: ". $conn->connect_error);
    // }
    // else
    // {
    //     if ($db->connect_error) {
    //         die("Connection failed: "
    //             . $db->connect_error);
    //     }
    //     printf($servername);

    //     $Name = $_POST["name"];
    //     $DateOfBirth = $_POST["dateOfBirth"];
    //     $Country = $_POST["country"];

    //     $sqlquery = "INSERT INTO Playerinfo VALUES
    // ('".$Name"', '".$DateOfBirth"', '".$Country"')";
    //     $result = mysqli_query($db, $sqlquery);
    //     if ($db->query($sqlquery) === TRUE)
    //     {
    //         echo "\nrecord inserted successfully!\n";
    //     } 
    //     else
    //     {
    //         echo "Error: " . $sql . "<br>\n" . $db->error;
    //     }
    // }

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
$time = $_POST["timer"];
$damager = $_POST["damager"];
$query = "INSERT INTO death
        SET
        x = '$x',
        y = '$y',
        z = '$z',
        timer = '$time',
        damager = '$damager'";
$result = mysqli_query($db,$query) or die('just  died');
$last_inserted =  $db->insert_id;
print($last_inserted);
?>