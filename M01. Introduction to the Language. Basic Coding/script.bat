dotnet new sln --name MySolution --output MySolution
dotnet new classlib --name ArrayHelper --output MySolution\ArrayHelper
dotnet sln MySolution\MySolution.sln add MySolution\ArrayHelper\ArrayHelper.csproj
dotnet new classlib --name RectangleHelper --output MySolution\RectangleHelper
dotnet sln MySolution\MySolution.sln add MySolution\RectangleHelper\RectangleHelper.csproj
dotnet new console --name LibsDemo --output MySolution\LibsDemo
dotnet sln MySolution\MySolution.sln add MySolution\LibsDemo\LibsDemo.csproj
dotnet build MySolution\MySolution.sln --output MySolution
dotnet publish MySolution\MySolution.sln --arch win-x64 --output MySolution
