namespace FunSharp.Client

open System
open Blazored.LocalStorage
open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Bolero.Remoting.Client
open Microsoft.Extensions.DependencyInjection
open Radzen

module MyApp =

    [<EntryPoint>]
    let Main args =
        
        let builder = WebAssemblyHostBuilder.CreateDefault(args)
        builder.Services.AddBoleroRemoting(builder.HostEnvironment) |> ignore
        builder.Services.AddRadzenComponents() |> ignore
        builder.Services.AddBlazoredLocalStorage() |> ignore

        builder.RootComponents.Add<Main.ClientApplication>("#main")

        builder.Build().RunAsync() |> ignore
        
        0
