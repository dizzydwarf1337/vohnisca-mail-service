# Agents — vohnisca-mail-service

## Identity
Отправка писем через Gmail SMTP. Получает задания двумя путями: JSON-RPC от gateway и **RabbitMQ**
(MassTransit consumers) от auth- и campaign-service. Собственной БД нет.

## Stack
- C# **.NET 9**, JSON-RPC сервер — **EdjCase.JsonRpc.Router**, события — **MassTransit + RabbitMQ**.
- Слои: `vohnisca-mail-service/` (Program, Controllers, DI, Middleware) · `Application/` (Commands, Consumers, Interfaces) · `Infrastructure/`.

## Read order
1. В мета-репо: `.claude/rules/dotnet-preflight.md`.
2. Этот файл.

## Verification
- Сборка: `dotnet build vohnisca-mail-service/vohnisca-mail-service.sln`
- Тестов нет. «Зелёно» = чистая сборка.

## Эталон
- Команда: `vohnisca-mail-service/Application/Commands/SendMail/` (Command + Handler).
- Контроллер (минимальный, ~18 строк): `vohnisca-mail-service/vohnisca-mail-service/Controllers/MailController.cs`.
- Consumer события: `vohnisca-mail-service/Application/Consumers/Users/UserCreatedConsumer.cs`.
- Новый метод — по образцу SendMail; новый consumer — по образцу UserCreatedConsumer.

## Локальные конвенции
- Сервис намеренно минимальный: один RPC-метод `SendMail(email, subject, content)` + consumers.
- SMTP-настройки — в `appsettings.Development.json` (секция `EmailSettings`).
- Consumers слушают события `user-created` (auth) и `invitation-created` (campaign) через MassTransit.
- Никакой БД и состояния — не добавляй хранилище без явной необходимости.

## Коммит-граница
Репозиторий `dizzydwarf1337/vohnisca-mail-service`, дефолтная ветка `master`. Коммиты — сюда.
Ветка текущей работы: `core/implement-mail-service-workspace`.
