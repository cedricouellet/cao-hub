FROM mcr.microsoft.com/mssql/server:2022-latest

EXPOSE 1433

COPY ./entrypoint.sh /entrypoint.sh
COPY ./scripts/init.sh /scripts/init.sh
COPY ./scripts/init.sql /scripts/init.sql

USER root
RUN chmod +x /entrypoint.sh
RUN chmod +x /scripts/init.sh

ENTRYPOINT [ "/entrypoint.sh" ]