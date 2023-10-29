**SageFlow: Workflows with the power of AI.**

Name: SageFlow
Tagine: Workflows with the power of AI.
Desc: AI-powered workflow automation for the modern business.
Title: SageFlow: Workflows with the power of AI
Author: Gurudev
Framework: Tornado
Features:
* Task management
* Workflow builder
* Collaboration
* Notifications
* Reporting
* Integrations
Other decisions:
* SageFlow will be an installable Python package.
* SageFlow will have CLI controls.


**Database schema design**

Entities:
* Task
* Workflow
* User
* Group

Attributes:
* Task:
    * id (primary key)
    * name
    * description
    * priority
    * due date
    * assigned user (foreign key to User table)
    * workflow (foreign key to Workflow table)
* Workflow:
    * id (primary key)
    * name
    * description
    * status
    * created by (foreign key to User table)
* User:
    * id (primary key)
    * name
    * email
    * password
* Group:
    * id (primary key)
    * name
    * description

Relationships:
* A task can belong to one workflow.
* A workflow can have many tasks.
* A user can be assigned to many tasks.
* A task can be assigned to one user.
* A user can belong to many groups.
* A group can have many users.


