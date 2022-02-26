# HiLCoE Class Schedule Server
API Server currently being used for the Class Schedule Project for HiLCoE.
This server is an updated iteration of the [Node.js Implementation](https://github.com/brukberhane/class-schedule_server) and it provides a lot more growth and modularity.

## Building
### Dependencies
* Dotnet Core SDK

### General Instructions
* I am currently using VSCode/Vim so this one will work best, you can `cd` into the project directory after cloning and run `dotnet restore` 

## Testing
* To test the application, you must change the `application.Development.json` file to a MongoDB connection URL that's actually a valid url. Then run `dotnet watch` or however you run your projects inside of Visual Studio to get it up

## State of the Project
I'm not currently actively working on it but I'll be paying attention to issues and pull requests because I intend for it to be actively used for quite a while still.
