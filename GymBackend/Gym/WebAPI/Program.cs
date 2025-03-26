var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add distributed memory cache
//builder.Services.AddDistributedMemoryCache();

// Add session services
//builder.Services.AddSession(options =>
//{
//options.IdleTimeout = TimeSpan.FromMinutes(30);
//options.Cookie.HttpOnly = true;
//options.Cookie.IsEssential = true;
//});

// Add HttpContextAccessor
//builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//CORS: Permitiendo cualquier origen , en cualquier metodo y con cualquier encabezado.
app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    //.AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use session
//app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();