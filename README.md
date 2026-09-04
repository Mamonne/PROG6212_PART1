# RaceDay

## System Description

RaceDay is a full-stack, API-driven event management system built for the South African road running, walking, and cycling community. South Africa has a rich road events culture — from the Comrades Marathon and Cape Town Cycle Tour to the Soweto Marathon and hundreds of local park runs — but many of these events are still managed through paper-based registration, spreadsheets, and disconnected communication channels.

RaceDay solves this by giving Event Organisers a single platform to create and manage events, categories, and participant results, while giving Participants a way to browse upcoming events, enter races, track their personal performance history, and prepare for race day using live weather and route information.

This repository currently contains the **Part 1 planning and design deliverables**: the database ERD, the API endpoint plan, and the SQL schema/seed script. No application code has been written yet — Part 1 is a planning phase only.

## User Roles

### Organiser
An Organiser is responsible for the event side of the platform. Organisers can:
- Create, update, and delete their own events
- Add and manage race categories within an event (e.g. 5km, 10km, 21km)
- Capture and publish participant results after an event has taken place
- Manage route and weather information linked to their events

Organisers can only manage events, categories, and results for events they themselves created — enforced through role-based, owner-only permissions on the relevant API endpoints.

### Participant
A Participant is the end user attending events. Participants can:
- Browse all upcoming events without needing to log in
- Register an account and log in to enter a specific event and category
- Track their own enrolment history and payment status
- View their personal performance history (finish times, positions) across past events
- Access live weather and route information to prepare for race day

## Database Design

The database schema is modeled around 7 entities: `Users`, `Events`, `Categories`, `Routes`, `WeatherInfo`, `Enrolments`, and `Results`. Full details, including primary keys, foreign keys, and cardinality, are documented in the ERD below.
