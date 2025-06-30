namespace FunSharp.Server.Foundation
    
open System
open FunSharp.Abstraction
open FunSharp.Model

type PlayerAccountStore(databaseFilePath: string) =
    
    let persistence: IPersistence<PlayerAccount, Guid> = LiteDbPersistence("player_accounts", databaseFilePath)

    member _.Save(account: PlayerAccount) = persistence.Save account

    member _.FindById(id: Guid) = persistence.GetById id

    member _.Delete(id: Guid) = persistence.DeleteById id

    member _.GetAll() = persistence.GetAll()
