CREATE TABLE IF NOT EXISTS users
(
    id               bigint primary key generated by default as identity,
    type             text                     NOT NULL default '',
    email            text                     NOT NULL default '',
    password         text                     NOT NULL default '',
    name             text                     NOT NULL default '',
    cpf              text                     NOT NULL default '',
    cnpj             text                     NOT NULL default '',
    telephone        text                     NOT NULL default '',
    street           TEXT                     NOT NUll default '',
    number           TEXT                     NOT NULL default '',
    complement       TEXT                              default '',
    city             TEXT                     NOT NULL default '',
    neighborhood     TEXT                              default '',
    state            TEXT                     NOT NULL default '',
    postal_code      TEXT                     NOT NULL default '',
    birth_date       date                     NOT NULL,
    enabled          boolean                  NOT NULL default false,
    creation_date    timestamp with time zone NOT NULL default now(),
    last_update_date timestamp with time zone
);

CREATE TABLE IF NOT EXISTS qualifications
(
    id   bigint primary key generated by default as identity,
    name text NOT NULL default ''
);

CREATE TABLE IF NOT EXISTS candidates_qualification_link
(
    candidate_id     bigint NOT NULL,
    qualification_id bigint NOT NULL,

    FOREIGN KEY (candidate_id)
        REFERENCES "users" (id),

    FOREIGN KEY (qualification_id)
        REFERENCES qualifications (id)
);

CREATE TABLE IF NOT EXISTS volunteer_opportunities
(
    id               bigint primary key generated by default as identity,
    institution_id   bigint                   NOT NULL,
    name             text                     NOT NULL default '',
    description      text                              default '',
    opportunity_date date                     NOT NULL,
    street           TEXT                     NOT NUll default '',
    number           TEXT                     NOT NULL default '',
    complement       TEXT                              default '',
    city             TEXT                     NOT NULL default '',
    neighborhood     TEXT                              default '',
    state            TEXT                     NOT NULL default '',
    postal_code      TEXT                     NOT NULL default '',
    creation_date    timestamp with time zone NOT NULL default now(),
    last_update_date timestamp with time zone,

    FOREIGN KEY (institution_id)
        REFERENCES "users" (id)
);

CREATE TABLE IF NOT EXISTS qualification_opportunity_link
(
    opportunity_id   bigint NOT NULL,
    qualification_id bigint NOT NULL,

    FOREIGN KEY (opportunity_id)
        REFERENCES volunteer_opportunities (id),

    FOREIGN KEY (qualification_id)
        REFERENCES qualifications (id)
);

CREATE TABLE IF NOT EXISTS opportunity_causes
(
    id   bigint primary key generated by default as identity,
    name text NOT NULL default ''
);

CREATE TABLE IF NOT EXISTS cause_opportunity_link
(
    opportunity_id bigint NOT NULL,
    cause_id       bigint NOT NULL,

    FOREIGN KEY (opportunity_id)
        REFERENCES volunteer_opportunities (id),

    FOREIGN KEY (cause_id)
        REFERENCES opportunity_causes (id)
);

CREATE TABLE IF NOT EXISTS participation_certificates
(
    id                       bigint primary key generated by default as identity,
    candidate_id             bigint                   NOT NULL,
    volunteer_opportunity_id bigint                   NOT NULL,
    creation_date            timestamp with time zone NOT NULL default now(),

    FOREIGN KEY (candidate_id)
        REFERENCES "users" (id),

    FOREIGN KEY (volunteer_opportunity_id)
        REFERENCES volunteer_opportunities (id)
);

CREATE TABLE IF NOT EXISTS confirmation_tokens
(
    id            bigint primary key generated by default as identity,
    user_id       bigint                   not null,
    token         text                     NOT NULL,
    creation_date timestamp with time zone NOT NULL default now(),

    FOREIGN KEY (user_id)
        REFERENCES "users" (id)
);