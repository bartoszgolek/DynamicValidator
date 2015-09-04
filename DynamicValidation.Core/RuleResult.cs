namespace DynamicValidation.Core
{
	public class RuleResult
	{
		public static RuleResult Valid(string memberName)
		{
			return new RuleResult(true, memberName, string.Empty, null);
		}

		public static RuleResult Invalid(string memberName, string message)
		{
			return new RuleResult(true, memberName, message, null);
		}

		public static RuleResult Invalid(string memberName, string message, ValidationResult innerResult)
		{
			return new RuleResult(true, memberName, message, innerResult);
		}

		public RuleResult(bool result, string property, string message, ValidationResult innerResult)
		{
			Result = result;
			Property = property;
			InnerResult = innerResult;
			Message = message;
		}

		public ValidationResult InnerResult { get; private set; }

		public string Message { get; private set; }

		public bool Result { get; private set; }

		public string Property { get; private set; }
	}
}