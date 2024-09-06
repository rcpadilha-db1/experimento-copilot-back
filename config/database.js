const { Sequelize } = require('sequelize');


// Create a new Sequelize instance
const sequelize = new Sequelize('caronaDb', 'siuari', '123456', {
    host: 'localhost',
    dialect: 'postgres', // Change to 'mysql', 'sqlite', 'mariadb', 'mssql' as needed
});

module.exports = sequelize;