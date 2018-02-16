using System.Net;

namespace mdryden.JsonApi
{
	public static class ErrorExtensions
	{	

		public static Error WithCode(this Error error, string code)
		{
			error.Code = code;
			return error;
		}

		public static Error WithDetail(this Error error, string detail)
		{
			error.Detail = detail;
			return error;
		}

		public static Error WithStatus(this Error error, HttpStatusCode status)
		{
			error.Status = status;
			return error;
		}
	}

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
	}
}
