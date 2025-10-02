# BasicDailyScheduler

**BasicDailyScheduler** is a lightweight .NET Web API project for running automated daily tasks.

It uses `BackgroundService` to schedule and execute multiple jobs (like sending emails, generating reports, or backups) at specific times every day, without requiring manual triggers.

## Features
- Run daily tasks automatically at configured times.
- Supports multiple jobs with separate schedules.
- Simple, lightweight, and easy to extend.

## Getting Started

1. Clone the repository.
2. Register your jobs in `MultiDailyTaskService`.
3. Run the project — tasks will execute automatically at their scheduled times.

## Technologies Used
- .NET 8
- BackgroundService
- Dependency Injection & Logging
