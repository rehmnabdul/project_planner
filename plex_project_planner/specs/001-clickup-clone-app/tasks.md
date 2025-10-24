---
description: "Task list template for feature implementation"
---

# Tasks: ClickUp Clone Application

**Input**: Design documents from `/specs/001-clickup-clone-app/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: The examples below include test tasks. Tests are OPTIONAL - only include them if explicitly requested in the feature specification.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- **Single project**: `src/`, `tests/` at repository root
- **DDD Web app**: `src/Core/`, `src/Infrastructure/`, `src/Application/`, `src/Web/`
- **Mobile**: `api/src/`, `ios/src/` or `android/src/`
- Paths shown below assume DDD structure - adjust based on plan.md structure

<!-- 
  ============================================================================
  IMPORTANT: The tasks below are SAMPLE TASKS for illustration purposes only.
  
  The /speckit.tasks command MUST replace these with actual tasks based on:
  - User stories from spec.md (with their priorities P1, P2, P3...)
  - Feature requirements from plan.md
  - Entities from data-model.md
  - Endpoints from contracts/
  
  Tasks MUST be organized by user story so each story can be:
  - Implemented independently
  - Tested independently
  - Delivered as an MVP increment
  
  DO NOT keep these sample tasks in the generated tasks.md file.
  ============================================================================
-->

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create project structure per implementation plan with src/Core, src/Application, src/Infrastructure, and src/Web directories
- [ ] T002 Initialize .NET 9.0 project with ASP.NET Core, .NET Aspire, Entity Framework Core and Blazor dependencies
- [ ] T003 [P] Configure linting and formatting tools (Roslyn analyzers, StyleCop)
- [ ] T004 [P] Initialize Git repository with proper .gitignore for .NET projects
- [ ] T005 [P] Configure project solution file (.sln) linking all project components

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

Examples of foundational tasks (adjust based on your project):

- [ ] T006 Setup database schema and Entity Framework Core migrations
- [ ] T007 [P] Implement authentication/authorization framework with ASP.NET Core Identity
- [ ] T008 [P] Setup API routing and middleware structure
- [ ] T009 Create domain entities and value objects following DDD patterns
- [ ] T010 Configure error handling, logging, and observability with .NET Aspire
- [ ] T011 Setup environment configuration management
- [ ] T012 Configure static analysis tools (Roslyn analyzers, StyleCop)
- [ ] T013 Setup unit testing framework (xUnit) with coverage reporting
- [ ] T014 Define bounded contexts and domain services for the application
- [ ] T015 Configure SignalR for real-time features
- [ ] T016 Create database context and configure connection to SQL Server
- [ ] T017 [P] Implement JWT authentication configuration

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Create and Manage Projects (Priority: P1) 🎯 MVP

**Goal**: Users can create, organize, and manage projects in a centralized workspace so that they can keep track of different initiatives and their progress.

**Independent Test**: Can be fully tested by creating a project workspace and organizing tasks within it, delivering the core value of project management.

### Tests for User Story 1 (OPTIONAL - only if tests requested) ⚠️

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T018 [P] [US1] Contract test for /api/projects endpoint in tests/Integration/Projects/[Project]ContractTests.cs
- [ ] T019 [P] [US1] Integration test for project creation user journey in tests/Integration/Projects/[Project]IntegrationTests.cs

### Implementation for User Story 1

- [ ] T020 [P] [US1] Create Project entity in src/Core/Entities/Project.cs following DDD patterns
- [ ] T021 [P] [US1] Create ProjectSettings value object in src/Core/ValueObjects/ProjectSettings.cs following DDD patterns
- [ ] T022 [US1] Implement ProjectService in src/Core/DomainServices/ProjectService.cs (depends on T020, T021)
- [ ] T023 [US1] Create ProjectRepository interface in src/Core/Interfaces/IProjectRepository.cs
- [ ] T024 [US1] Implement ProjectRepository in src/Infrastructure/Repositories/ProjectRepository.cs
- [ ] T025 [US1] Create ProjectCreateCommand in src/Application/Commands/ProjectCreateCommand.cs
- [ ] T026 [US1] Create ProjectUpdateCommand in src/Application/Commands/ProjectUpdateCommand.cs
- [ ] T027 [US1] Create ProjectDTO in src/Application/DTOs/ProjectDTO.cs
- [ ] T028 [US1] Implement ProjectsController in src/Web/Controllers/ProjectsController.cs
- [ ] T029 [US1] Create Project index page component in src/Web/Pages/Projects.razor
- [ ] T030 [US1] Add validation and error handling for projects
- [ ] T031 [US1] Add logging for project operations
- [ ] T032 [US1] Create Blazor component for project card display

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Task Management and Tracking (Priority: P1)

**Goal**: Users can create, assign, and track tasks within projects so that they can manage their work effectively and ensure nothing falls through the cracks.

**Independent Test**: Can be tested by creating tasks, assigning them, updating their status, and tracking their progress, delivering the core task management value.

### Tests for User Story 2 (OPTIONAL - only if tests requested) ⚠️

- [ ] T033 [P] [US2] Contract test for /api/projects/{projectId}/tasks endpoint in tests/Integration/Tasks/[Task]ContractTests.cs
- [ ] T034 [P] [US2] Integration test for task management user journey in tests/Integration/Tasks/[Task]IntegrationTests.cs

### Implementation for User Story 2

- [ ] T035 [P] [US2] Create Task entity in src/Core/Entities/Task.cs following DDD patterns
- [ ] T036 [P] [US2] Create TaskPriority and TaskStatus value objects in src/Core/ValueObjects/TaskPriority.cs and TaskStatus.cs following DDD patterns
- [ ] T037 [US2] Implement TaskService in src/Core/DomainServices/TaskService.cs (depends on T035, T036)
- [ ] T038 [US2] Create TaskRepository interface in src/Core/Interfaces/ITaskRepository.cs
- [ ] T039 [US2] Implement TaskRepository in src/Infrastructure/Repositories/TaskRepository.cs
- [ ] T040 [US2] Create TaskCreateCommand in src/Application/Commands/TaskCreateCommand.cs
- [ ] T041 [US2] Create TaskUpdateCommand in src/Application/Commands/TaskUpdateCommand.cs
- [ ] T042 [US2] Create TaskDTO in src/Application/DTOs/TaskDTO.cs
- [ ] T043 [US2] Implement TasksController in src/Web/Controllers/TasksController.cs
- [ ] T044 [US2] Create Task list page component in src/Web/Pages/Tasks.razor
- [ ] T045 [US2] Add validation and error handling for tasks
- [ ] T046 [US2] Add logging for task operations
- [ ] T047 [US2] Create Blazor component for task management (Kanban board view)

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Team Collaboration and Communication (Priority: P2)

**Goal**: Users can collaborate with team members on tasks and projects through comments and mentions so that we can work together effectively and stay aligned.

**Independent Test**: Can be tested by creating comments and mentions on tasks, delivering the collaboration value without requiring all other features to be complete.

### Tests for User Story 3 (OPTIONAL - only if tests requested) ⚠️

- [ ] T048 [P] [US3] Contract test for /api/tasks/{taskId}/comments endpoint in tests/Integration/Collaboration/[Comment]ContractTests.cs
- [ ] T049 [P] [US3] Integration test for collaboration user journey in tests/Integration/Collaboration/[Comment]IntegrationTests.cs

### Implementation for User Story 3

- [ ] T050 [P] [US3] Create Comment entity in src/Core/Entities/Comment.cs following DDD patterns
- [ ] T051 [P] [US3] Create Attachment entity in src/Core/Entities/Attachment.cs following DDD patterns
- [ ] T052 [US3] Implement CollaborationService in src/Core/DomainServices/CollaborationService.cs (depends on T050, T051)
- [ ] T053 [US3] Create CommentRepository interface in src/Core/Interfaces/ICommentRepository.cs
- [ ] T054 [US3] Implement CommentRepository in src/Infrastructure/Repositories/CommentRepository.cs
- [ ] T055 [US3] Create CommentCreateCommand in src/Application/Commands/CommentCreateCommand.cs
- [ ] T056 [US3] Create AttachmentDTO in src/Application/DTOs/AttachmentDTO.cs
- [ ] T057 [US3] Implement CommentsController in src/Web/Controllers/CommentsController.cs
- [ ] T058 [US3] Create file upload functionality for attachments
- [ ] T059 [US3] Add validation and error handling for comments/attachments
- [ ] T060 [US3] Add logging for collaboration operations
- [ ] T061 [US3] Create Blazor component for comments on tasks

**Checkpoint**: At this point, User Stories 1, 2 AND 3 should all work independently

---

## Phase 6: User Story 4 - Dashboard and Reporting (Priority: P2)

**Goal**: Users can view project progress, task status, and productivity metrics through dashboards so that they can make informed decisions about their projects.

**Independent Test**: Can be tested by viewing dashboards with existing project data, delivering the value of progress visibility without requiring all other features.

### Tests for User Story 4 (OPTIONAL - only if tests requested) ⚠️

- [ ] T062 [P] [US4] Contract test for dashboard API endpoints in tests/Integration/Dashboard/[Dashboard]ContractTests.cs
- [ ] T063 [P] [US4] Integration test for dashboard user journey in tests/Integration/Dashboard/[Dashboard]IntegrationTests.cs

### Implementation for User Story 4

- [ ] T064 [P] [US4] Create DashboardQuery in src/Application/Queries/DashboardQuery.cs
- [ ] T065 [P] [US4] Create DashboardDTO in src/Application/DTOs/DashboardDTO.cs
- [ ] T066 [US4] Create ReportingService in src/Application/Services/ReportingService.cs
- [ ] T067 [US4] Implement DashboardController in src/Web/Controllers/DashboardController.cs
- [ ] T068 [US4] Create Dashboard page component in src/Web/Pages/Dashboard.razor
- [ ] T069 [US4] Add chart visualization components for metrics
- [ ] T070 [US4] Add filtering and search functionality for dashboard data
- [ ] T071 [US4] Add logging for dashboard operations
- [ ] T072 [US4] Create Blazor chart components for project metrics

**Checkpoint**: At this point, User Stories 1, 2, 3 AND 4 should all work independently

---

## Phase 7: User Story 5 - Custom Workflows and Statuses (Priority: P3)

**Goal**: Users can customize workflows and task statuses to match their team's working methods so that the system adapts to their processes rather than forcing them to change.

**Independent Test**: Can be tested by creating custom workflows and applying them to tasks, delivering the value of process customization without requiring all other features.

### Tests for User Story 5 (OPTIONAL - only if tests requested) ⚠️

- [ ] T073 [P] [US5] Contract test for workflow configuration endpoints in tests/Integration/Workflows/[Workflow]ContractTests.cs
- [ ] T074 [P] [US5] Integration test for custom workflow user journey in tests/Integration/Workflows/[Workflow]IntegrationTests.cs

### Implementation for User Story 5

- [ ] T075 [P] [US5] Enhance ProjectSettings with custom workflow configuration in src/Core/ValueObjects/ProjectSettings.cs
- [ ] T076 [P] [US5] Create Workflow entity in src/Core/Entities/Workflow.cs following DDD patterns
- [ ] T077 [US5] Implement WorkflowService in src/Core/DomainServices/WorkflowService.cs (depends on T075, T076)
- [ ] T078 [US5] Create WorkflowRepository interface in src/Core/Interfaces/IWorkflowRepository.cs
- [ ] T079 [US5] Implement WorkflowRepository in src/Infrastructure/Repositories/WorkflowRepository.cs
- [ ] T080 [US5] Create WorkflowCreateCommand in src/Application/Commands/WorkflowCreateCommand.cs
- [ ] T081 [US5] Create WorkflowDTO in src/Application/DTOs/WorkflowDTO.cs
- [ ] T082 [US5] Implement WorkflowsController in src/Web/Controllers/WorkflowsController.cs
- [ ] T083 [US5] Create workflow configuration page component in src/Web/Pages/Workflows.razor
- [ ] T084 [US5] Add validation for custom workflow rules
- [ ] T085 [US5] Add logging for workflow configuration operations
- [ ] T086 [US5] Create Blazor component for workflow editor

**Checkpoint**: All user stories should now be independently functional

---

[Add more user story phases as needed, following the same pattern]

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T087 [P] Documentation updates in docs/
- [ ] T088 Code cleanup and refactoring
- [ ] T089 Performance optimization across all stories
- [ ] T090 [P] Additional unit tests (if requested) in tests/Unit/
- [ ] T091 Security hardening
- [ ] T092 Run quickstart.md validation
- [ ] T093 [P] Accessibility improvements (WCAG 2.1 AA compliance)
- [ ] T094 [P] Add caching for performance optimization
- [ ] T095 [P] Add search functionality across projects and tasks
- [ ] T096 [P] Add signalR for real-time collaboration updates

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 2 (P1)**: Can start after Foundational (Phase 2) - May integrate with US1 but should be independently testable
- **User Story 3 (P2)**: Can start after Foundational (Phase 2) - May integrate with US1/US2 but should be independently testable
- **User Story 4 (P2)**: Can start after Foundational (Phase 2) - May integrate with US1/US2/US3 but should be independently testable
- **User Story 5 (P3)**: Can start after Foundational (Phase 2) - May integrate with US1/US2/US3/US4 but should be independently testable

### Within Each User Story

- Tests (if included) MUST be written and FAIL before implementation
- Models before services
- Services before endpoints
- Core implementation before integration
- Story complete before moving to next priority

### Parallel Opportunities

- All Setup tasks marked [P] can run in parallel
- All Foundational tasks marked [P] can run in parallel (within Phase 2)
- Once Foundational phase completes, all user stories can start in parallel (if team capacity allows)
- All tests for a user story marked [P] can run in parallel
- Models within a story marked [P] can run in parallel
- Different user stories can be worked on in parallel by different team members

---

## Parallel Example: User Story 1

```bash
# Launch all tests for User Story 1 together (if tests requested):
Task: "Contract test for /api/projects endpoint in tests/Integration/Projects/[Project]ContractTests.cs"
Task: "Integration test for project creation user journey in tests/Integration/Projects/[Project]IntegrationTests.cs"

# Launch all domain entities for User Story 1 together:
Task: "Create Project entity in src/Core/Entities/Project.cs following DDD patterns"
Task: "Create ProjectSettings value object in src/Core/ValueObjects/ProjectSettings.cs following DDD patterns"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL - blocks all stories)
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE**: Test User Story 1 independently
5. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 → Test independently → Deploy/Demo
4. Add User Story 3 → Test independently → Deploy/Demo
5. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

1. Team completes Setup + Foundational together
2. Once Foundational is done:
   - Developer A: User Story 1
   - Developer B: User Story 2
   - Developer C: User Story 3
3. Stories complete and integrate independently

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and testable
- Verify tests fail before implementing
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence