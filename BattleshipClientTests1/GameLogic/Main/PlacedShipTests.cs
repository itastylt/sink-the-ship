using BattleshipClient.GameLogic.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class PlacedShipTests
    {
        [TestMethod()]
        public void FireWeapon_CallsCannonFire()
        {
            // Arrange
            var mockCannon = new Mock<ICannonStrategy>();
            var placedShip = new PlacedShip("Type", 1, 2, 3, 90, mockCannon.Object);

            var mockOpponent = new Mock<Player>();
            int xCoordinate = 5;
            int yCoordinate = 6;
            int flag = 1;

            // Act
            placedShip.FireWeapon(mockOpponent.Object, xCoordinate, yCoordinate, flag);

            // Assert
            mockCannon.Verify(c => c.Fire(mockOpponent.Object, xCoordinate, yCoordinate, flag), Times.Once);
        }

        [TestMethod()]
        public void Clone_ReturnsClonedInstance()
        {
            // Arrange
            var mockCannon = new Mock<ICannonStrategy>();
            var originalShip = new PlacedShip("Type", 1, 2, 3, 90, mockCannon.Object);

            // Act
            var clonedShip = (PlacedShip)originalShip.Clone();

            // Assert
            Assert.AreEqual(originalShip.Type, clonedShip.Type);
            Assert.AreEqual(originalShip.X, clonedShip.X);
            Assert.AreEqual(originalShip.Y, clonedShip.Y);
            Assert.AreEqual(originalShip.Size, clonedShip.Size);
            Assert.AreEqual(originalShip.Angle, clonedShip.Angle);
            Assert.AreSame(originalShip.Cannon, clonedShip.Cannon); // Verify same cannon strategy instance
        }
    }
}
