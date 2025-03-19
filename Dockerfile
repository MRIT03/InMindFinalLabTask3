FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./InMindLab3/InMindLab3.csproj ./
RUN dotnet restore ./InMindLab3.csproj

COPY . ./
RUN dotnet publish ./InMindLab3.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "InMindLab3.dll"]
