#!/bin/bash

sleep 20

# Run the setup script to create the DB and the schema in the DB
echo "Running /scripts/init.sql"
/opt/mssql-tools18/bin/sqlcmd -S localhost,1433 -U sa -P $MSSQL_SA_PASSWORD -C -d master -i /scripts/init.sql

# Create default admin login
echo "Creating default admin login"
/opt/mssql-tools18/bin/sqlcmd -S localhost,1433 -U sa -P $MSSQL_SA_PASSWORD -C -d master -Q "\
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = '$DB_ADMIN') \
BEGIN \
    CREATE LOGIN [$DB_ADMIN] WITH PASSWORD = '$DB_ADMIN_PASSWORD', CHECK_POLICY = OFF; \
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [$DB_ADMIN]; \
END;"

# Create default app login
echo "Creating default app login"
/opt/mssql-tools18/bin/sqlcmd -S localhost,1433 -U sa -P $MSSQL_SA_PASSWORD -C -d master -Q "\
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = '$DB_USER') \
BEGIN \
    CREATE LOGIN [$DB_USER] WITH PASSWORD = '$DB_USER_PASSWORD', CHECK_POLICY = OFF; \
END;"

# Create default app user if it doesn't exist
# and grant db_datareader and db_datawriter roles for DB CAO_HUB
echo "Adding CAO_HUB roles db_datareader and db_datawriter roles to app user"
/opt/mssql-tools18/bin/sqlcmd -S localhost,1433 -U sa -P $MSSQL_SA_PASSWORD -C -d CAO_HUB -Q "\
IF USER_ID('$DB_USER') IS NULL \
BEGIN \
    CREATE USER [$DB_USER] FOR LOGIN [$DB_USER]; \
END; \
ALTER ROLE [db_datareader] ADD MEMBER [$DB_USER]; \
ALTER ROLE [db_datawriter] ADD MEMBER [$DB_USER];"

echo "Done"
