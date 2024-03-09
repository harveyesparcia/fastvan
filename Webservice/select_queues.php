<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Check if DriversId is provided in the POST request
    if (isset($_POST["DriversId"])) {
        $DriversId = $_POST["DriversId"];

        $conn = new mysqli($servername, $username, $password, $database);

        // Check for database connection errors
        if ($conn->connect_error) {
            $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
            echo json_encode($response);
        } else {
            // Retrieve all values for the specified DriversId
            $query = "SELECT * FROM Queues WHERE DriversId = ?";
            $stmt = $conn->prepare($query);
            $stmt->bind_param("s", $DriversId);
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
        $response = array("status" => "failed", "error" => "DriversId not provided in the request");
        echo json_encode($response);
    }
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
