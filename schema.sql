CREATE TABLE IF NOT EXISTS candidate (
	id serial PRIMARY KEY,
	email varchar(255) NOT NULL,
	password varchar(500) NOT NULL,
	name varchar(255) NOT NULL,
	birth_date date NOT NULL,
	cpf varchar(11) NOT NULL,
	telephone varchar(11) NOT NULL,
	address varchar(500) NOT NULL,
	creation_date date NOT NULL,
	last_update_date date NOT NULL
);

CREATE TABLE IF NOT EXISTS institution_line_of_work(
	id serial PRIMARY,
	name varchar(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS institution(
	id serial PRIMARY KEY,
	line_of_work_id int NOT NULL,
	email varchar(255) NOT NULL,
	password varchar(500) NOT NULL,
	name varchar(255) NOT NULL,
	cnpj varchar(18) NOT NULL,
  	telephone varchar(11) NOT NULL,
	address varchar(500) NOT NULL,
	creation_date date NOT NULL,
	last_update_date date NOT NULL,

	FOREIGN KEY(line_of_work_id)
		REFERENCES institution_line_of_work(id)
);

/*
tabela para requisitos?
tabela para causas?*/
CREATE TABLE IF NOT EXISTS volunteer_opportunity(
	id serial PRIMARY KEY,
	institution_id int NOT NULL,
	name varchar(255) NOT NULL,
	description varchar(500),
	address varchar(500) NOT NULL,
	opportunity_date date NOT NULL,
	creation_date date NOT NULL,
	last_update_date date NOT NULL,

	FOREIGN KEY(institution_id)
		REFERENCES institution(id)
);

CREATE TABLE IF NOT EXISTS participation_certificate(
	id serial PRIMARY KEY,
	candidate_id int NOT NULL,
	volunteer_opportunity_id int NOT NULL,	
	creation_date date NOT NULL,
	
	FOREIGN KEY(candidate_id)
		REFERENCES candidate(id),
	
	FOREIGN KEY(volunteer_opportunity_id)
		REFERENCES volunteer_opportunity(id)
);
