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
open Radzen

open FunSharp.Common

module Startup =

    [<EntryPoint>]
    let main args =

        Console.WriteLine("Application started. Initializing...")

        let builder = WebApplication.CreateBuilder(args)
        
        builder.Services.AddRadzenComponents() |> ignore

        builder.WebHost.UseStaticWebAssets() |> ignore

        let services = builder.Services
        let configuration = builder.Configuration

        services
            .AddHttpLogging(fun _ -> ())
            .AddOptions()
            .AddBoleroHost()
            .AddBlazoredLocalStorage()
            .Configure<JsonOptions>(fun (o: JsonOptions) -> JsonSerializer.configure o.SerializerOptions)
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

        app.Run()

        0
