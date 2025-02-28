#!/bin/bash

/scripts/init-db.sh &
/opt/mssql/bin/sqlservr
