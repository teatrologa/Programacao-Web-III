using Aula_03.Filters;
using Aula_03_Core.Interface;
using Aula_03_Core.Service;
using Aula_03_Infra_Data.Ropository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<GaranteProdutoExisteActionFilter>(); //filtro add, logo, implementando uma instancia de produto service.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add global filters - essa ? uma forma de add filtro em TODO o programa
builder.Services.AddMvc(options =>
{
    options.Filters.Add<LogResultFilter>();
    options.Filters.Add<GeneralExceptionFilter>();
});

builder.Services.AddControllers().ConfigureApiBehaviorOptions(op =>
{
    op.SuppressModelStateInvalidFilter = true;
});


//????
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
