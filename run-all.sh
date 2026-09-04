
#!/bin/bash


trap 'echo "Stopping all microservices..."; kill 0' EXIT INT TERM

echo "Starting all projects..."

dotnet run --project ./customerApi/customerApi.csproj &
dotnet run --project ./accountApi/accountApi.csproj &
dotnet run --project ./transactionApi/transactionApi.csproj &
dotnet run --project ./authApi/authApi.csproj &



echo "Waiting for services to bind to ports..."
sleep 5

echo "=========================================="
echo "      ACTIVE MICROSERVICE PORTS          "
echo "=========================================="


ss -tulpn | grep "dotnet"

echo "=========================================="


wait
