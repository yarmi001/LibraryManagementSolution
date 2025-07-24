FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LibraryManagement.WebApi/LibraryManagement.WebApi.csproj", "LibraryManagement.WebApi/"]
COPY ["LibraryManagement.Domain/LibraryManagement.Domain.csproj", "LibraryManagement.Domain/"]
COPY ["LibraryManagement.Application/LibraryManagement.Application.csproj", "LibraryManagement.Application/"]
COPY ["LibraryManagement.Infrastructure/LibraryManagement.Infrastructure.csproj", "LibraryManagement.Infrastructure/"]
RUN dotnet restore "LibraryManagement.WebApi/LibraryManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/LibraryManagement.WebApi"
RUN dotnet build "LibraryManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryManagement.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagement.WebApi.dll"]