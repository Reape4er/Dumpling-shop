������� ��� ���������� �������� �� ���������� dotnet tool install --global dotnet-ef

������� �������� dotnet ef migrations add "migration_name" -p .\Product.DB\ -s .\Product.API

������� �������� dotnet ef migrations remove -p .\Product.DB\ -s .\Product.API

�������� ���� ������ ��� �������� dotnet ef database update -p .\Product.DB\ -s .\Product.API

