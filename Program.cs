using ProjetoBeta1.Data;
using Microsoft.EntityFrameworkCore;
using ProjetoBeta1.Models;
using MiniValidation;
using NetDevPack.Identity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using NetDevPack.Identity.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MinimalContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityEntityFrameworkContextConfiguration(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("ProjetoBeta1")));

builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration, "AppSettings");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthConfiguration();//
app.UseHttpsRedirection();

app.MapPost("/cadastro", async (
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<AppJwtSettings> appJwtSettings,
        RegisterUser registerUser) =>
    {
        if (registerUser == null)
            return Results.BadRequest("Produto n�o informado");

        if (!MiniValidator.TryValidate(registerUser, out var errors))
            return Results.ValidationProblem(errors);

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors);

        var jwt = new JwtBuilder()
                    .WithUserManager(userManager)
                    .WithJwtSettings(appJwtSettings.Value)
                    .WithEmail(user.Email)
                    .WithJwtClaims()
                    .WithUserClaims()
                    .WithUserRoles()
                    .BuildUserResponse();

        return Results. Ok(jwt);

    }).ProducesValidationProblem()
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status400BadRequest)
      .WithName("RegistroProduto")
      .WithTags("Produto");


app.MapGet("/ListadeCompras", async (
    MinimalContextDb context) =>
    await context.ListaDeCompras.ToListAsync())
    .WithName("GetListadeCompras")
    .WithTags("ListadeCompras");

app.MapGet("/ListadeCompras/{C�digo}", async (
    Guid C�digo,
    MinimalContextDb context) =>

    await context.ListaDeCompras.FindAsync(C�digo)
        is ListaDeCompras listaDeCompras
            ?Results.Ok(listaDeCompras)
            :Results.NotFound())
    .Produces<ListaDeCompras>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetListadeComprasPorC�digo")
    .WithTags("ListadeCompras");

app.MapPost("/ListadeCompras", async (
    MinimalContextDb context,
    ListaDeCompras listaDeCompras) =>
{
    if (!MiniValidator.TryValidate(listaDeCompras, out var errors))  //Caso o MiniValidator n�o consiga validar com sucesso teremos o "errors"//
        return Results.ValidationProblem(errors);

    context.ListaDeCompras.Add(listaDeCompras);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.CreatedAtRoute("GetListadeComprasPorC�digo", new {C�digo = listaDeCompras.C�digo }, listaDeCompras)
        : Results.BadRequest("Houve um problema ao salvar o registro, Att, MSFA.");

}).ProducesValidationProblem()
.Produces<ListaDeCompras>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("PostListadeCompras")
.WithTags("ListadeCompras");

app.MapPut("/listadeCompras/{C�digo}", async (
        Guid C�digo,
        MinimalContextDb context,
        ListaDeCompras listaDeCompras) =>
{
    var listadeComprasBanco = await context.ListaDeCompras.AsNoTracking<ListaDeCompras>()
                                                          .FirstOrDefaultAsync(f=>f.C�digo == C�digo);

    if (listadeComprasBanco == null) return Results.NotFound();

    if (!MiniValidator.TryValidate(listaDeCompras, out var errors))
        return Results.ValidationProblem(errors);

    context.ListaDeCompras.Update(listaDeCompras);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Houve um problema ao salvar o registro, Att, MSFA.");

}).ProducesValidationProblem()
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("PutListadeCompras")
    .WithTags("ListadeCompras");

app.MapDelete("/listadeCompras/{C�digo}", async (
       Guid C�digo,
       MinimalContextDb context) =>
{
    var listaDeCompras = await context.ListaDeCompras.FindAsync(C�digo);
    if (listaDeCompras == null) return Results.NotFound();

    context.ListaDeCompras.Remove(listaDeCompras);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Houve um problema ao salvar o registro, Att, MSFA.");

}).Produces(StatusCodes.Status400BadRequest)
   .Produces(StatusCodes.Status204NoContent)
   .Produces(StatusCodes.Status404NotFound)
   .WithName("DeleteListaDeCompras")
   .WithTags("ListaDeCompras");

app.Run();
