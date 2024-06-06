#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["POS.Api/POS.Api.csproj", "POS.Api/"]
COPY ["POS.Application/POS.Application.csproj", "POS.Application/"]
COPY ["POS.Domain/POS.Domain.csproj", "POS.Domain/"]
COPY ["POS.Infrastructure/POS.Infrastructure.csproj", "POS.Infrastructure/"]
COPY ["POS.Utilities/POS.Utilities.csproj", "POS.Utilities/"]
RUN dotnet restore "./POS.Api/./POS.Api.csproj"
COPY . .
WORKDIR "/src/POS.Api"
RUN dotnet build "./POS.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./POS.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#RUN ln -fs /usr/share/zoneinfo/America/Lima /etc/localtime 
#RUN dpkg-reconfigure --frontend noninteractive tzdata

ENTRYPOINT ["dotnet", "POS.Api.dll"]