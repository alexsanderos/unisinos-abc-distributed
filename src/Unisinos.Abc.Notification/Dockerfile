#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Unisinos.Abc.Notification/Unisinos.Abc.Notification.csproj", "src/Unisinos.Abc.Notification/"]
RUN dotnet restore "src/Unisinos.Abc.Notification/Unisinos.Abc.Notification.csproj"
COPY . .
WORKDIR "/src/src/Unisinos.Abc.Notification"
RUN dotnet build "Unisinos.Abc.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unisinos.Abc.Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unisinos.Abc.Notification.dll"]