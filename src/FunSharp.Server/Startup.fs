namespace FunSharp.Server

open System
open System.Threading.Tasks
open Blazored.LocalStorage
open Bolero.Remoting.Server
open Bolero.Templating.Server
open Microsoft.AspNetCore.Authentication.Cookies
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http.Json
open Microsoft.Extensions.DependencyInjection
open Bolero.Server
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open Radzen

open FunSharp.Common
open FunSharp.Model

module Startup =
    
    let createPlayerAccountsPersistence (services: IServiceProvider) : Persistence.PlayerAccounts =
        
        let configuration = services.GetService<IOptions<PersistenceConfiguration>>() 
        
        Persistence.PlayerAccounts(configuration.Value.DatabaseFilePath)
        
    let createLogger (services: IServiceProvider) name = services.GetRequiredService<ILoggerFactory>().CreateLogger(name)

    [<EntryPoint>]
    let main args =

        Console.WriteLine("FunSharp Server started. Initializing...")

        let builder = WebApplication.CreateBuilder(args)
        
        builder.Logging
            .ClearProviders()
            .AddConsole()
            .AddDebug()
        |> ignore
        
        builder.Services.AddRadzenComponents() |> ignore

        builder.WebHost.UseStaticWebAssets() |> ignore

        let services = builder.Services

        services
            .AddHttpLogging(fun _ -> ())
            .AddOptions()
            .AddBoleroHost()
            .AddBlazoredLocalStorage()
            .Configure<JsonOptions>(fun (o: JsonOptions) -> JsonSerializer.configure o.SerializerOptions)
        |> ignore
        
        services
            .AddSingleton<Persistence.PlayerAccounts>(createPlayerAccountsPersistence)
        |> ignore

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(fun options ->
                let events = CookieAuthenticationEvents()

                events.OnRedirectToLogin <-
                    fun context ->
                        context.Response.StatusCode <- 401
                        Task.CompletedTask

                events.OnRedirectToAccessDenied <-
                    fun context ->
                        context.Response.StatusCode <- 403
                        Task.CompletedTask

                options.SlidingExpiration <- false

                options.Events <- events)
        |> ignore

        let env = builder.Environment

        if env.IsDevelopment() then
            services.AddHotReload(templateDir = __SOURCE_DIRECTORY__ + "/../FunSharp.Client")
            |> ignore

        services.AddMvc() |> ignore
        services.AddServerSideBlazor() |> ignore

        let app = builder.Build()
        let serviceProvider = app.Services
        
        let logger = "FunSharp.Initialization" |> createLogger serviceProvider

        if env.IsDevelopment() then
            app.UseWebAssemblyDebugging()

        app
            .UseHttpLogging()
            .UseAuthentication()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseAntiforgery()
            .UseBlazorFrameworkFiles()
            .UseEndpoints(fun endpoints ->
                if env.IsDevelopment() then
                    endpoints.UseHotReload()

                endpoints.MapBoleroRemoting() |> ignore
                endpoints.MapBlazorHub() |> ignore
                endpoints.MapFallbackToBolero(Index.page) |> ignore)
        |> ignore

        logger.LogTrace($"Initialization finished - V{Version.current}")
        
        app.Run()

        0
