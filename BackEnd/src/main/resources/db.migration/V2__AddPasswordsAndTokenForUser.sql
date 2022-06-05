CREATE TABLE passwords(
    id integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    value varchar
);

ALTER TABLE users ADD login varchar;
ALTER TABLE users ADD token varchar;
ALTER TABLE users ADD password_id integer REFERENCES passwords;