FROM debian:sid-slim

ARG FEDERATION_VERSION

ENV FEDERATION_VERSION ${FEDERATION_VERSION}
ENV DB_TYPE ${DB_TYPE}
ENV DB_URI ${DB_URI}
ENV FEDERATION_PORT ${FEDERATION_PORT}
ENV NGINX_SERVER_NAME ${GINX_SERVER_NAME}
ENV RECEIVING_ACCOUNT ${RECEIVING_ACCOUNT}
ENV CREATE_ACCOUNTS_TABLE ${CREATE_ACCOUNTS_TABLE}
ENv POPULATE_ACCOUNTS_TABLE $POPULATE_ACCOUNTS_TABLE

RUN mkdir -p /usr/share/man/man1/ && touch /usr/share/man/man1/sh.distrib.1.gz #Fix weird upgrade bug with sid-slim
RUN apt-get update -y && apt-get dist-upgrade -y && apt-get install wget postgresql-client netcat-openbsd -y

WORKDIR /

RUN wget -O federation.tar.gz https://github.com/stellar/go/releases/download/federation-v${FEDERATION_VERSION}/federation-v${FEDERATION_VERSION}-linux-amd64.tar.gz

RUN tar xf federation.tar.gz

WORKDIR /federation-v${FEDERATION_VERSION}-linux-amd64

RUN chmod u+x federation

COPY federation.cfg.sh .

CMD until nc -z federation_db 5432; do sleep 1; done && sh federation.cfg.sh > federation.cfg && ./federation