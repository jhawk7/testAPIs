FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /App
# copy everything
COPY . ./
# restore as distinct layers
RUN dotnet restore
# build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
COPY --from=builder /App/out .
EXPOSE 5256
# set this to override ASPNETCORE_HTTP_PORTS which is 8080 by default
ENV ASPNETCORE_URLS=http://+:5256
ENTRYPOINT ["dotnet", "DotnetAPI.dll"]
