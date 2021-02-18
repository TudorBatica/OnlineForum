# Forum
![.NET workflow badge](https://github.com/TudorBatica/OnlineForum/workflows/.NET/badge.svg) 
![.NET workflow badge](https://github.com/TudorBatica/OnlineForum/workflows/Node.js%20CI/badge.svg)  

![SQL Server badge](https://img.shields.io/badge/Powered%20by-SQL%20Server-blue)
![.NET badge](https://img.shields.io/badge/Built%20with-.NET%20Core-blueviolet)
![React.js badge](https://img.shields.io/badge/Completed%20with-React.js-blue)

An online forum where users can discuss their careers.  

## Getting started

### API
The API implementation can be found in the `api/` directory. 
To use it:  
1. Restore `appsettings.json`  
  - in order to use the API, the `appsettings.json` file must be created, as it is not included in this repository.
  - a file called `appsettingssample.json` has been included that illustrates how to create `appsettings.json`.  
2. Restore dependencies:  
  - use `dotnet restore`
3. Deployment:  
  - locally: use `dotnet watch run`; it will start at `https://localhost:5001` and `http://localhost:5000`.  
  - dockerize 🐳: `Dockerfile` and `.dockerignore` files are included.  
  
⚠️ Warning: For testing purposes, the API has Cross-Origin Resource Sharing enabled for all hosts. This may represent a big security issue. CORS can be disabled in the `Startup.cs` file. You can read more about CORS risks in this [article](https://medium.com/@ehayushpathak/security-risks-of-cors-e3f4a25c04d7).

### Single Page Application
The implementation is located in the `app/` directory.  
To use it:  
1. Restore `.env`  
2. Restore dependencies:  
  - use `npm i`  
3. Deployment:  
  - locally: use `npm start`
