# TodoApp

This is a full-stack app with Dockerized frontend and backend.
It is designed to be simple and portable.


## Guide
### First clone the repository:
	git clone https://github.com/elyytsepp/todo-list.git
	cd todo-list

### Then build and start the app:
	docker-compose up --build

### The ports will be set as follows:
	Frontend: http://localhost:3000
	Backend: http://localhost:8080/api/todo
 If you open these addresses in your browser you should see the app running and API responding.

### To stop the app:
	docker-compose down

## Thought process / Reasoning for tech-stack choices
#### I used SQLite for the database, .NET for the backend and React for the frontend of my app.
#### SQLite - because I am familiar with SQL from my school projects and could reference those to build this app. Additionally, SQLite can run on its own with the main app and saves the database to a database file in the project directory. Previously I have made use of SQLServer but that would have been overkill for this lightweight app.
#### .NET - because I have the most experience with it and I again have past projects to draw from. Also I feel it is more versatile than Java for this use case.
#### React - this was my first time using React. I found it to be an intuitive and powerful tool with which to build a frontend. I am sure to make better use of it in future projects.
