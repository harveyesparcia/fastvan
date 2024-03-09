<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $conn = new mysqli($servername, $username, $password, $database);

    if ($conn->connect_error) {
        $response = array("status" => "failed", "error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
        exit;
    }

    if (isset($_POST["DriversId"])) {
        // If DriversId is provided, retrieve data for that specific driver
        $DriversId = $_POST["DriversId"];

        $query = "SELECT * FROM Queues WHERE DriversId = ? AND Status = 1 AND DATE_FORMAT(Date, '%Y-%m-%d') = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ss", $DriversId, date('Y-m-d'));
    } else {
        // If DriversId is not provided, retrieve all data
        $query = "SELECT * FROM Queues WHERE Status = 1 AND DATE_FORMAT(Date, '%Y-%m-%d') = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("s", date('Y-m-d'));
    }

    $stmt->execute();
    $result = $stmt->get_result();

    if ($stmt->error) {
        $response = array("status" => "failed", "error" => "Database query error: " . $stmt->error);
        echo json_encode($response);
    } else {
        $data = $result->fetch_all(MYSQLI_ASSOC);
        $response = array("status" => "success", "data" => $data);
        echo json_encode($response);
    }

    $stmt->close();
    $conn->close();
} else {
    $response = array("status" => "failed", "error" => "Invalid request method");
    echo json_encode($response);
}
?>
