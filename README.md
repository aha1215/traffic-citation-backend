
# Introduction
A .NET 6 Web API to be used with our [Angular application](https://github.com/PedroG1018/Capstone-FA22-Traffic-Citation-Web-Interface)<br>

Frameworks: Entity Framework Core (Object Database Mapper) <br>
Database: PostgreSQL<br>
CI/CD pipeline. Deployed on Heroku. [Using Jincod .NET Core Buildpack](https://github.com/jincod/dotnetcore-buildpack)<br>

## Database ERD

![ERD](https://user-images.githubusercontent.com/80275985/206373144-b9a9edf5-488c-4efe-a422-9f0bdd8e889f.png)

<hr>

## API Endpoints <br>
https://traffic-citation-backend.herokuapp.com<br>

### Citation
GET - Retrieve all citations<br>
/api/Citation<br>

GET - Retrieve list of citations for pagination based on page number, page size, user id, and user role<br>
/api/Citation/{pageNumber}/{pageSize}/{userId}/{userRole}<br>

POST - Create a citation with violations<br>
/api/CitationWithViolations<br>

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

GET - Retrieve driver by license number<br>
/api/Driver/license/{license_no}<br>

GET - Retrieve driver by id<br>
/api/Driver/{id}<br>

DELETE - Delete a driver by id<br>
/api/Driver/{id}<br>

<hr>

### Violation
GET - Retrieve all violations<br>
/api/Violation<br>

POST - Create a violation<br>
/api/Violation<br>

PUT - Update a violation<br>
/api/Violation<br>

GET - Get violation by citation id<br>
/api/Violation/{citation_id}<br>

PUT - Update a list of violations<br>
/api/Violation/violations<br>

DELETE - Delete a list of violations<br>
/api/Violation/violations<br>

DELETE - Delete a violation by id<br>
/api/Violation/{id}<br>

<hr>

## Learning Resources 
- [CRUD with Angular 14 & .NET 6 Web API, EF Core & SQL Server](https://www.youtube.com/watch?v=dtthbiP3SE0)
