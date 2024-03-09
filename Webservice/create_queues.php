<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $VanPlateNumber = @$_REQUEST["VanPlateNumber"];
    $DepartureDateTime = @$_REQUEST["DepartureDateTime"];
    $ArrivalDateTime = @$_REQUEST["ArrivalDateTime"];
    $DriversId = @$_REQUEST["DriversId"];
    $Id = @$_REQUEST["Id"];

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Use placeholders in the SQL query
        $stmt = $conn->prepare("INSERT INTO Queues (VanPlateNumber, DepartureDateTime, ArrivalDateTime, DriversId, Id, Status, Date) 
        VALUES (?, ?, ?, ?, ?, ?, NOW())");

        // Check for the existence of the "Status" parameter in your request
        $Status = isset($_REQUEST["Status"]) ? $_REQUEST["Status"] : 1;

        // Bind parameters to the prepared statement
        $stmt->bind_param("ssssii", $VanPlateNumber, $DepartureDateTime, $ArrivalDateTime, $DriversId, $Id, $Status);

        // Execute the statement
        $stmt->execute();

        // Check for query errors
        if ($stmt->error) {
            $response = array("status" => "failed", "error" => "Database query error: " . $stmt->error);
            echo json_encode($response);
        } else {
            // Insert successful, provide success response
            $response = array("status" => "success");
            echo json_encode($response);
        }

        // Close the prepared statement
        $stmt->close();

        // Close the database connection
        $conn->close();
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
