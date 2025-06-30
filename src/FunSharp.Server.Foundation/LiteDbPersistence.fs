namespace FunSharp.Server.Foundation

open System
open System.Collections.Generic
open System.Linq.Expressions
open Newtonsoft.Json
open LiteDB
open FunSharp.Abstraction

type LiteDbPersistence<'T, 'Id when 'T : not struct and 'T : equality and 'T: not null>
    (collectionName: string, databaseFilePath: string) =
    
    let mapper = FSharpBsonMapper()
    
    do mapper.EnsureRecord<'T>() |> ignore

    let withCollection f =
        
        use db = new LiteDatabase(databaseFilePath, mapper)
        let collection = db.GetCollection<'T>(collectionName)
        f collection

    interface IPersistence<'T, 'Id> with
    
        member _.Save item =
            
            withCollection (fun collection -> collection.Upsert(item) |> ignore)
            
        member _.GetById (id: 'Id) =
            
            withCollection (fun collection -> collection.FindById(BsonValue(id)) |> Option.ofObj)
            
        member _.DeleteById (id: 'Id) =
            
            withCollection (fun collection -> collection.Delete(BsonValue(id)) |> ignore)
            
        member _.GetAll () =
            
            withCollection (fun collection -> collection.FindAll() |> Seq.toList)
