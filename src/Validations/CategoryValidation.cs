// using System.ComponentModel.DataAnnotations;

// public class CategoryValidation: ValidationAttribute
// {
//     public CategoryValidation(params string[] catNames)
//     {
//         this.catNames = catNames;
//     }

//     public string[] catNames { get; private set; }

//     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//     {
//         var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
//         var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
//         var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
//         if (totalLength < this.MinLength)
//         {
//             return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
//         }
//         return null;
//     }
// }