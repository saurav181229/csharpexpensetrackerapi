# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
# Listen on all interfaces, container port 8080
ENV DOTNET_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ExpenseTrackerAPI.dll"]
