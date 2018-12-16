FROM debian:sid-slim

ARG BRIDGE_VERSION

ENV BRIDGE_VERSION ${BRIDGE_VERSION}
ENV HORIZON ${HORIZON}
ENV PASSPHRASE ${PASSPHRASE}
ENV DB_TYPE ${DB_TYPE}
ENV DB_URI ${DB_URI}
ENV BRIDGE_PORT ${BRIDGE_PORT}
ENV COMPLIANCE_URL ${COMPLIANCE_URL}
ENV RECEIVE_URL ${RECEIVE_URL}
ENV ISSUER ${ISSUER}
ENV BASE_SEED ${BASE_SEED}
ENV RECEIVING_ACCOUNT ${RECEIVING_ACCOUNT}

WORKDIR /

RUN mkdir -p /usr/share/man/man1/ && touch /usr/share/man/man1/sh.distrib.1.gz #Fix weird upgrade bug with sid-slim
RUN apt-get update -y && apt-get dist-upgrade -y && apt-get install wget -y && apt-get install netcat-openbsd -y

RUN wget -O bridge.tar.gz https://github.com/stellar/bridge-server/releases/download/v${BRIDGE_VERSION}/bridge-v${BRIDGE_VERSION}-linux-amd64.tar.gz

RUN tar xf bridge.tar.gz

WORKDIR /bridge-v${BRIDGE_VERSION}-linux-amd64

RUN chmod u+x bridge

COPY bridge.cfg.sh .

CMD until nc -z bridge_db 5432; do sleep 1; done && sh bridge.cfg.sh > bridge.cfg && ./bridge --migrate-db && ./bridge