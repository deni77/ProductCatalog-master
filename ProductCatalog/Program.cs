using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Behaviors;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Web.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // dobavih pri update
    });

builder.Services.AddTransient<ExceptionHandlingMiddleware>();


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)); //since 12.0 mediatR

builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // !!!!!!!



builder.Services.AddScoped<FakeDataStore>();

builder.Services.AddControllers();

//add fluentvalidation !!!!!!!!!
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

var config = app.Configuration; // add configuration 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseMiddleware<ExceptionHandlingMiddleware>(); // !!!!

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.UseHttpsRedirection();

//app.MapGet("product/getall", async (IMediator _mediator) =>
//{
//    try
//    {
//        var command = new GetAllProductsQuery();
//        var response = await _mediator.Send(command);
//        return response is not null ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest(ex.Message);
//    }
//});

//app.MapGet("product/getbyid", async (IMediator _mediator, int id) =>
//{
//    try
//    {
//        var command = new GetProductByIdQuery() { Id = id };
//        var response = await _mediator.Send(command);
//        return response is not null ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest(ex.Message);
//    }
//});

//app.MapPost("product/create", async (IMediator _mediator, Product product) =>
//{
//    try
//    {
//        var command = new CreateProductCommand()
//        {
//            Name = product.Name,
//            Description = product.Description,
//            Category = product.Category,
//            Price = product.Price,
//            Active = product.Active,
//        };
//        var response = await _mediator.Send(command);
//        return response is not null ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest(ex.Message);
//    }
//});


//app.MapPut("product/update", async (IMediator _mediator, Product product) =>
//{
//    try
//    {
//        var command = new UpdateProductCommand()
//        {
//            Id = product.Id,
//            Name = product.Name,
//            Description = product.Description,
//            Category = product.Category,
//            Price = product.Price,
//            Active = product.Active,
//        };
//        var response = await _mediator.Send(command);
//        return response is not null ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest(ex.Message);
//    }
//});

//app.MapDelete("product/delete", async (IMediator _mediator, int id) =>
//{
//    try
//    {
//        var command = new DeleteProductCommand() { Id = id };
//        var response = await _mediator.Send(command);
//        return response is not null ? Results.Ok(response) : Results.NotFound();
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest(ex.Message);
//    }
//});

app.Run();
