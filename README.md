# JsonApi.Net
A really simple set of helper classes for creating a JSON API

I built these to make it easy to return responses that are Json API compliant.  The full spec isn't even close to implemented, all you get here is a response that either returns a data object, or a collection of errors, plus an optional collection of meta objects.

Since I also tend to use API keys in my services, there is also an optional API filter which can configured.

## Setup:

For the bare minimum, call services.AddJsonApi() in your Startup.ConfigureService method.  This adds the formatter and exception filter to your controllers, giving you compliant responses at all times.

## ApiExceptionFilterAttribute

By default, any unhandled exception will create a response like this:

{
  errors: [
  {
    code: 500,
    detail: "Exception of type 'System.Exception' was thrown."
  }]
}

For more control over the code that is returned, you can throw a JsonApiException, which accepts an HttpStatusCode value in the constructor.
When a JsonApiException is caught by the filter, the code in the exception will be returned with the response, for example:

{
  errors: [
  {
    code: 403,
    detail: "API key is missing or invalid."
  }]
}

## ApiKeyFilterAttribute

To protect all controllers with API keys, include a configuration section of type JsonApiKeyCollection in your AddJsonApi() call.  If using appsettings.json in a .Net Core project, that could look like this:

appsettings.json:

{
  "JsonApiKeys": [
    {
      "Key": "10000000-0000-0000-0000-000000000000",
      "Revoked": false,
      "Details": "Test key"
    }
  ]
}

Startup.cs:

services.AddJsonApi(Configuration.GetSection("JsonApiKeys"));

I will probably add the option to apply the filter selectively, but I haven't yet.

## Metadata

You can provide a function with returns meta data for all responses in the startup as well, like so:

services.AddJsonApiMeta(() =>
    {
        return new Dictionary<string, object>
        {
            { "version", "1.0.0" },
            { "copyright", $"Copyright {DateTime.Now.Year} me." }
        };
    });
    
 I should probably make this a bit more robust as well, it would probably be nice if the data object and/or error (maybe the whole response?) were passed in to the function, so it could be more than just a mostly-static function.  Eventually, probably.
 
 
 ## Other notes
 
 I built this for me, and I put it on NuGet so I could use it all over (https://www.nuget.org/packages/mdryden.JsonApi.Net/). Maybe it'll be useful for others too, maybe it won't.  If you're using it, and you want to contribute, I'm more than happy to accept pull requests as long as they are fairly simple.  If you notice a bug, I'll probably fix that too, since I'm actively using this library.  Beyond that, I make no warranties as to the quality of the code.  This is a simple project for a simple use case, because all of the other libraries out there seemed to be trying to do way more than I needed.
 
