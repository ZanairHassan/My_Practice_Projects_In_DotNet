using System.Net.Mime;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddControllersWithViews();
app.UseRouting();
app.MapControllerRoute(
    name: "Default",
    pattern: "{Controller=Home}/{Action=index}/{id}"
    );

app.MapGet("/", (HttpContext context) =>
{
    string html = @"<html>
                <body>
                <h1> Hello World !</h1>
                <br/>
                Welcom to this new World
                </body>
                </html>";
    WriteHtml(context, html);
});

app.Run();
void WriteHtml(HttpContext context, string html)
{
    context.Response.ContentType = MediaTypeNames.Text.Html;
    context.Response.ContentLength=Encoding.UTF8.GetByteCount(html);
    context.Response.WriteAsync(html);
}