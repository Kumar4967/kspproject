# КСП магазин

Интернет-магазин с системой аутентификации, корзиной товаров и оформлением заказов.

## Схема проекта

```
корень
├── AppDbContext.cs   // схема базы данных
├── AppDbContextFactory.cs
├── appsettings.Development.json
├── appsettings.json
├── AuthService.cs
├── Components   // часть блейзора
│   ├── AdsRepeater.razor
│   ├── App.razor
│   ├── _Imports.razor
│   ├── Layout   // Оформление фронтенда
│   │   ├── MainLayout.razor
│   │   ├── MainLayout.razor.css
│   │   ├── ReconnectModal.razor
│   │   ├── ReconnectModal.razor.css
│   │   └── ReconnectModal.razor.js
│   ├── Pages   // фронтенд
│   │   ├── About.razor
│   │   ├── Cart.razor
│   │   ├── Checkout.razor
│   │   ├── Error.razor
│   │   ├── Index.razor
│   │   ├── Login.razor
│   │   ├── Login.razor.css
│   │   ├── NotFound.razor
│   │   ├── Orders.razor
│   │   ├── Profile.razor
│   │   └── Shop.razor
│   ├── ProductCard.razor
│   └── Routes.razor
├── Controllers   // бекэнд
│   ├── AuthController.cs
│   └── OrderController.cs
├── docker-compose.yml   // докер для запуска
├── Dockerfile
├── dotnet-tools.json   // тут dotnet-ef прописан
├── ksoproject.csproj
├── Migrations   // Папка с миграциями
│   ├── ...
├── Models
│   ├── CartItem.cs
│   └── Order.cs
├── Pages   // бекэнд на asp.net core
│   ├── LoginHandler.cshtml
│   ├── LoginHandler.cshtml.cs
│   └── LogoutHandler.cshtml
├── Program.cs
├── Properties
│   └── launchSettings.json
├── README.md
├── Services   // общие интерфейсы для работы с бизнес логикой
│   ├── CartService.cs
│   ├── DataSeeder.cs
│   ├── ICartService.cs
│   ├── IOrderService.cs
│   ├── LocalStorageService.cs
│   └── OrderService.cs
├── Validators.cs
├── ViewModels.cs
└── wwwroot   // статические файлы
    ├── app.css
    ├── favicon.png
    ├── js
    │   ├── app.js
    │   ├── auth.js
    │   └── notifications.js
    └── lib
        └── bootstrap
```

## Запуск

### Через docker compose
```sh
docker compose up --build
```

### Через dotnet run
(Для начала надо запустить postgresql)
```sh
dotnet restore
dotnet bulid
dotnet run --project ksoproject.csproj
```

## Изменение базы данных
После изменения файла `AppDbContext.cs`, надо прописать
```sh
dotnet ef migrations add <название миграции>
dotnet ef database update
```

## Внутренние особенности
В коде есть поддержка аккаунтов администратора, однако на данный момент нет
функционала для их использования.

Внутри проект имеет префикс `kso` (не `ksp`, т.е. Кроссплатформенная Среда
Разработки). `o` и `p` на клавиатуре расположены очень близко ¯\_(ツ)_/¯

Есть некоторе легаси, вроде бесполезных миграций и коннекторов для разных
технологий ASP.NET. В связи с обратной совместимостью их нельзя удалить.
