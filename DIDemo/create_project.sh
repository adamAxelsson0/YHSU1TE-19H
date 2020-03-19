dotnet new sln
mkdir src
dotnet new classlib -o src/DependencyInjectionDemo.Domain
dotnet new xunit -o src/DependencyInjectionDemo.Domain.Tests
dotnet add src/DependencyInjectionDemo.Domain.Tests/DependencyInjectionDemo.Domain.Tests.csproj reference src/DependencyInjectionDemo.Domain/DependencyInjectionDemo.Domain.csproj
dotnet sln add src/DependencyInjectionDemo.Domain/DependencyInjectionDemo.Domain.csproj
dotnet sln add src/DependencyInjectionDemo.Domain.Tests/DependencyInjectionDemo.Domain.Tests.csproj
dotnet add src/DependencyInjectionDemo.Domain.Tests/DependencyInjectionDemo.Domain.Tests.csproj reference src/DependencyInjectionDemo.Domain/DependencyInjectionDemo.Domain.csproj