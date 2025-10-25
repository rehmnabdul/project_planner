using System;
using System.Collections.Generic;

namespace PlexProjectPlanner.Core.ValueObjects
{
    public class ProjectSettings
    {
        public Dictionary<string, object> CustomFields { get; private set; }
        public List<string> WorkflowStatuses { get; private set; }
        public ProjectPermissions Permissions { get; private set; }
        public ViewPreferences ViewPreferences { get; private set; }

        public ProjectSettings()
        {
            CustomFields = new Dictionary<string, object>();
            WorkflowStatuses = new List<string> { "To Do", "In Progress", "Done" }; // Default workflow statuses
            Permissions = new ProjectPermissions();
            ViewPreferences = new ViewPreferences();
        }

        public void SetCustomField(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Custom field key cannot be empty", nameof(key));

            CustomFields[key] = value;
        }

        public void RemoveCustomField(string key)
        {
            if (CustomFields.ContainsKey(key))
                CustomFields.Remove(key);
        }

        public void AddWorkflowStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Workflow status cannot be empty", nameof(status));

            if (!WorkflowStatuses.Contains(status))
                WorkflowStatuses.Add(status);
        }

        public void SetCustomWorkflowConfiguration(Dictionary<string, object> workflowConfig)
        {
            if (workflowConfig == null)
                throw new ArgumentNullException(nameof(workflowConfig));

            // This could include custom statuses, transitions, rules, etc.
            if (workflowConfig.ContainsKey("CustomStatuses") && workflowConfig["CustomStatuses"] is List<string> customStatuses)
            {
                // Clear default statuses first
                WorkflowStatuses.Clear();
                // Add custom statuses
                foreach (var status in customStatuses)
                {
                    if (!WorkflowStatuses.Contains(status))
                        WorkflowStatuses.Add(status);
                }
            }
            
            // Store any additional workflow configuration
            foreach (var kvp in workflowConfig)
            {
                if (kvp.Key != "CustomStatuses") // Don't store custom statuses in the general CustomFields
                {
                    if (CustomFields.ContainsKey(kvp.Key))
                        CustomFields[kvp.Key] = kvp.Value;
                    else
                        CustomFields.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public void RemoveWorkflowStatus(string status)
        {
            // Don't allow removing default statuses that are essential
            if (status != "To Do" && status != "In Progress" && status != "Done")
            {
                WorkflowStatuses.Remove(status);
            }
        }

        public void SetPermission(string role, string permission)
        {
            Permissions.SetPermission(role, permission);
        }

        public void SetViewPreference(string viewName, string preference)
        {
            ViewPreferences.SetPreference(viewName, preference);
        }
    }

    public class ProjectPermissions
    {
        public Dictionary<string, List<string>> RolePermissions { get; private set; }

        public ProjectPermissions()
        {
            RolePermissions = new Dictionary<string, List<string>>();
        }

        public void SetPermission(string role, string permission)
        {
            if (!RolePermissions.ContainsKey(role))
            {
                RolePermissions[role] = new List<string>();
            }

            if (!RolePermissions[role].Contains(permission))
            {
                RolePermissions[role].Add(permission);
            }
        }

        public bool HasPermission(string role, string permission)
        {
            if (!RolePermissions.ContainsKey(role))
                return false;

            return RolePermissions[role].Contains(permission);
        }
    }

    public class ViewPreferences
    {
        public Dictionary<string, string> Preferences { get; private set; }

        public ViewPreferences()
        {
            Preferences = new Dictionary<string, string>();
        }

        public void SetPreference(string viewName, string preference)
        {
            Preferences[viewName] = preference;
        }

        public string GetPreference(string viewName, string defaultValue = "")
        {
            if (Preferences.ContainsKey(viewName))
                return Preferences[viewName];

            return defaultValue;
        }
    }
}