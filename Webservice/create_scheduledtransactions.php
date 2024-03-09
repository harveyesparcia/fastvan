<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $DriversId = @$_REQUEST["DriversId"];
    $QueuesId = @$_REQUEST["QueuesId"];
    $ArrivalDateTime = @$_REQUEST["ArrivalDateTime"];
    $DepartureDateTime = @$_REQUEST["DepartureDateTime"];
    $FrontSeat1 = @$_REQUEST["FrontSeat1"];
    $FrontSeat2 = @$_REQUEST["FrontSeat2"];
    $van1stSeat1 = @$_REQUEST["van1stSeat1"];
    $van1stSeat2 = @$_REQUEST["van1stSeat2"];
    $van1stSeat3 = @$_REQUEST["van1stSeat3"];
    $van1stSeat4 = @$_REQUEST["van1stSeat4"];
    $van2ndSeat1 = @$_REQUEST["van2ndSeat1"];
    $van2ndSeat2 = @$_REQUEST["van2ndSeat2"];
    $van2ndSeat3 = @$_REQUEST["van2ndSeat3"];
    $van2ndSeat4 = @$_REQUEST["van2ndSeat4"];
    $van3rdSeat1 = @$_REQUEST["van3rdSeat1"];
    $van3rdSeat2 = @$_REQUEST["van3rdSeat2"];
    $van3rdSeat3 = @$_REQUEST["van3rdSeat3"];
    $van3rdSeat4 = @$_REQUEST["van3rdSeat4"];
    $van4thSeat1 = @$_REQUEST["van4thSeat1"];
    $van4thSeat2 = @$_REQUEST["van4thSeat2"];
    $van4thSeat3 = @$_REQUEST["van4thSeat3"];
    $van4thSeat4 = @$_REQUEST["van4thSeat4"];
    $ExtraSeat1 = @$_REQUEST["ExtraSeat1"];
    $ExtraSeat2 = @$_REQUEST["ExtraSeat2"];
    $ExtraSeat3 = @$_REQUEST["ExtraSeat3"];
    $ExtraSeat4 = @$_REQUEST["ExtraSeat4"];
    $Status = @$_REQUEST["Status"];

    // Get the current date
    $currentDate = date("Y-m-d");

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Use placeholders in the SQL query
        $stmt = $conn->prepare("INSERT INTO ScheduledTransactions (DriversId, QueuesId, Date, ArrivalDateTime, DepartureDateTime, FrontSeat1, FrontSeat2, 1stSeat1, 1stSeat2, 1stSeat3, 1stSeat4, 2ndSeat1, 2ndSeat2, 
        2ndSeat3, 2ndSeat4, 3rdSeat1, 3rdSeat2, 3rdSeat3, 3rdSeat4, 4thSeat1, 4thSeat2, 4thSeat3, 4thSeat4, ExtraSeat1, ExtraSeat2, ExtraSeat3, ExtraSeat4, Status) 
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");

        // Bind parameters to the prepared statement
        $stmt->bind_param("ssssiiiiiiiiiiiiiiiiiiiiiiii", $DriversId, $QueuesId, $currentDate, $ArrivalDateTime, $DepartureDateTime, $FrontSeat1, $FrontSeat2, $van1stSeat1, $van1stSeat2, $van1stSeat3, $van1stSeat4, 
        $van2ndSeat1, $van2ndSeat2, $van2ndSeat3, $van2ndSeat4, $van3rdSeat1, $van3rdSeat2, $van3rdSeat3, $van3rdSeat4, $van4thSeat1, $van4thSeat2, $van4thSeat3, $van4thSeat4, $ExtraSeat1, $ExtraSeat2, $ExtraSeat3, 
        $ExtraSeat4, $Status);

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
