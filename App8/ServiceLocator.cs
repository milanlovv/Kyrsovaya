using App8;
using System.Net.Http;

public static class ServiceLocator
{
	private static HttpClientService _httpClientService;
	public static IHttpClientService HttpClientService
	{
		get
		{
			if (_httpClientService == null)
			{
				_httpClientService = new HttpClientService(new HttpClient());
			}

			return (IHttpClientService)_httpClientService;
		}
	}
}