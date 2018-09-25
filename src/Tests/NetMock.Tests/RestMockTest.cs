using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using NetMock.Rest;
using NetMock.Tests.Model;
using NetMock.Tests.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Method = NetMock.Rest.Method;
using Parameter = NetMock.Rest.Parameter;

namespace NetMock.Tests
{
	[TestFixture]
	public class RestMockTest
	{
		private Client _client;
		private Client _secureClient;

		[OneTimeSetUp]
		public void Initialize()
		{
			_client = new Client(Uri.UriSchemeHttp, "/api/v1", 9001);
			_secureClient = new Client(Uri.UriSchemeHttps, "/api/v1", 9001);
		}

		[Test]
		public void Scenario01_Simple()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock.Setup(Method.Get, "/alive").Returns(message);
				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void Scenario01_Simple_Secure()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateSecureRestMock("/api/v1", 9001,
					certificateThumbprint: "78ac133aaf23b4d39e701b342cb5a5eb9a3924a0",
					storeName: StoreName.My,
					storeLocation: StoreLocation.LocalMachine);
				restMock.Setup(Method.Get, "/alive").Returns(message);
				serviceMock.Activate();

				// act
				IRestResponse response = _secureClient.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void Scenario02_UriSegmentParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{id}", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message/{id}", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void Scenario03_QueryParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message?msgid={id}&x=y", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/message?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message?msgid={id}&x=y", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void Scenario04_UriSegmentParameter_QueryParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				string[] categories = { "FRUIT", "MEAT", "JAM" };
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{category}?msgid={id}&x=y",
						Parameter.IsAny<Guid>("id"),
						Parameter.Is("category", x => categories.Contains(x.ToUpper())))
					.Returns(message);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/message/jam?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message/jam?msgid={id}&x=y", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void Scenario05_Body_PrintReceivedRequests()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message/reverse", Body.Is(requestMessage))
					.Returns(responseMessage);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Post("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				_client.Get("/alive");
				_client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");
				_client.Get("/message/jam?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse", Body.Is(requestMessage), Times.Once);
				restMock.VerifyPost("/message/reverse", Body.Is("{ 'Text': 'torraP' }"), Times.Never);

				restMock.PrintReceivedRequests(" ", x => x.Method, x => x.Uri.ToString(), x => $"(body length: {x.Body.Length})");
			}
		}

		[Test]
		public void Scenario06_NoResponseDefined_DefaultResponseStatusCode()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock.DefaultResponseStatusCode = HttpStatusCode.NoContent;

				restMock.Setup(Method.Get, "/alive");

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/alive");

				// assert
				Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
				JsonAssert.AreEqual(string.Empty, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);

				restMock.PrintReceivedRequests(" ", x => x.Method, x => x.Uri.ToString(), x => $"(body length: {x.Body.Length})");
			}
		}

		[Test]
		public void Scenario07_StatusCodeAndResponseHeaders()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message/reverse/store", Body.Is(requestMessage))
					.Returns(HttpStatusCode.Created, ("X-Message-Mode", "normal"), ("X-Message-Case-Sensitive", "true"));

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Post("/message/reverse/store", body: JsonConvert.SerializeObject(requestMessage));

				// assert
				Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
				CollectionAssert.Contains(response.Headers.Select(x => (x.Name, x.Value.ToString())), ("X-Message-Mode", "normal"));
				CollectionAssert.Contains(response.Headers.Select(x => (x.Name, x.Value.ToString())), ("X-Message-Case-Sensitive", "true"));
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse/store", Body.Is(requestMessage), Times.Once);

				restMock.PrintReceivedRequests(" ", x => x.Method, x => x.Uri.ToString(), x => $"(body length: {x.Body.Length})");
			}
		}
	}
}
