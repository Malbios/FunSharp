namespace FunSharp.Model

open System

[<RequireQualifiedAccess>]
module Game =
        
    type NodeType =
        | Empty
        | DataVault
        | Firewall
        | Corrupted
        | MemoryCore
        | Uplink

    type Node = {
        Id: int * int
        NodeType: NodeType
        Explored: bool
        Resources: Common.Resource list
        Corrupted: bool
    }

    type GameMap = Node list

    type Session = {
        Id: Guid
        Player: Player.Character
        Map: GameMap
        Turn: int
        Instability: int
    }

    module Session =
        
        let create = {
            Id = Guid.NewGuid()
            Player = Player.Character.create
            Map = List.empty
            Turn = 0
            Instability = 0
        }
