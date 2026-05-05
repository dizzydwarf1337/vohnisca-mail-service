FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY . .
WORKDIR /app/vohnisca-mail-service
EXPOSE 5001
CMD ["dotnet", "watch"]
