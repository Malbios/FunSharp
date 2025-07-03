namespace FunSharp.Server

open System
open Microsoft.AspNetCore.Mvc
open FunSharp.Model

type MoveRequest = {
    SessionId: Guid
    X: int
    Y: int
}

[<ApiController>]
[<Route("api/v1/game")>]
type GameController(sessionsPersistence: Persistence.GameSessions) =
    
    [<HttpPost("start")>]
    member _.StartNewGame() =
        printfn $"Start New Game!"
        
        Game.Session.create
        |> sessionsPersistence.CreateGame
        |> ActionResult<Game.Session>

    [<HttpPost("move")>]
    member _.MovePlayer(
        [<FromBody>] move: MoveRequest
    ) =
        printfn $"MoveRequest: {move.SessionId}"
        
        move.SessionId
        |> sessionsPersistence.GetGame
        |> Option.map (fun session ->
            let updatedPlayer = {
                session.Player with Position = (move.X, move.Y)
            }
            
            {
                session with
                    Player = updatedPlayer
                    Turn = session.Turn + 1
                    Instability = session.Instability + 1
            }
            |> sessionsPersistence.UpdateGame
        )
        |> ActionResult<Game.Session option>
