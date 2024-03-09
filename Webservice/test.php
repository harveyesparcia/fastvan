<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    // Handle GET requests to retrieve data

    // If DriversId is provided, retrieve data for that specific driver
    if (isset($_REQUEST["DriversId"])) {
        $DriversId = $_REQUEST["DriversId"];

        $conn = new mysqli($servername, $username, $password, $database);

        // Check for database connection errors
        if ($conn->connect_error) {
            $response = array("status" => "failed 1", "error" => "Database connection error: " . $conn->connect_error);
            echo json_encode($response);
        } else {
            // Retrieve data for the specified DriversId
            $query = "SELECT * FROM Queues WHERE DriversId = ?";
            $stmt = $conn->prepare($query);
            $stmt->bind_param("s", $DriversId);
            $stmt->execute();
            $result = $stmt->get_result();

            // Check for query errors
            if ($stmt->error) {
                $response = array("status" => "failed 2", "error" => "Database query error: " . $stmt->error);
                echo json_encode($response);
            } else {
                // Fetch data and provide success response
                $data = $result->fetch_all(MYSQLI_ASSOC);
                $response = array("status" => "success", "data" => $data);
                echo json_encode($response);
            }

            // Close the prepared statement
            $stmt->close();

            // Close the database connection
            $conn->close();
        }
    } else {
        // If DriversId is not provided, retrieve all data
        $conn2 = new mysqli($servername, $username, $password, $database);

        // Check for database connection errors
        if ($conn2->connect_error) {
            $response = array("status" => "failed 3", "error" => "Database connection error: " . $conn2->connect_error);
            echo json_encode($response);
        } else {
            // Retrieve all data
            $query = "SELECT * FROM Queues";
            $stmt = $conn2->prepare($query);

            // Check for query preparation errors
            if (!$stmt) {
                $response = array("status" => "failed 4", "error" => "Database query preparation error (Code: " . $conn2->errno . "): " . $conn2->error);
                echo json_encode($response);
            } else {
                // Execute the statement
                $stmt->execute();

                // Get the result set
                $result = $stmt->get_result();

                // Check for query errors
                if (!$result) {
                    $response = array("status" => "failed 5", "error" => "Database query execution error (Code: " . $stmt->errno . "): " . $stmt->error);
                    echo json_encode($response);
                } else {
                    // Fetch data and provide success response
                    $data = array();

                    while ($row = $result->fetch_assoc()) {
                        $data[] = $row;
                    }

                    $response = array("status" => "success", "data" => $data);
                    echo json_encode($response);
                }

                // Close the prepared statement
                $stmt->close();
            }

            // Close the database connection
            $conn2->close();
        }
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
