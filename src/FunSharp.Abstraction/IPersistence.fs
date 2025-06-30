namespace FunSharp.Abstraction

type IPersistence<'T, 'Id when 'T : not struct and 'T : equality and 'T: not null> =
    abstract member Save : 'T -> unit
    abstract member GetById : 'Id -> 'T option
    abstract member DeleteById : 'Id -> unit
    abstract member GetAll : unit -> 'T list
