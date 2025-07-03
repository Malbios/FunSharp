namespace FunSharp.Client

open System.Net.Http
open System.Net.Http.Json
open System
open FunSharp.Model

[<RequireQualifiedAccess>]
module GameClient =
    let http = new HttpClient(BaseAddress = Uri("http://localhost:5031/"))

    let startNewGame () = async {
        let! res = http.PostAsync("api/v1/game/start", null) |> Async.AwaitTask
        return! res.Content.ReadFromJsonAsync<Game.Session>() |> Async.AwaitTask
    }

    let movePlayer sessionId x y = async {
        let body = {| SessionId = sessionId; X = x; Y = y |}
        let! res = http.PostAsJsonAsync("api/game/move", body) |> Async.AwaitTask
        return! res.Content.ReadFromJsonAsync<Game.Session>() |> Async.AwaitTask
    }
