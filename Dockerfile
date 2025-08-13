FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-stage

WORKDIR /app
COPY src .

RUN dotnet build ClassNamer.sln -c Release
RUN dotnet test --logger:"console;verbosity=detailed"
RUN dotnet publish /app/ClassNamer/ClassNamer.csproj -c Release -o /out

###
FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy-chiseled

COPY --from=build-stage /out /out

LABEL MAINTAINER="Pawel.Trzcinski@onet.eu"
LABEL SUMMARY="ClassNamer RestService"

# serviceui 
EXPOSE 15400 

WORKDIR /out
ENTRYPOINT [ "dotnet", "ClassNamer.dll" ]