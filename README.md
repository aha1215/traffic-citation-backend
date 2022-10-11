
# Introduction 
A .NET 6 Web API to be used with our [Angular application](https://github.com/PedroG1018/Capstone-FA22-Traffic-Citation-Web-Interface)<br>

Frameworks: Entity Framework Core (Object Database Mapper) <br>
Database: PostgreSQL<br>
CI/CD pipeline. Deployed on Heroku. [Using Jincod .NET Core Buildpack](https://github.com/jincod/dotnetcore-buildpack)<br>

## Database ERD
![ERD](https://user-images.githubusercontent.com/80275985/195202268-a12f1d8e-c8b2-4fc2-8b99-d4ef61c3629b.png)

<hr>

## API Endpoints <br>
https://traffic-citation-backend.herokuapp.com<br>

### Citation
GET - Retrieves all citations<br>
/api/Citation<br>

POST - Creates a Citation<br>
/api/Citation<br>

PUT - Upates a Citation<br>
/api/Citation<br>

DELETE - Deletes a citation by id<br>
/api/Citation/{id}<br>

<hr>

### Driver
GET - Retrieves all drivers<br>
/api/Driver<br>

POST - Create a driver<br>
/api/Driver<br>

PUT - Update a driver<br>
/api/Driver<br>

GET - Retrieves driver by id<br>
/api/Driver/{id}<br>

DELETE - Deletes a driver by id<br>
/api/Driver/{id}<br>

<hr>

### User
GET - Retrieves all users<br>
/api/User<br>

POST - Create a new user<br>
/api/User<br>

PUT - Update a user<br>
/api/User<br>

DELETE - Deletes a user by id<br>
/api/User/{id}<br>

<hr>

## Learning Resources 
- [CRUD with Angular 14 & .NET 6 Web API, EF Core & SQL Server](https://www.youtube.com/watch?v=dtthbiP3SE0)
