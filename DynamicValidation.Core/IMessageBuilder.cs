namespace DynamicValidation.Core
{
	public interface IMessageBuilder<TEntity> : IValidatorBuilder<TEntity>
	{
		IValidatorBuilder<TEntity> WithMessage(string message);
	}
}