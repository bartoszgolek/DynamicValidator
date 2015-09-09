using System;

namespace DynamicValidation.Core
{
	public class RuleResult
	{
		private readonly Type type;

		public static RuleResult Valid(Type type, string name, string memberName)
		{
			return new RuleResult(true, type, name, memberName, string.Empty, null);
		}

		public static RuleResult Invalid(Type type, string name, string memberName, string message)
		{
			return new RuleResult(true, type, name, memberName, message, null);
		}

		public static RuleResult Invalid(Type type, string name, string memberName, string message, ValidationResult innerResult)
		{
			return new RuleResult(true, type, name, memberName, message, innerResult);
		}

		public RuleResult(bool result, Type type, string name, string property, string message, ValidationResult innerResult)
		{
			Result = result;
			Type = type;
			Name = name;
			Property = property;
			InnerResult = innerResult;
			Message = message;
		}

		public ValidationResult InnerResult { get; private set; }

		public string Message { get; private set; }

		public bool Result { get; private set; }
		public Type Type { get; private set; }
		public string Name { get; private set; }

		public string FullName
		{
			get
			{
				if (string.IsNullOrEmpty(Name))
					return string.Empty;

				if (string.IsNullOrEmpty(Property))
					return Type + "." + Name;

				return Type + "." + Property + "." + Name;
			}
		}

		public string Property { get; private set; }
	}
}