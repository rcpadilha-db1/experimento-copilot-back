const express = require('express');
const { DataTypes } = require('sequelize');
const sequelize = require('./config/database'); // Corrected path
const User = require('./models/user'); // Corrected path
const Ride = require('./models/ride'); // Import the Ride model
const Vehicle = require('./models/vehicle'); // Import the Vehicle model

// Initialize Express app
const app = express();
app.use(express.json());

// Test the database connection
sequelize.authenticate()
  .then(() => {
    console.log('Connection has been established successfully.');
  })
  .catch(err => {
    console.error('Unable to connect to the database:', err);
  });

// Define API routes
app.get('/', (req, res) => {
  res.send('Hello, World!');
});

app.get('/api/ola', (req, res) => {
  res.send('Hello, World!');
});

// Endpoint to list all users
app.get('/api/users', async (req, res) => {
  try {
    const users = await User.findAll();
    res.json(users);
  } catch (err) {
    console.error('Error fetching users:', err);
    res.status(500).send('Internal Server Error');
  }
});

app.post('/api/rides', async (req, res) => {
  const { rider, vehicle, date } = req.body;

  try {
    // Check if the ride is scheduled for a past date
    const today = new Date().toISOString().split('T')[0];
    if (date < today) {
      return res.status(400).json({ error: 'Ride cannot be scheduled for a past date' });
    }

    // Check if the user already has a ride scheduled for the same date
    const existingRide = await Ride.findOne({ where: { rider, date } });
    if (existingRide) {
      return res.status(400).json({ error: 'User already has a ride scheduled for this date' });
    }

    // Check if the vehicle has more rides than its capacity
    const vehicleRide = await Vehicle.findByPk(vehicle);
    if (!vehicleRide) {
      return res.status(400).json({ error: 'Vehicle not found' });
    }

    const ridesOnSameDate = await Ride.count({ where: { vehicle, date } });
    if (ridesOnSameDate >= vehicleRide.capacity) {
      return res.status(400).json({ error: 'Vehicle has reached its capacity for the day' });
    }

    // Create a new ride
    const newRide = await Ride.create({ rider, vehicle, date });
    res.status(201).json(newRide);
  } catch (err) {
    console.error('Error inserting ride:', err);
    res.status(500).send('Internal Server Error');
  }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});