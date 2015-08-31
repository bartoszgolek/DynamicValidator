namespace Validate
{
	public interface IValidator<in T>
	{
		ValidationResult Validate(T entity);
	}
}