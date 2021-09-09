# Toy Robot Simulator

This is an implementation of a toy robot simulator that runs the following commands:

###### PLACE X,Y,DIRECTION
###### MOVE
###### LEFT
###### RIGHT
###### REPORT

## Requirements
Ensure that you have got [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.400-windows-x64-installer) installed

## Steps to compile and run unit tests

1. Clone the source code to a folder on your computer
2. Compile
```
dotnet build
```
3. Run unit tests
```
dotnet test
```

## Usage
To run the application
```
cd .\src\ToyRobotConsoleApp
dotnet run
```
### Sample output
```
PS C:\Users\ToyRobotSimulator\src\ToyRobotConsoleApp> dotnet run
Toy Robot simulator
========================
Available commands ->
PLACE X,Y,DIRECTION
MOVE
LEFT
RIGHT
REPORT
Ctrl-C to exit application
========================
Let us start

place 1,2,north
report
Output: 1,2,North
move
left
right
report
Output: 1,3,North
place 2,2
report
Output: 2,2,North
move
right
right
report
Output: 2,3,South
```