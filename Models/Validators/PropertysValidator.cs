using FluentValidation;

namespace Homie_backend_test.Models
{
  public class PropertysValidator: AbstractValidator<Propertys>{
    public PropertysValidator(){
      RuleFor(property=>property.Name).NotNull().MinimumLength(1).MaximumLength(100);
      RuleFor(property=>property.Description).NotNull().MinimumLength(1).MaximumLength(500);
      RuleFor(property=> property.StatusId).InclusiveBetween(1,3);
      RuleFor(property=> property.TenantId).NotNull().NotEmpty();
    }
  }
}