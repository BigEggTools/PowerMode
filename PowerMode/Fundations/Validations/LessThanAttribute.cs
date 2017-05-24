namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class LessThanAttribute : ValidationAttribute
    {
        private readonly string dependentPropertyName;
        private readonly bool allowEqual;

        public LessThanAttribute(string dependentPropertyName, bool allowEqual = false)
        {
            this.dependentPropertyName = dependentPropertyName;
            this.allowEqual = allowEqual;
        }

        public override bool RequiresValidationContext { get { return true; } }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult($"Property '{validationContext.MemberName}' have null value.", new List<string>() { validationContext.MemberName, dependentPropertyName }); }
            if (!(value is IComparable)) { return new ValidationResult($"Property '{validationContext.MemberName}' should be comparable."); }

            var dependentPropertyInfo = validationContext.ObjectType.GetProperty(this.dependentPropertyName);
            if (dependentPropertyInfo == null) { return new ValidationResult($"Cannot find the dependent property '{dependentPropertyName}'.", new List<string>() { validationContext.MemberName, dependentPropertyName }); }

            var dependentPropertyValue = dependentPropertyInfo.GetValue(validationContext.ObjectInstance);
            if (dependentPropertyValue == null) { return new ValidationResult($"Property '{dependentPropertyName}' have null value.", new List<string>() { validationContext.MemberName, dependentPropertyName }); }
            if (!(dependentPropertyValue is IComparable)) { return new ValidationResult($"Property '{dependentPropertyName}' should be comparable.", new List<string>() { validationContext.MemberName, dependentPropertyName }); }

            if (value.GetType() != dependentPropertyValue.GetType()) { return new ValidationResult($"Property '{validationContext.MemberName}' and property '{dependentPropertyName}' should have same type.", new List<string>() { validationContext.MemberName, dependentPropertyName }); }

            var compareResult = (value as IComparable).CompareTo(dependentPropertyValue as IComparable);
            if (compareResult > 0 || (compareResult == 0 && !allowEqual))
            {
                return new ValidationResult(ErrorMessage ?? $"Property '{validationContext.MemberName}' Should less than property '{dependentPropertyName}'.", new List<string>() { validationContext.MemberName, dependentPropertyName });
            }
            return ValidationResult.Success;
        }
    }
}
