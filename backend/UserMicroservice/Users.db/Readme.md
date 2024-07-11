Команда для добавления миграции на устройство dotnet tool install --global dotnet-ef

создать миграцию dotnet ef migrations add "migration_name" -p .\Users.db\ -s .\Users.API

удалить миграцию dotnet ef migrations remove -p .\Users.db\ -s .\Users.API

обновить миграцию dotnet ef database update -p .\Users.db\ -s .\Users.API

