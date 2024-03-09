<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Check if either DriversId or QueueId is provided in the POST request
    if (isset($_POST["DriversId"]) || isset($_POST["QueueId"])) {
        $DriversId = isset($_POST["DriversId"]) ? $_POST["DriversId"] : null;
        $QueueId = isset($_POST["QueueId"]) ? $_POST["QueueId"] : null;

        $conn = new mysqli($servername, $username, $password, $database);

        // Check for database connection errors
        if ($conn->connect_error) {
            $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
            echo json_encode($response);
            exit;
        }

        try {
            // Use placeholders in the SQL query
            $query = "SELECT * FROM ScheduledTransactions WHERE DriversId = ?";

            $paramTypes = "s";
            $bindParams = array($DriversId);

            if ($QueueId !== null) {
                $query .= " AND QueuesId = ?";
                $paramTypes .= "i"; // Assuming QueueId is an integer; adjust accordingly
                $bindParams[] = $QueueId;
            }

            $stmt = $conn->prepare($query);

            if (!$stmt) {
                throw new Exception("Query preparation failed: " . $conn->error);
            }

            $stmt->bind_param($paramTypes, ...$bindParams);

            if (!$stmt->execute()) {
                throw new Exception("Query execution failed: " . $stmt->error);
            }

            $result = $stmt->get_result();

            // Fetch data and provide success response
            $data = $result->fetch_all(MYSQLI_ASSOC);
            $response = array("status" => "success", "data" => $data);
            echo json_encode($response);

            // Close the prepared statement
            $stmt->close();

        } catch (Exception $ex) {
            $response = array("status" => "failed", "error" => $ex->getMessage());
            echo json_encode($response);
        } finally {
            // Close the database connection
            $conn->close();
        }
    } else {
        $response = array("status" => "failed", "error" => "Either DriversId or QueueId should be provided in the request");
        echo json_encode($response);
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
