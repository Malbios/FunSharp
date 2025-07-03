namespace FunSharp.Server
    
open System
open FunSharp.Abstraction
open FunSharp.Model
open FunSharp.Server.Foundation

[<RequireQualifiedAccess>]
module Persistence =
        
    type PlayerAccounts(databaseFilePath: string) =
        
        let persistence: IPersistence<Player.Account, Guid> = LiteDbPersistence("player_accounts", databaseFilePath)

        member _.Save(account: Player.Account) =
            persistence.Save account
            account

        member _.FindById(id: Guid) =
            persistence.GetById id

        member _.Delete(id: Guid) =
            persistence.DeleteById id

        member _.GetAll() =
            persistence.GetAll()
        
    type GameSessions(databaseFilePath: string) =

        let persistence: IPersistence<Game.Session, Guid> = LiteDbPersistence("game_sessions", databaseFilePath)

        member _.CreateGame(session: Game.Session) =
            persistence.Save(session)
            session

        member _.UpdateGame(session: Game.Session) =
            persistence.Save(session)
            session

        member _.GetGame(id: Guid) : Game.Session option =
            persistence.GetById(id)

        member _.DeleteGame(id: Guid) =
            persistence.DeleteById(id)
