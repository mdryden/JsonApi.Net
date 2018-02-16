using Newtonsoft.Json;
using System.Net;

namespace mdryden.JsonApi
{
	public interface IApiResponse : IErrors, ILinks, IMeta
	{
	}
}