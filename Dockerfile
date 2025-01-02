FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /api
EXPOSE 80

COPY *.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final

WORKDIR /api
COPY --from=build /api/out .
ENTRYPOINT [ "dotnet", "backend.dll" ]