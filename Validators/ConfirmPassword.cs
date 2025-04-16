    using System.ComponentModel.DataAnnotations;

namespace Tabeekh.Validators
{
    public class ConfirmPassword:ValidationAttribute
    {
        public string ConfirmPasswordProperty { get; private set; }

        public ConfirmPassword(string confirmPasswordProperty)
        {
            ConfirmPasswordProperty = confirmPasswordProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty("Password");
            var confirmPasswordProperty = validationContext.ObjectType.GetProperty(ConfirmPasswordProperty);

            if (passwordProperty == null || confirmPasswordProperty == null)
                return new ValidationResult("Invalid property");

            var password = passwordProperty.GetValue(validationContext.ObjectInstance, null)?.ToString();
            var confirmPassword = confirmPasswordProperty.GetValue(validationContext.ObjectInstance, null)?.ToString();

            if (password != confirmPassword)
            {
                return new ValidationResult("Password and Confirm Password do not match");
            }

            return ValidationResult.Success;
        }
    }
}
