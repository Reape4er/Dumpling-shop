������� ��� ���������� �������� �� ���������� dotnet tool install --global dotnet-ef

������� �������� dotnet ef migrations add "migration_name" -p .\Users.db\ -s .\Users.API

������� �������� dotnet ef migrations remove -p .\Users.db\ -s .\Users.API

�������� �������� dotnet ef database update -p .\Users.db\ -s .\Users.API

