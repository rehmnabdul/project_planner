using System;
using System.ComponentModel.DataAnnotations;

namespace PlexProjectPlanner.Core.Validation
{
    // Custom validation attribute for project name
    public class ProjectNameAttribute : ValidationAttribute
    {
        public ProjectNameAttribute() : base("Project name is invalid.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Project name is required.");
            }

            var name = value.ToString();
            if (name.Length > 200)
            {
                return new ValidationResult("Project name must not exceed 200 characters.");
            }

            return ValidationResult.Success;
        }
    }

    // Custom validation attribute for task title
    public class TaskTitleAttribute : ValidationAttribute
    {
        public TaskTitleAttribute() : base("Task title is invalid.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Task title is required.");
            }

            var title = value.ToString();
            if (title.Length > 500)
            {
                return new ValidationResult("Task title must not exceed 500 characters.");
            }

            return ValidationResult.Success;
        }
    }
}