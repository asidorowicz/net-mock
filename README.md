[![Build Status](https://dev.azure.com/m00ndark/net-mock/_apis/build/status/net-mock-ci)](https://dev.azure.com/m00ndark/net-mock/_build/latest?definitionId=1) [![NuGet Badge](https://buildstats.info/nuget/NetMock)](https://www.nuget.org/packages/NetMock/)

# net-mock

NetMock is a .NET network service mock framework, inspired by [Moq](https://github.com/moq) syntax, providing the ability to mock REST and web socket APIs from within a test framework of choice.

## Examples

A set of examples to show usage and possibilities are part of the solution, available [here](/src/Tests/NetMock.Tests/RestMockTest.cs).

## Status

The following table shows the implementation status of currenly planned features.

### Service mock
Component | Feature | Status
--- | --- | ---
Configuration | `ActivationStrategy`&nbsp;`{ Manual, AutomaticOnCreation }` (default is `AutomaticOnCreation`) | &#10003;
&nbsp; | `MockBehavior`&nbsp;`{ Loose, Strict }` (default is `Loose`) | &#10003;
&nbsp; | `PrintReceivedRequestsOnTearDown` (default is `false`) | &#10003;
Mock creation | `CreateRestMock(int port, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateRestMock(string basePath, int port, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(string basePath, int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(int port, X509Certificate2 certificate, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate, MockBehavior mockBehavior)` | &#10003;

### REST mock
Component | Feature | Status
--- | --- | ---
HTTP methods | `Get `, `Post `, `Put `, `Patch`, `Delete `, `Head `, `Options `, `Trace `, `Connect` | &#10003;
Configuration | `StaticHeaders` | &#10003;
&nbsp; | `DefaultResponseStatusCode` (default is `NotImplemented`) | &#10003;
&nbsp; | `UndefinedQueryParameterHandling`&nbsp;`{ Ignore, Fail }` (default is `Fail`) | &#10003;
&nbsp; | `UndefinedHeaderHandling`&nbsp;`{ Ignore, Fail }` (default is `Ignore`) | &#10003;
&nbsp; | `MockBehavior`&nbsp;`{ Loose, Strict }` (default is `Loose`) | &#10003;
&nbsp; | `InterpretBodyAsJson` (default is `true`) | &#10003;
Request setup | `Setup(Method method, string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupGet(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupPost(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupPut(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupPatch(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupDelete(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupHead(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupOptions(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupTrace(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupConnect(string path, params IMatch[] matches)` | &#10003;
Request verification | `Verify(Method method, string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyGet(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyPost(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyPut(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyPatch(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyDelete(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyHead(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyOptions(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyTrace(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyConnect(string path, params IMatch[] matches, Times times)` | &#10003;
Response setup | `Returns(params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(int statusCode, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(HttpStatusCode statusCode, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(object body, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(int statusCode, object body, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(HttpStatusCode statusCode, object body, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(Func<int> statusCodeProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, int> statusCodeProvider)` | &#10003;
&nbsp; | `Returns(Func<HttpStatusCode> statusCodeProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, HttpStatusCode> statusCodeProvider)` | &#10003;
&nbsp; | `Returns(Func<object> bodyProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, object> bodyProvider)` | &#10003;
&nbsp; | `Returns(Func<IEnumerable<AttachedHeader>> headersProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, IEnumerable<AttachedHeader>> headersProvider)` | &#10003;
&nbsp; | `Returns(Func<(int StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, (int StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<(HttpStatusCode StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, (HttpStatusCode StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<(int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<(HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<(object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns(Func<IReceivedRequest, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, int> statusCodeProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, int> statusCodeProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, HttpStatusCode> statusCodeProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, HttpStatusCode> statusCodeProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, object> bodyProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, object> bodyProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, IEnumerable<AttachedHeader>> headersProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, IEnumerable<AttachedHeader>> headersProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, (int StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, (int StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, (HttpStatusCode StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, (HttpStatusCode StatusCode, object Body)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | `Returns<T1>(Func<IReceivedRequest, T1, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)` | &#10003;
&nbsp; | Above generic signatures with generic arguments ranging up to T9 (additional 144 overloads)| &#10003;
&nbsp; | `Callback(Action callback)` | &#10003;
&nbsp; | `Callback(Action<IReceivedRequest> callback)` | &#10003;
&nbsp; | `Callback<T1>(Action<T1> callback)` | &#10003;
&nbsp; | `Callback<T1>(Action<IReceivedRequest, T1> callback)` | &#10003;
&nbsp; | Above generic signatures with generic arguments ranging up to T9 (additional 16 overloads)| &#10003;
Parameter matching | `Parameter.Is<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.Is<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.Is(string name, Func<string, bool> condition)` | &#10003;
&nbsp; | `Parameter.Is<TValue>(string name, Func<TValue, bool> condition)` | &#10003;
&nbsp; | `Parameter.IsNot<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.IsNot<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.IsAny(string name)` | &#10003;
&nbsp; | `Parameter.IsAny<TValue>(string name)` | &#10003;
&nbsp; | `Parameter.StartsWith<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.StartsWith<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.EndsWith<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.EndsWith<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.Contains<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.Contains<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.NotContains<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Parameter.NotContains<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.StartsWithWord<TValue>(string name, TValue word)` | &#10003;
&nbsp; | `Parameter.StartsWithWord<TValue>(string name, TValue word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.EndsWithWord<TValue>(string name, TValue word)` | &#10003;
&nbsp; | `Parameter.EndsWithWord<TValue>(string name, TValue word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.ContainsWord<TValue>(string name, TValue word)` | &#10003;
&nbsp; | `Parameter.ContainsWord<TValue>(string name, TValue word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.NotContainsWord<TValue>(string name, TValue word)` | &#10003;
&nbsp; | `Parameter.NotContainsWord<TValue>(string name, TValue word, CompareCase compareCase)` | &#10003;
Header matching | `Header.Is<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Header.Is<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.Is(string name, Func<string, bool> condition)` | &#10003;
&nbsp; | `Header.Is<TValue>(string name, Func<TValue, bool> condition)` | &#10003;
&nbsp; | `Header.IsNot<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Header.IsNot<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.IsSet(string name)` | &#10003;
&nbsp; | `Header.IsNotSet(string name)` | &#10003;
&nbsp; | `Header.Contains<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Header.Contains<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.NotContains<TValue>(string name, TValue value)` | &#10003;
&nbsp; | `Header.NotContains<TValue>(string name, TValue value, CompareCase compareCase)` | &#10003;
Body matching | `Body.Is<TValue>(TValue value)` | &#10003;
&nbsp; | `Body.Is<TValue>(TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.Is(Func<string, bool> condition)` | &#10003;
&nbsp; | `Body.Is<TValue>(Func<TValue, bool> condition)` | &#10003;
&nbsp; | `Body.IsNot<TValue>(TValue value)` | &#10003;
&nbsp; | `Body.IsNot<TValue>(TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.IsAny()` | &#10003;
&nbsp; | `Body.IsAny<TValue>()` | &#10003;
&nbsp; | `Body.IsEmpty()` | &#10003;
&nbsp; | `Body.IsNotEmpty()` | &#10003;
&nbsp; | `Body.Contains<TValue>(TValue value)` | &#10003;
&nbsp; | `Body.Contains<TValue>(TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.NotContains<TValue>(TValue value)` | &#10003;
&nbsp; | `Body.NotContains<TValue>(TValue value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.ContainsWord<TValue>(TValue word)` | &#10003;
&nbsp; | `Body.ContainsWord<TValue>(TValue word, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.NotContainsWord<TValue>(TValue word)` | &#10003;
&nbsp; | `Body.NotContainsWord<TValue>(TValue word, CompareCase compareCase)` | &#10003;
Verification hit count matching | `Times.Never` | &#10003;
&nbsp; | `Times.Once` | &#10003;
&nbsp; | `Times.Twice` | &#10003;
&nbsp; | `Times.AtLeastOnce` | &#10003;
&nbsp; | `Times.AtMostOnce` | &#10003;
&nbsp; | `Times.Exactly(int callCount)` | &#10003;
&nbsp; | `Times.AtLeast(int callCount)` | &#10003;
&nbsp; | `Times.AtMost(int callCount)` | &#10003;
&nbsp; | `Times.Between(int callCountFrom, int callCountTo)` | &#10003;

<!--
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
-->

### Web socket mock

No implementation started.

## Inspiration

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: false);
restMock.Setup(Method.Get, "/alive").Returns(new AliveMessage());
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Get, "/alive", Times.Once);

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: new User() { UserId = "abc" })
        .Returns(new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc" });
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Post, "/user", Times.Once)

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = 'abc' }")
        .Returns("{ 'id' = '698fa58a-8e9c-4607-b6c3-b5da19751a04', 'userId' = 'abc' }");
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = 'xyz' }")
        .Returns("{ 'id' = '607a755d-3ded-4a42-9a96-03d9ecfd31da', 'userId' = 'xyz' }");
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Post, "/user", Times.Twice);
restMock.Verify(Method.Post, "/user", body: "{ 'userId' = 'abc' }", Times.Once);
restMock.Verify(Method.Post, "/user", body: "{ 'userId' = 'xyz' }", Times.Once);

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
int getUserCallCount = 0;
string encryptedConnectionString = "...";
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = '' }")
        .ReturnsError(400);
restMock.Setup(Method.Post, "/user", body: "{ 'userDirectory' = '' }")
        .ReturnsError(400, body: "User directory missing, what were you thinking?");
restMock.Setup(Method.Get, "/user/{id}", Parameter.Is("id", "698fa58a-8e9c-4607-b6c3-b5da19751a04"))
        .Returns(new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc" });
restMock.Setup(Method.Get, "/user")
        .Callback(() => { getUserCallCount++; });
restMock.Setup(Method.Get, "/user/detail?filter={filter}&orderby={orderby}",
               Parameter.Is("filter", "userDirectory eq 'acme'", CompareCase.Insensitive),
               Parameter.EndsWithWord("orderby", "desc", CompareCase.Insensitive))
        .Returns(new[]
  {
     new User() { ID = new Guid("607a755d-3ded-4a42-9a96-03d9ecfd31da"), UserId = "xyz", UserDirectory = "ACME" },
     new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc", UserDirectory = "ACME" }
  });
restMock.Setup(Method.Get, "/product?filter={filter}&orderby={orderby}",
               Parameter.Is("filter", filter => filter.Split(' ').Any(word => word.ToLower() == "and")),
               Parameter.IsAny("orderby"))
        .ReturnsError(500);
restMock.Setup(Method.Get, "/secure/item",
               Header.Is("X-Acme-User", "UserDirectory=INTERNAL; UserId=sa", CompareCase.Insensitive))
        .Returns(new[] { new DataConnection() { Connectionstring = Decrypt(encryptedConnectionString) } },
                 Header.Attach("Cache-Control", "private"));
restMock.Setup(Method.Get, "/secure/item",
               Header.IsNot("X-Acme-User", "UserDirectory=INTERNAL; UserId=sa", CompareCase.Insensitive),
               Header.IsNotSet("X-Acme-Security"))
        .Returns(new[] { new DataConnection() { Connectionstring = encryptedConnectionString } });
serviceMock.Activate();

// act
...

// assert
Assert.AreEqual(5, getUserCallCount);
restMock.Verify(Method.Get, "/user/detail?filter={filter}&orderby={orderby}", Times.Never,
                Parameter.IsAny("filter"),
                Parameter.ContainsWord("orderby", "userDirectory", CompareCase.Insensitive));

// teardown
serviceMock.Teardown();
```
