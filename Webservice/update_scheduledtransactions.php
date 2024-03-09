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
        $updateQuery = "UPDATE ScheduledTransactions SET ";
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
            // Fetch the updated record
            $selectQuery = "SELECT * FROM ScheduledTransactions WHERE DriversId = ?";
            $selectStmt = $conn->prepare($selectQuery);
            $selectStmt->bind_param('s', $DriversId);
            $selectStmt->execute();
            $result = $selectStmt->get_result();
            $updatedRecord = $result->fetch_assoc();

            // Close the select statement
            $selectStmt->close();

            // Update successful, provide success response with the updated record
            $response = array("status" => "success", "data" => $updatedRecord);
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
