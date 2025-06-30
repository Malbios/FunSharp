namespace FunSharp.Common.Test

open System
open System.IO
open FunSharp.Abstraction
open FunSharp.Server.Foundation
open Xunit
open Faqt
open Faqt.Operators

type TestModel = {
    Id: Guid
    Text: string
    Number: int
    Timestamp: DateTimeOffset
}

[<RequireQualifiedAccess>]
module Helpers =
    
    let createPersistence<'T, 'Id when 'T : not struct and 'T : equality and 'T: not null>
        (databaseFilePath: string, collectionName: string) =
        
        if File.Exists databaseFilePath then File.Delete databaseFilePath
        LiteDbPersistence<'T, 'Id>(collectionName, databaseFilePath) :> IPersistence<_, _>

[<Trait("Category", "Standard")>]
module ``LiteDbPersistence Tests`` =
    
    [<Fact>]
    let ``GetAll() for new database should return no items`` () =
    
        // Arrange
        let persistence = Helpers.createPersistence<TestModel, Guid>("test.db", "test")
        
        // Act
        let result = persistence.GetAll()
        
        // Assert
        %result.Should().BeEmpty()
        
    [<Fact>]
    let ``GetById() after saving an item should return that item`` () =
    
        // Arrange
        let testItem = {
            Id = Guid.Parse "44b8ae0d-37b3-4be3-8992-e7f6832b472a"
            Text = "abc"
            Number = 123
            Timestamp = DateTimeOffset(2023, 12, 25, 15, 30, 0, TimeSpan.FromHours(-5.0))
        }
        
        let persistence = Helpers.createPersistence<TestModel, Guid>("test.db", "test")
        
        %persistence.Save(testItem)
        
        // Act
        let result = persistence.GetById(testItem.Id)
        
        // Assert
        %result.Should().BeSome()
        %result.Value.Should().Be(testItem)
        
    [<Fact>]
    let ``GetAll() after saving an item should return a single-item collection with that item`` () =
    
        // Arrange
        let testItem = {
            Id = Guid.Parse "44b8ae0d-37b3-4be3-8992-e7f6832b472a"
            Text = "abc"
            Number = 123
            Timestamp = DateTimeOffset(2023, 12, 25, 15, 30, 0, TimeSpan.FromHours(-5.0))
        }
        
        let persistence = Helpers.createPersistence<TestModel, Guid>("test.db", "test")
        
        %persistence.Save(testItem)
        
        // Act
        let result = persistence.GetAll()
        
        // Assert
        %result.Should().HaveLength(1)
        %result.Head.Should().Be(testItem)
