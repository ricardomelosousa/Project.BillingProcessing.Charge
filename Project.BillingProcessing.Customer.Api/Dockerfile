#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project.BillingProcessing.Charge.Api/Project.BillingProcessing.Charge.Api.csproj", "Project.BillingProcessing.Charge.Api/"]
RUN dotnet restore "Project.BillingProcessing.Charge.Api/Project.BillingProcessing.Charge.Api.csproj"
COPY . .
WORKDIR "/src/Project.BillingProcessing.Charge.Api"
RUN dotnet build "Project.BillingProcessing.Charge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.BillingProcessing.Charge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.BillingProcessing.Charge.Api.dll"]