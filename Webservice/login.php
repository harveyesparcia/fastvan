<?php

$servername = "localhost";
$username = "cctcccsn_u_asimudin";
$password = "09977163080SJ";
$database = "cctcccsn_aasimudin";

if ($_SERVER['REQUEST_METHOD'] === 'POST' || $_SERVER['REQUEST_METHOD'] === 'GET') {
    $userusername = @$_REQUEST["username"];
    $userpassword = @$_REQUEST["password"];
    
    // Output received data for debugging
    $debugInfo = array("username" => $userusername, "password" => $userpassword);

    $conn = new mysqli($servername, $username, $password, $database);

    // Check for database connection errors
    if ($conn->connect_error) {
        $response = array("error" => "Database connection error: " . $conn->connect_error);
        echo json_encode($response);
    } else {
        // Hash the provided password if it's stored hashed in the database
        // $hashedPassword = password_hash($userpassword, PASSWORD_BCRYPT);

        // Use prepared statement to prevent SQL injection
        $stmt = $conn->prepare("SELECT * FROM account WHERE username=? AND password=?");
        $stmt->bind_param("ss", $userusername, $userpassword);
        $stmt->execute();
        $result = $stmt->get_result();

        // Check for query errors
        if (!$result) {
            $response = array("error" => "Database query error: " . $stmt->error);
            echo json_encode($response);
        } else {
            if ($result->num_rows == 1) {
                $row = $result->fetch_assoc();
                $response = array(
                    "role" => getRole($row['typeofaccount']),
                    "username" => $row['username'],
                    "password" => $row['password'],
                    "Email" => $row['Email'],
                    "firstname" => $row['firstname'],
                    "lastname" => $row['lastname'],
                    "BirthDate" => $row['BirthDate'],
                    "ContactNumber" => $row['ContactNumber'],
                    "Address" => $row ['Address'],
                    "IsResign" => $row ['IsResign'],
                    "IsBlocked" => $row ['IsBlocked'],
                    "Avatar" => $row ['Avatar'],
                    "DriversId" => $row ['DriversId'],
                    "PassengersId" => $row ['PassengersId'],
                    "PlateNumber" => $row ['PlateNumber'],
                    "DriversLicenseNumber" => $row ['DriversLicenseNumber'],
                    
                );
                echo json_encode($response);
            } else {
                $response = array("error" => "Failed to login");
                echo json_encode($response);
            }
        }

        // Close the prepared statement
        $stmt->close();

        // Close the database connection
        $conn->close();
    }
} else {
    $response = array("error" => "Invalid request method");
    echo json_encode($response);
}

// Function to determine the role based on typeofaccount value
function getRole($accountType) {
    switch ($accountType) {
        case 1:
            return "Admin";
        case 3:
            return "Driver";
        default:
            return "Passenger";
    }
}
?>