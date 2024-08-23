# Running the Application

## Using Docker

1. Build and start the containers:

```shell
docker compose up --build
```

The application should now be running at http://localhost:3000.

# Running Tests

Run the following command to execute the test suite:

```shell
❯ npx jest
PASS  src/user/user.service.spec.ts
PASS  src/ride/ride.controller.spec.ts
PASS  src/vehicle/vehicle.service.spec.ts
PASS  src/user/user.controller.spec.ts
PASS  src/ride/ride.service.spec.ts
PASS  src/vehicle/vehicle.controller.spec.ts

Test Suites: 6 passed, 6 total
Tests:       55 passed, 55 total
Snapshots:   0 total
Time:        3.808 s, estimated 5 s
Ran all test suites.
```
# Endpoints

The Postman collection can be found in the `github-copilot-experiment.postman_collection.json` file.


## User Endpoints

### Create User
**URL:** `/user`  
**Method:** `POST`  
**Request Body:**
```json
{
  "name": "string",
  "email": "string"
}
```
**Response:**
```json
{
  "id": "number",
  "name": "string",
  "email": "string"
}
```
**Possible Errors:**
- `400 Bad Request:` User creation failed

### Get All Users
**URL:** `/user`  
**Method:** `GET`  
**Response:**
```json
[
  {
    "id": "number",
    "name": "string",
    "email": "string"
  }
]
```

### Get User by ID
**URL:** `/user/:id`  
**Method:** `GET`  
**Response:**
```json
{
  "id": "number",
  "name": "string",
  "email": "string"
}
```
**Possible Errors:**
- `404 Not Found:` User not found

### Update User
**URL:** `/user/:id`  
**Method:** `PUT`  
**Request Body:**
```json
{
  "name": "string",
  "email": "string"
}
```
**Response:**
```json
{
  "id": "number",
  "name": "string",
  "email": "string"
}
```
**Possible Errors:**
- `404 Not Found:` User not found

### Delete User
**URL:** `/user/:id`  
**Method:** `DELETE`  
**Response:** `204 No Content`  
**Possible Errors:**
- `404 Not Found:` User not found

## Vehicle Endpoints

### Create Vehicle
**URL:** `/vehicle`  
**Method:** `POST`  
**Request Body:**
```json
{
  "plate": "string",
  "ownerId": "number",
  "capacity": "number"
}
```
**Response:**
```json
{
  "id": "number",
  "plate": "string",
  "ownerId": "number",
  "capacity": "number"
}
```
**Possible Errors:**
- `400 Bad Request:` Invalid owner

### Get All Vehicles
**URL:** `/vehicle`  
**Method:** `GET`  
**Response:**
```json
[
  {
    "id": "number",
    "plate": "string",
    "ownerId": "number",
    "capacity": "number"
  }
]
```

### Get Vehicle by ID
**URL:** `/vehicle/:id`  
**Method:** `GET`  
**Response:**
```json
{
  "id": "number",
  "plate": "string",
  "ownerId": "number",
  "capacity": "number"
}
```
**Possible Errors:**
- `404 Not Found:` Vehicle not found

### Update Vehicle
**URL:** `/vehicle/:id`  
**Method:** `PUT`  
**Request Body:**
```json
{
  "plate": "string",
  "ownerId": "number",
  "capacity": "number"
}
```
**Response:**
```json
{
  "id": "number",
  "plate": "string",
  "ownerId": "number",
  "capacity": "number"
}
```
**Possible Errors:**
- `404 Not Found:` Vehicle not found

### Delete Vehicle
**URL:** `/vehicle/:id`  
**Method:** `DELETE`  
**Response:** `204 No Content`  
**Possible Errors:**
- `404 Not Found:` Vehicle not found

## Ride Endpoints

### Create Ride
**URL:** `/ride`  
**Method:** `POST`  
**Request Body:**
```json
{
  "userId": "number",
  "vehicleId": "number",
  "date": "string (YYYY-MM-DD)"
}
```
**Response:**
```json
{
  "id": "number",
  "userId": "number",
  "vehicleId": "number",
  "date": "string (YYYY-MM-DD)"
}
```

**Possible Errors**
- 400 Bad Request: Invalid user
- 400 Bad Request: Invalid vehicle
- 400 Bad Request: Vehicle has no available capacity for the selected day
- 400 Bad Request: User already has a ride on the selected day

### Get All Rides
**URL:** `/ride`  
**Method:** `GET`  
**Response:**
```json
[
  {
    "id": "number",
    "userId": "number",
    "vehicleId": "number",
    "date": "string (YYYY-MM-DD)"
  }
]
```

### Get Ride by ID
**URL:** `/ride/:id`  
**Method:** `GET`  
**Response:**
```json
{
  "id": "number",
  "userId": "number",
  "vehicleId": "number",
  "date": "string (YYYY-MM-DD)"
}
```

### Update Ride
**URL:** `/ride/:id`  
**Method:** `PUT`  
**Request Body:**
```json
{
  "userId": "number",
  "vehicleId": "number",
  "date": "string (YYYY-MM-DD)"
}
```
**Response:**
```json
{
  "id": "number",
  "userId": "number",
  "vehicleId": "number",
  "date": "string (YYYY-MM-DD)"
}
```

### Delete Ride
**URL:** `/ride/:id`  
**Method:** `DELETE`  
**Response:** `204 No Content`
