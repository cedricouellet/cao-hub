FROM mcr.microsoft.com/mssql/server:2022-latest

EXPOSE 1433

COPY ./entrypoint.sh /entrypoint.sh
COPY ./scripts/init-db.sh /scripts/init-db.sh
COPY ./scripts/init-db.sql /scripts/init-db.sql

USER root
RUN chmod +x /entrypoint.sh
RUN chmod +x /scripts/init-db.sh

ENTRYPOINT [ "/entrypoint.sh" ]