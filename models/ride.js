const { Model, DataTypes } = require('sequelize');
const sequelize = require('../config/database'); // Adjust the path as needed

class Ride extends Model {}

Ride.init({
  id: {
    type: DataTypes.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    allowNull: false
  },
  rider: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: {
      model: 'user', // Name of the User table
      key: 'id'
    }
  },
  vehicle: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: {
      model: 'vehicle', // Name of the Vehicle table
      key: 'id'
    }
  },
  date: {
    type: DataTypes.DATEONLY,
    allowNull: false
  },
}, {
  sequelize,
  modelName: 'Ride',
  tableName: 'ride',
  timestamps: false
});

module.exports = Ride;