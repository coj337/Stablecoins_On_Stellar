﻿FROM debian:sid-slim

ARG COMPLIANCE_VERSION

ENV COMPLIANCE_VERSION ${COMPLIANCE_VERSION}
ENV DB_TYPE ${DB_TYPE}
ENV DB_URI ${DB_URI}
ENV COMPLIANCE_EXTERNAL_PORT ${COMPLIANCE_EXTERNAL_PORT}
ENV COMPLIANCE_INTERNAL_PORT ${COMPLIANCE_INTERNAL_PORT}
ENV SIGNING_SEED ${SIGNING_SEED}
# ENV ENCRYPTION_KEY ${ENCRYPTION_KEY}
ENV SANCTIONS_URL ${SANCTIONS_URL}
ENV ASK_USER_URL ${ASK_USER_URL}
ENV FETCH_INFO_URL ${FETCH_INFO_URL}
ENV PASSPHRASE ${PASSPHRASE}
ENV COMPLIANCE_NODE_PORT ${COMPLIANCE_NODE_PORT}

WORKDIR /

RUN mkdir -p /usr/share/man/man1/ && touch /usr/share/man/man1/sh.distrib.1.gz #Fix weird upgrade bug with sid-slim
RUN apt-get update -y && apt-get dist-upgrade -y && apt-get install wget netcat-openbsd -y

RUN wget -O compliance.tar.gz https://github.com/stellar/bridge-server/releases/download/v${COMPLIANCE_VERSION}/compliance-v${COMPLIANCE_VERSION}-linux-amd64.tar.gz

RUN tar xf compliance.tar.gz

WORKDIR /compliance-v${COMPLIANCE_VERSION}-linux-amd64

RUN chmod u+x compliance

COPY compliance.cfg.sh .

CMD until nc -z compliance_db 5432; do sleep 1; done && sh compliance.cfg.sh > compliance.cfg && ./compliance --migrate-db && ./compliance