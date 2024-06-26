<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    if ($_SERVER['REQUEST_METHOD'] === 'GET') {
        // Handle GET requests to retrieve data

        // If PassengersId is provided, retrieve data for that specific driver
        if (isset($_REQUEST["PassengersId"])) {
            $PassengersId = $_REQUEST["PassengersId"];

            $conn = new mysqli($servername, $username, $password, $database);

            // Check for database connection errors
            if ($conn->connect_error) {
                $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
                echo json_encode($response);
            } else {
                // Retrieve data for the specified PassengersId
                $query = "SELECT * FROM account WHERE typeofaccount = 2";
                $stmt = $conn->prepare($query);
                $stmt->bind_param("s", $PassengersId);
                $stmt->execute();
                $result = $stmt->get_result();

                // Check for query errors
                if ($stmt->error) {
                    $response = array("status" => "failed", "error" => "Database query error: " . $stmt->error);
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
            // If PassengersId is not provided, retrieve all data
            $conn = new mysqli($servername, $username, $password, $database);

            // Check for database connection errors
            if ($conn->connect_error) {
                $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
                echo json_encode($response);
            } else {
                // Retrieve all data
                $query = "SELECT * FROM account"; // Corrected line
                $result = $conn->query($query);

                // Check for query errors
                if (!$result) {
                    $response = array("status" => "failed", "error" => "Database query error: " . $conn->error);
                    echo json_encode($response);
                } else {
                    // Fetch data and provide success response
                    $data = $result->fetch_all(MYSQLI_ASSOC);
                    $response = array("status" => "success", "data" => $data);
                    echo json_encode($response);
                }

                // Close the database connection
                $conn->close();
            }
        }
    } elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
        // Handle POST requests to update data
        $PassengersId = @$_REQUEST["PassengersId"];

        $conn = new mysqli($servername, $username, $password, $database);

        // Check for database connection errors
        if ($conn->connect_error) {
            $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
            echo json_encode($response);
        } else {
            // Prepare the update query
            $updateQuery = "UPDATE account SET ";
            $updateParams = array();

            // Build the SET clause dynamically based on provided parameters
            foreach ($_REQUEST as $param => $value) {
                // Exclude PassengersId from being updated
                if ($param !== "PassengersId") {
                    $updateQuery .= "$param = ?, ";
                    $updateParams[] = $value;
                }
            }

            // Remove the trailing comma and space
            $updateQuery = rtrim($updateQuery, ", ");

            // Add the WHERE clause
            $updateQuery .= " WHERE PassengersId = ?";

            // Append the PassengersId to the updateParams array
            $updateParams[] = $PassengersId;

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
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>