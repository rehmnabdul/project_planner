# Data Model: ClickUp Clone Application

## Domain Model

### Core Domain Entities

#### User
- **Properties**:
  - Id (Guid) - Unique identifier
  - Email (string) - User's email address
  - PasswordHash (string) - Hashed password
  - FirstName (string) - User's first name
  - LastName (string) - User's last name
  - AvatarUrl (string?) - Optional user avatar
  - CreatedAt (DateTime) - Account creation timestamp
  - LastLoginAt (DateTime?) - Last login timestamp
  - IsActive (bool) - Account status
- **Invariants**:
  - Email must be valid and unique
  - Password must meet complexity requirements
  - FirstName and LastName are required
- **Behaviors**:
  - Can change profile information
  - Can update security settings

#### Project
- **Properties**:
  - Id (Guid) - Unique identifier
  - Name (string) - Project name
  - Description (string?) - Optional project description
  - CreatedBy (Guid) - User ID of creator
  - CreatedAt (DateTime) - Project creation timestamp
  - UpdatedAt (DateTime) - Last update timestamp
  - Settings (ProjectSettings) - Project-specific settings
  - Status (ProjectStatus) - Current project status (Active, Archived, etc.)
- **Invariants**:
  - Name is required and unique per user/workspace
  - CreatedBy must reference a valid user
- **Behaviors**:
  - Can be assigned to multiple users
  - Can be configured with custom workflows
  - Can be archived/restored

#### Task
- **Properties**:
  - Id (Guid) - Unique identifier
  - Title (string) - Task title
  - Description (string?) - Optional task description
  - ProjectId (Guid) - Reference to parent project
  - AssigneeId (Guid?) - User ID of assignee
  - Priority (TaskPriority) - Priority level (Low, Medium, High, Urgent)
  - Status (TaskStatus) - Current status (To Do, In Progress, Done, etc.)
  - DueDate (DateTime?) - Optional due date
  - CreatedBy (Guid) - User ID of creator
  - CreatedAt (DateTime) - Task creation timestamp
  - UpdatedAt (DateTime) - Last update timestamp
  - Position (int) - Position in list/board view
- **Invariants**:
  - Title is required
  - Must belong to a valid project
  - Assignee must be a valid user
- **Behaviors**:
  - Can be assigned to users
  - Can have status updated
  - Can be prioritized
  - Can be moved within project

#### Comment
- **Properties**:
  - Id (Guid) - Unique identifier
  - Content (string) - Comment content
  - TaskId (Guid) - Reference to parent task
  - AuthorId (Guid) - User ID of author
  - CreatedAt (DateTime) - Comment creation timestamp
  - UpdatedAt (DateTime) - Last update timestamp
  - IsEdited (bool) - Whether comment has been edited
- **Invariants**:
  - Content is required
  - Must be associated with a valid task
  - Author must be a valid user
- **Behaviors**:
  - Can be edited by author
  - Can mention other users

#### Attachment
- **Properties**:
  - Id (Guid) - Unique identifier
  - FileName (string) - Original file name
  - ContentType (string) - MIME type
  - FileSize (long) - Size in bytes
  - StoragePath (string) - Path to stored file
  - TaskId (Guid?) - Optional reference to task
  - ProjectId (Guid?) - Optional reference to project
  - UploadedBy (Guid) - User ID of uploader
  - UploadedAt (DateTime) - Upload timestamp
- **Invariants**:
  - Must be associated with either a task or project
  - File path must be valid
  - UploadedBy must be valid user
- **Behaviors**:
  - Can be deleted by uploader or project owner
  - Can be linked to multiple entities

#### Workspace
- **Properties**:
  - Id (Guid) - Unique identifier
  - Name (string) - Workspace name
  - OwnerId (Guid) - User ID of workspace owner
  - CreatedAt (DateTime) - Workspace creation timestamp
  - UpdatedAt (DateTime) - Last update timestamp
  - Settings (WorkspaceSettings) - Workspace-specific settings
- **Invariants**:
  - Name is required and unique
  - Owner must be valid user
- **Behaviors**:
  - Can contain multiple projects
  - Can have members invited
  - Can be shared with other users

### Value Objects

#### ProjectSettings
- **Properties**:
  - CustomFields (Dictionary<string, object>) - Custom project fields
  - WorkflowStatuses (List<string>) - Custom workflow statuses
  - Permissions (ProjectPermissions) - Access control settings
  - ViewPreferences (ViewPreferences) - Default view settings

#### TaskPriority (enum)
- Low
- Medium
- High
- Urgent

#### TaskStatus (enum)
- To Do
- In Progress
- Code Review
- Testing
- Done

#### ProjectStatus (enum)
- Active
- Archived
- Completed

### Domain Services

#### ProjectService
- Responsible for project creation and validation
- Handles project-to-user assignments
- Enforces project constraints

#### TaskService
- Responsible for task creation and validation
- Handles task state transitions
- Coordinates task dependencies

#### CollaborationService
- Manages real-time collaboration features
- Handles comment notifications
- Processes mentions

### Aggregates

#### Project Aggregate Root
- Root: Project
- Children: Tasks, Comments (on tasks), Attachments
- Consistency boundary: Project-wide operations

#### User Aggregate Root
- Root: User
- Children: User profile data, settings
- Consistency boundary: User-specific operations

#### Task Aggregate Root
- Root: Task
- Children: Comments, Attachments
- Consistency boundary: Task-level operations