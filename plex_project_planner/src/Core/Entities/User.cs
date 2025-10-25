using System;
using System.Text.RegularExpressions;

namespace PlexProjectPlanner.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? AvatarUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public bool IsActive { get; private set; }

        // Private constructor for EF Core
        private User() { }

        public User(string email, string passwordHash, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetPasswordHash(passwordHash);
            SetName(firstName, lastName);
            CreatedAt = DateTime.UtcNow;
            LastLoginAt = null;
            IsActive = true;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format", nameof(email));

            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash is required", nameof(passwordHash));

            PasswordHash = passwordHash;
        }

        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required", nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateProfile(string firstName, string lastName, string? avatarUrl = null)
        {
            SetName(firstName, lastName);
            AvatarUrl = avatarUrl;
        }

        public void UpdateSecuritySettings(string passwordHash)
        {
            SetPasswordHash(passwordHash);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public void SetLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}