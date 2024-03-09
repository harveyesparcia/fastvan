<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $DriversId = @$_REQUEST["DriversId"];

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Prepare the update query
        $updateQuery = "UPDATE Queues SET ";
        $updateParams = array();

        // Build the SET clause dynamically based on provided parameters
        foreach ($_REQUEST as $param => $value) {
            // Exclude DriversId from being updated
            if ($param !== "DriversId") {
                $updateQuery .= "$param = ?, ";
                $updateParams[] = $value;
            }
        }

        // Remove the trailing comma and space
        $updateQuery = rtrim($updateQuery, ", ");

        // Add the WHERE clause
        $updateQuery .= " WHERE DriversId = ?";

        // Append the DriversId to the updateParams array
        $updateParams[] = $DriversId;

        // Prepare the statement
        $stmt = $conn->prepare($updateQuery);

        // Bind parameters to the prepared statement dynamically
        $bindTypes = str_repeat('s', count($updateParams));
        $stmt->bind_param($bindTypes, ...$updateParams);

        // Execute the statement
        $stmt->execute();

        // Check for query errors
        if ($stmt->error) {
            $response = array("status" => "failed", "error" => "Database query error: " . $stmt->error);
            echo json_encode($response);
        } else {
            // Update successful, provide success response
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