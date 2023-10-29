CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    priority INT NOT NULL,
    due_date DATE NOT NULL,
    assigned_user_id INT,
    workflow_id INT,
    status TEXT NOT NULL
);

CREATE TABLE workflows (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    status TEXT NOT NULL,
    created_by INT
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    email TEXT NOT NULL,
    password TEXT NOT NULL
);

CREATE TABLE groups (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT
);

CREATE TABLE task_status_log (
    id SERIAL PRIMARY KEY,
    task_id INT NOT NULL,
    status TEXT NOT NULL,
    timestamp TIMESTAMP NOT NULL
);

ALTER TABLE tasks ADD CONSTRAINT fk_tasks_assigned_user FOREIGN KEY (assigned_user_id) REFERENCES users (id);

ALTER TABLE tasks ADD CONSTRAINT fk_tasks_workflow FOREIGN KEY (workflow_id) REFERENCES workflows (id);

ALTER TABLE users ADD CONSTRAINT fk_users_created_by FOREIGN KEY (created_by) REFERENCES users (id);
