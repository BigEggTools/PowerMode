namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ValidatableModel : Model, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<ValidationResult>> errors;
        private IReadOnlyList<ValidationResult> allErrorsCache;
        private bool hasErrors;


        protected ValidatableModel()
        {
            errors = new Dictionary<string, List<ValidationResult>>();
            allErrorsCache = new ValidationResult[0];
        }


        #region Implement Interface INotifyDataErrorInfo
        public bool HasErrors
        {
            get { return hasErrors; }
            private set { SetProperty(ref hasErrors, value); }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        public IEnumerable<ValidationResult> GetErrors()
        {
            return GetErrors(null);
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return GetErrors(propertyName);
        }
        #endregion


        public IEnumerable<ValidationResult> GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                List<ValidationResult> result;
                if (errors.TryGetValue(propertyName, out result))
                {
                    return result;
                }
                return new ValidationResult[0];
            }
            else
            {
                return allErrorsCache;
            }
        }

        public bool Validate()
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);
            UpdateErrors(validationResults);
            return !HasErrors;
        }


        protected bool SetPropertyAndValidate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("The argument propertyName must not be null or empty.", nameof(propertyName));

            if (SetProperty(ref field, value, propertyName))
            {
                Validate();
                return true;
            }
            return false;
        }


        private void UpdateErrors(IReadOnlyList<ValidationResult> validationResults, string propertyName = null)
        {
            var newErrors = new Dictionary<string, List<ValidationResult>>();
            foreach (var validationResult in validationResults)
            {
                var memberNames = validationResult.MemberNames.Any() ? validationResult.MemberNames : new[] { "" };
                foreach (string memberName in memberNames)
                {
                    if (!newErrors.ContainsKey(memberName))
                    {
                        newErrors.Add(memberName, new List<ValidationResult>() { validationResult });
                    }
                    else
                    {
                        newErrors[memberName].Add(validationResult);
                    }
                }
            }

            var changedProperties = new HashSet<string>();
            var errorKeys = propertyName == null ? errors.Keys : errors.Keys.Where(x => x == propertyName);
            var newErrorKeys = propertyName == null ? newErrors.Keys : newErrors.Keys.Where(x => x == propertyName);
            foreach (var propertyToRemove in errorKeys.Except(newErrorKeys).ToArray())
            {
                changedProperties.Add(propertyToRemove);
                errors.Remove(propertyToRemove);
            }
            foreach (var propertyToUpdate in errorKeys.ToArray())
            {
                if (!errors[propertyToUpdate].SequenceEqual(newErrors[propertyToUpdate], ValidationResultComparer.Default))
                {
                    changedProperties.Add(propertyToUpdate);
                    errors[propertyToUpdate] = newErrors[propertyToUpdate];
                }
            }
            foreach (var propertyToAdd in newErrorKeys.Except(errorKeys).ToArray())
            {
                changedProperties.Add(propertyToAdd);
                errors.Add(propertyToAdd, newErrors[propertyToAdd]);
            }

            if (changedProperties.Any())
            {
                allErrorsCache = errors.Values.SelectMany(x => x).Distinct().ToArray();
                HasErrors = errors.Any();
            }

            foreach (var changedProperty in changedProperties) RaiseErrorsChanged(changedProperty);
        }

        private void RaiseErrorsChanged(string propertyName = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        private sealed class ValidationResultComparer : IEqualityComparer<ValidationResult>
        {
            public static ValidationResultComparer Default { get; } = new ValidationResultComparer();

            public bool Equals(ValidationResult x, ValidationResult y)
            {
                if (x == y) return true;
                if (x == null || y == null) return false;
                return Equals(x.ErrorMessage, y.ErrorMessage) && x.MemberNames.SequenceEqual(y.MemberNames);
            }

            public int GetHashCode(ValidationResult obj)
            {
                if (obj == null) return 0;
                return (obj.ErrorMessage?.GetHashCode() ?? 0) ^ obj.MemberNames.Select(x => x?.GetHashCode() ?? 0).Aggregate(0, (current, next) => current ^ next);
            }
        }
    }
}