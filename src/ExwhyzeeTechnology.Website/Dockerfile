#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ExwhyzeeTechnology.Website/ExwhyzeeTechnology.Website.csproj", "src/ExwhyzeeTechnology.Website/"]
COPY ["src/ExwhyzeeTechnology.Application/ExwhyzeeTechnology.Application.csproj", "src/ExwhyzeeTechnology.Application/"]
COPY ["src/ExwhyzeeTechnology.Domain/ExwhyzeeTechnology.Domain.csproj", "src/ExwhyzeeTechnology.Domain/"]
COPY ["src/ExwhyzeeTechnology.Infrastructure/ExwhyzeeTechnology.Infrastructure.csproj", "src/ExwhyzeeTechnology.Infrastructure/"]
COPY ["src/ExwhyzeeTechnology.Persistence.EF.SQL/ExwhyzeeTechnology.Persistence.EF.SQL.csproj", "src/ExwhyzeeTechnology.Persistence.EF.SQL/"]
COPY ["src/PostmarkEmailService/PostmarkEmailService.csproj", "src/PostmarkEmailService/"] # Verify this path
COPY ["src/SmsPlatform/SmsPlatform.csproj", "src/SmsPlatform/"] # Verify this path
RUN dotnet restore "./src/ExwhyzeeTechnology.Website/ExwhyzeeTechnology.Website.csproj"
COPY . .
WORKDIR "/src/src/ExwhyzeeTechnology.Website"
RUN dotnet build "./ExwhyzeeTechnology.Website.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExwhyzeeTechnology.Website.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExwhyzeeTechnology.Website.dll"]
