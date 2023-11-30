using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class PlayerBuilderTests
    {
        private ShipsBoard shipsBoard;
        private Random random;
        private PlayerBuilder playerBuilder;
        [TestInitialize]
        public void Initialize()
        {
            this.shipsBoard = new ShipsBoard();
            this.random = new Random();
            this.playerBuilder = new PlayerBuilder(shipsBoard);
        }
        [TestMethod()]
        public void PlayerBuilderTest()
        {
            Initialize();
            Assert.IsNotNull(playerBuilder);
        }

        [TestMethod()]
        public void CreateRandomPlayerTest()
        {
            Initialize();
            string expected = "Andrius_Mezencevas";
            Player player = playerBuilder.CreateRandomPlayer(expected);
            Assert.AreEqual(expected, player.Name);
            Assert.AreEqual(this.shipsBoard, player.GetShipsBoard());
        }
    }
}