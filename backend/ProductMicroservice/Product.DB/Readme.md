Команда для добавления миграции на устройство dotnet tool install --global dotnet-ef

создать миграцию dotnet ef migrations add "migration_name" -p .\Product.DB\ -s .\Product.API

удалить миграцию dotnet ef migrations remove -p .\Product.DB\ -s .\Product.API

обновить базу данных под миграцию dotnet ef database update -p .\Product.DB\ -s .\Product.API

