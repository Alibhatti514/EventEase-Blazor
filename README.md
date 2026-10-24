# EventEase — Blazor Event Management

An educational .NET 10 Blazor Web App (Interactive Server) that covers all three course activities.

## Features
- Event cards with name, date, location, description, and dynamic data binding.
- Search and paginated rendering (4 events/page) with `@key` for stable component identity.
- Event details, editable events, registration, attendance, session tracker and friendly invalid-route handling.
- DataAnnotations form validation; duplicate registration prevention; thread-safe in-memory stores.
- Responsive design, no third-party NuGet dependencies.

## Requirements
Install **.NET 10 SDK** on macOS. Confirm with `dotnet --version`.

## Start
```bash
dotnet restore
dotnet build --configuration Release
dotnet run --urls http://localhost:5080
```
Open http://localhost:5080 (the terminal running `dotnet run` stays open).

## Automated smoke test
In a **second Terminal window** (while app is running):
```bash
bash scripts/smoke-test.sh
```
Or run everything automatically in one command (from the project folder):
```bash
bash scripts/test-all.sh
```

## Manual browser checklist
1. Events page loads, search filters, next/previous pagination works.
2. Event details opens and links to registration.
3. Edit event: blank name/invalid values show validation; valid save appears on details.
4. Registration: invalid email and blank name show errors; valid submission confirms; duplicate email for same event is rejected.
5. Attendance shows registration totals and attendee information.
6. Session page updates display name; viewing an event increases visited count.
7. `/does-not-exist` and `/events/999` fail gracefully.
8. Test on a narrow/mobile-sized browser window.

## Architecture and limitations
- Event and registration data are **in memory**; restart clears changes and registrations.
- `SessionState` is scoped to the Blazor Server circuit; it is not durable login/session storage and can reset on reconnect or refresh.
- No authentication or persistent database; this is a **course demonstration**, not a production system.
- Attendee names and email addresses appear in the shared attendance table; **do not enter real personal information** in this demo.
- Blazor Server requires a SignalR connection for interactions.

## Course rubric mapping
- Activity 1: `Components/Shared/EventCard.razor`, `Components/Pages/Home.razor`, `Details.razor`, `EditEvent.razor`, routing in `Routes.razor`.
- Activity 2: DataAnnotations validation, fallback routes, paginated lists, `@key`, tests.
- Activity 3: `Register.razor`, `Session.razor`, `Attendance.razor`, service classes.

## Development tools
- Visual Studio Code — code editor.
- .NET 10 SDK — application development and testing.
- Git and GitHub — version control and repository hosting.
- ChatGPT — initial code generation.

## GitHub repository

Project repository: https://github.com/Alibhatti514/EventEase-Blazor

After making local changes, update the existing repository:

```bash
git add README.md
git commit -m "Update README for .NET 10 and GitHub submission"
git push origin main
```

Never commit secrets or real attendee information.
