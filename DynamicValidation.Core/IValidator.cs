namespace DynamicValidation.Core
{
	public interface IValidator<in T>
	{
		ValidationResult Validate(T entity);

		string Name { get; }
	}
}