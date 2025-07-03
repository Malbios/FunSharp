namespace FunSharp.Model

[<RequireQualifiedAccess>]
module Common =

    type Resource =
        | DataFragment
        | EncryptionKey
        | Blueprint of string

    type Tool =
        | FirewallBypass
        | DataCompressor
        | RecoveryRoutine
