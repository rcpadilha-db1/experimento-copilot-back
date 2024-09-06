const { Model, DataTypes } = require('sequelize');
const sequelize = require('../config/database'); // Adjust the path as needed

class Vehicle extends Model {}

Vehicle.init({
  id: {
    type: DataTypes.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    allowNull: false
  },
  plate: {
    type: DataTypes.STRING,
    allowNull: false
  },
  owner: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: {
      model: 'user', // Name of the User table
      key: 'id'
    }
  },
  capacity: {
    type: DataTypes.INTEGER,
    allowNull: false
  }
}, {
  sequelize,
  modelName: 'Vehicle',
  tableName: 'vehicle',
  timestamps: false
});

module.exports = Vehicle;