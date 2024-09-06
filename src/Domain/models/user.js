const { Model, DataTypes } = require('sequelize');
const sequelize = require('../../Infraestructure/config/database'); // Adjust the path as needed

class User extends Model {}

User.init({
  id: {
    type: DataTypes.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    allowNull: false
  },
  name: {
    type: DataTypes.STRING,
    allowNull: false
  },
  email: {
    type: DataTypes.STRING,
    allowNull: false
  }
}, {
  sequelize,
  modelName: 'User',
  tableName: 'user',
  timestamps: false
});

module.exports = User;