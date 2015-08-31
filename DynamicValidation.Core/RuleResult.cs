namespace DynamicValidation.Core
{
	public class RuleResult
	{
		public RuleResult(bool result, string property, string message)
		{
			Result = result;
			Property = property;
			Message = message;
		}

		public string Message { get; private set; }

		public bool Result { get; private set; }

		public string Property { get; private set; }
	}
}