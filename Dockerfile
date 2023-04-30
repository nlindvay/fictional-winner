FROM mcr.microsoft.com/dotnet/core/sdk:6.0.15 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Application/Fw.Application.Wms/*.csproj ./src/Application/Fw.Application.Wms/
COPY src/Application/Fw.Application.Tms/*.csproj ./src/Application/Fw.Application.Tms/
COPY src/Application/Fw.Application.Ams/*.csproj ./src/Application/Fw.Application.Ams/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY Experiment/. ./Experiment/
COPY Experiment.Procedure/. ./Experiment.Procedure/
COPY Experiment.Sample/. ./Experiment.Sample/ 
#
WORKDIR /app/Experiment
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0.15 AS runtime
WORKDIR /app 
#
COPY --from=build /app/Experiment/out ./
ENTRYPOINT ["dotnet", "Experiment.dll"]