const sequelize = require('../config/database');
const User = require('../models/user');
const Vehicle = require('../models/vehicle');

async function seedDatabase() {
  try {
    await sequelize.authenticate();
    console.log('Connection has been established successfully.');

    // Synchronize models with the database
    await sequelize.sync({ force: true }); // Use force: true to drop and recreate tables
    console.log('Database synchronized.');

    // Seed data for users
    const users = [
      {  id: 1, name: 'John Doe', email: 'john.doe@example.com' },
      { id: 2, name: 'Jane Smith', email: 'jane.smith@example.com' },
      { id: 3, name: 'Alice Johnson', email: 'alice.johnson@example.com' },
      { id: 4, name: 'Bob Johnson', email: 'bob.johnson@example.com' },
      { id: 5, name: 'Sarah Davis', email: 'sarah.davis@example.com' }
    ];

    // Seed data for vehicles
    const vehicles = [
      { plate: 'ABC1234',  owner: 1, capacity: 3 },
      { plate: 'ABS3456',  owner: 2, capacity: 4 },
      { plate: 'ABS4587',  owner: 3, capacity: 2 }
    ];

    // Insert users into the database
    await User.bulkCreate(users);
    console.log('Users have been inserted.');

    // Insert vehicles into the database
    await Vehicle.bulkCreate(vehicles);
    console.log('Vehicles have been inserted.');

    console.log('Database seeding completed.');
  } catch (error) {
    console.error('Unable to connect to the database:', error);
  } finally {
    await sequelize.close();
  }
}

seedDatabase();