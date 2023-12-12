using BattleshipClient.GameLogic.Factory;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class ShipPlayersTests
    {
        IShip ship;

        [TestInitialize]
        public void Setup()
        {
            ShipPlayers.Clear();
            ship = new IBoat(0,0);
        }

        [TestMethod()]
        public void AddPlayer_AddsPlayerToList()
        {
            // Arrange
            var player = new Player("paulius");

            // Act
            var result = ShipPlayers.AddPlayer(player);

            // Assert
            Assert.IsTrue(result.Contains(player));
        }

        [TestMethod()]
        public void GetPlayer_ReturnsPlayerByName()
        {
            // Arrange
            var player = new Player("TestPlayer");
            ShipPlayers.AddPlayer(player);

            // Act
            var retrievedPlayer = ShipPlayers.GetPlayer("TestPlayer");

            // Assert
            Assert.AreEqual(player, retrievedPlayer);
        }

        [TestMethod()]
        public void GetPlayerOpponent_ReturnsOpponent()
        {
            // Arrange
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            ShipPlayers.AddPlayer(player1);
            ShipPlayers.AddPlayer(player2);

            // Act
            var opponent = ShipPlayers.GetPlayerOpponent("Player1");

            // Assert
            Assert.AreEqual(player2, opponent);
        }
        [TestMethod()]
        public void UpdatePlayer_UpdatesPlayerCorrectly()
        {
            // Arrange
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            ShipPlayers.AddPlayer(player1);
            ShipPlayers.AddPlayer(player2);

            // Update player1
            var updatedPlayer1 = new Player("UpdatedPlayer1");

            // Act
            ShipPlayers.UpdatePlayer("Player1", updatedPlayer1);

            // Assert
            var retrievedPlayer1 = ShipPlayers.GetPlayer("UpdatedPlayer1");
            Assert.AreEqual(updatedPlayer1, retrievedPlayer1);
        }

        [TestMethod()]
        public void RemovePlayer_RemovesPlayerFromList()
        {
            // Arrange
            var player = new Player("TestPlayer");
            ShipPlayers.AddPlayer(player);

            // Act
            ShipPlayers.RemovePlayer("TestPlayer");

            // Assert
            Assert.AreEqual(0, ShipPlayers.PlayerCount());
        }

        [TestMethod()]
        public void EndPlayer_ReturnsNullIfNoGameEnd()
        {
            // Arrange
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            ShipPlayers.AddPlayer(player1);
            ShipPlayers.AddPlayer(player2);

            // Act
            var gameEnd = ShipPlayers.EndPlayer();

            // Assert
            Assert.IsNull(gameEnd);
        }

        [TestMethod()]
        public void EndPlayer_ReturnsPlayerIfGameEnd()
        {
            // Arrange
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            player1.GetShipsBoard().PlaceShip(ship);
            ShipPlayers.AddPlayer(player1);
            ShipPlayers.AddPlayer(player2);

            // Act
            var gameEnd = ShipPlayers.EndPlayer();

            // Assert
            Assert.AreEqual(player1, gameEnd);
        }
    }
}