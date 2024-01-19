var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (httpContxt, next) => {
	if(httpContxt.Request.Query.TryGetValue("a", out var value)){
		Console.WriteLine(value);
	}

	await next();
});

app.Use(async (httpContxt, next) => {
	if(httpContxt.Request.Path.StartsWithSegments("/taalla-html")){
		await httpContxt.Response.WriteAsync("""
			<p>Moi?</p>
		""");
	}
	else {
		await next();
	}
});

app.UseRouting();
app.UseEndpoints(_ => {});

app.MapGet("/", () => "Hello World!");
app.MapGet("/{mypath}", (string mypath, string a) => $"Hello {a}, you are visiting {mypath}");

app.Run();
