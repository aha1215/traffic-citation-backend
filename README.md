
# Introduction
A .NET 6 Web API to be used with our [Angular application](https://github.com/PedroG1018/Capstone-FA22-Traffic-Citation-Web-Interface)<br>

Frameworks: Entity Framework Core (Object Database Mapper) <br>
Database: PostgreSQL<br>
CI/CD pipeline. Deployed on Heroku. [Using Jincod .NET Core Buildpack](https://github.com/jincod/dotnetcore-buildpack)<br>

## Database ERD
![ERD](https://user-images.githubusercontent.com/80275985/199389587-d807de35-3ae5-4795-b875-378fadedf371.png)

<hr>

## API Endpoints <br>
https://traffic-citation-backend.herokuapp.com<br>

### Citation
GET - Retrieve all citations<br>
/api/Citation<br>

GET - Retrieve citations based on page number and page size<br>
/api/Citation/{pageNumber}/{pageSize}<br>

POST - Create a citation<br>
/api/Citation<br>

PUT - Update a citation<br>
/api/Citation<br>

DELETE - Delete a citation by id<br>
/api/Citation/{id}<br>

<hr>

### Driver
GET - Retrieve all drivers<br>
/api/Driver<br>

POST - Create a driver<br>
/api/Driver<br>

PUT - Update a driver<br>
/api/Driver<br>

GET - Retrieve driver by id<br>
/api/Driver/{id}<br>

DELETE - Delete a driver by id<br>
/api/Driver/{id}<br>

<hr>

### User
GET - Retrieve all users<br>
/api/User<br>

POST - Create a new user<br>
/api/User<br>

PUT - Update a user<br>
/api/User<br>

DELETE - Delete a user by id<br>
/api/User/{id}<br>

<hr>

## Learning Resources 
- [CRUD with Angular 14 & .NET 6 Web API, EF Core & SQL Server](https://www.youtube.com/watch?v=dtthbiP3SE0)
