namespace FunSharp.Model

open System

[<RequireQualifiedAccess>]
module Player =

    type Inventory = {
        Resources: Map<Common.Resource, int>
        Tools: Set<Common.Tool>
    }

    module Inventory =
        
        let create = {
            Resources = Map.empty
            Tools = Set.empty
        }

    type Character = {
        Position: int * int
        Integrity: int
        Inventory: Inventory
    }

    module Character =
        
        let create = {
            Position = 0, 0
            Integrity = 100
            Inventory = Inventory.create
        }

    type Account = {
        Id: Guid
        Name: string
        Password: string
    }

    module Account =
        
        let create name password = {
            Id = Guid.NewGuid ()
            Name = name
            Password = password
        }
