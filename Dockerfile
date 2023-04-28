FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
#EXPOSE 8080

# Copy csproj e restaurar as camadas
COPY *.csproj ./
RUN dotnet restore

#Copia tudo e realiza o build
COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Usa porta dinâmica do Heroku
#CMD ASPNETCORE_URLS="http://*:$PORT" dotnet api--custom-system.dll


#Default
ENTRYPOINT ["dotnet", "api.custom.system.dll"]
