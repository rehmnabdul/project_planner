# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: C# .NET 9.0  
**Primary Dependencies**: ASP.NET Core, .NET Aspire, Entity Framework Core, Blazor  
**Storage**: PostgreSQL or SQL Server  
**Testing**: xUnit, NUnit, or MSTest with minimum 85% coverage  
**Target Platform**: Web application (cross-platform)  
**Project Type**: Web application with Blazor UI (DDD architecture)  
**Performance Goals**: <200ms response time for common operations, <3s page load time  
**Constraints**: WCAG 2.1 AA accessibility compliance, minimum 1000 concurrent users support  
**Scale/Scope**: Multi-user project planning application with domain-driven design

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- Code Quality Standards: Static analysis tools (Roslyn/StyleCop) configured and enforced
- Testing Standards: TDD approach with 85%+ test coverage requirement (95%+ for critical paths)
- DDD Implementation: Architecture follows Domain-Driven Design with bounded contexts
- User Experience: Blazor UI components follow consistent design system, WCAG 2.1 AA compliant
- Performance Requirements: Response times <200ms, page loads <3s, supports 1000+ concurrent users
- Technology Stack: Built with ASP.NET 9.0, .NET Aspire, DDD, and Blazor as required

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
