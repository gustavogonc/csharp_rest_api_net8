#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RestWithASP-NET8Udemy/RestWithASP-NET8Udemy.csproj", "RestWithASP-NET8Udemy/"]
RUN dotnet restore "RestWithASP-NET8Udemy/RestWithASP-NET8Udemy.csproj"
COPY . .
WORKDIR "/src/RestWithASP-NET8Udemy"
RUN dotnet build "RestWithASP-NET8Udemy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestWithASP-NET8Udemy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithASP-NET8Udemy.dll"]