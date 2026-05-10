# Coding Case - Workday Calendar

This repository contains the solution for the provded Workday Calendar Case.

The project inplements a calandar system that returns the dates while adjusting to:

- Working hours
- Weekends and holidays
- Forward and backwards shifting of workdays

## Core idea

Handle the workday as a unit of time, not as a typical calendar day. Calculations should only happen during the given timeframe, i.e. when the workday is. Holidays, weekends and before/after the workday should be skipped.

### Design decisions

1. _Workday as a time unit_
   Instead of using a typical work (24h) the workday is considered as its own unit.

2. _Structure_
   The project is divided into different parts based on if its domain (core models), policy (validation and rules) or service (logic and calculations). This desicion was made to ensure it follows a clean arcitecture (even if its a small project).

3. _Approach to calculation of workday_
   When shifting workday the logic is divided into two steps. First we align to worktime, making it possible to start calculating from a "non workday time". Secondly shifting is handled as workdays and minutes, see next point.

4. _Handling fractions of a workday_
   Decimal values are handeled seperate from whole values. I.e. 2.5 is calculated as 2 whole workdays and 0.5 workday.
   The decimal value is calculated as minutes of a workday, while the whole value is handeled as a whole shift.

## Tests

The solution includes tests written based on the examples provided as well as some misc tests written during the development phase. This case was solved with a testdriven approach, where tests were throught of first. I.e. "whats should this code solve?"

Tests include:

- Forward and backwards shifting of workday regardless of starting time
- Adding and taking holidays into account
- Check for weekend
- Workday start/end

## Tradeoffs

At first I thought of creating a custom date type, similair to DateTime, that would be specialized for the calculations that Workday could need. The main win from this, from my perspective, would be to set workday boundaries and handle movement of the workweek inside this type.

However this was discarded since it would increase the complexity of this case, but for a more complex case it could be a solution.
