using System.Linq;

namespace mdryden.JsonApi
{
	public static class IErrorsExtensions
	{
		public static bool HasErrors(this IErrors source)
		{
			return source.Errors?.Count > 0;
		}

		public static void AddError(this IErrors target, Error error)
		{
			if (target.Errors == null)
			{
				target.Errors = new ErrorCollection();
			}

			target.Errors.Add(error);
		}

		public static string JoinDetails(this IErrors source)
		{
			var errors = source.Errors?.Select(e => e.Detail);
			var errorsString = errors == null ? "" : string.Join(',', errors);

			return errorsString;
		}
	}
}
