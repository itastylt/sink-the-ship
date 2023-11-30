using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Strategy;
using BattleshipClient.GameLogic.Strategy.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class ReadyFacadeTests
    {
        protected ShipsBoard Board;
        private Player Player;
        private ReadyFacade Facade;
        private ShipHub Hub;

        [TestInitialize]
        public void Init()
        {
            this.Board = new ShipsBoard();
            this.Hub = new ShipHub();
            this.Player = new Player("Andrius_Mezencevas");
            this.Facade = new ReadyFacade();
        }

        [TestMethod()]
        public void ReadyFacadeTest()
        {
            Init();

            Assert.IsNotNull(Facade);
            Assert.IsFalse(Facade.GetBoard().ContainsGreaterThan(0));
            Assert.IsNull(Facade.GetPlayer());
        }

        private static IEnumerable<object[]> TestMethodInput
        {
            get
            {
                return new[]
                {
                    new object[] {"Boat", 1, new SingleShot(), 1, 0, 0 },
                    new object[] {"Lavantier", 2, new HorizontalShot(), 2, 0, 0 },
                    new object[] {"Submarine", 3, new VerticalShot(),3, 0, 0 },
                    new object[] {"Destroyer", 4, new DiagonalShot(), 4, 0, 0 },
                };

            }

        }

        [TestMethod()]
        [DynamicData(nameof(TestMethodInput))]

        public void FormBoardTest(string ship, int size, ICannonStrategy cannon, int expected, int x, int y)
        {
            Init();

            PlacedShip placedShip = new PlacedShip(ship, x, y, size, 90, cannon);
            Facade.FormBoard(new List<PlacedShip>(1) { placedShip });

            Assert.AreEqual(expected, this.Facade.GetBoard().Board[x, y]);

        }


        [TestMethod()]
        public void CreatePlayerTest()
        {
            this.Init();
            List<Player> players = Facade.CreatePlayer(this.Player.Name);
            Assert.AreEqual(ShipPlayers.GetPlayer(this.Player.Name), players[0]);
        }

        [TestMethod()]
        public void CreateRandomPlayerTest()
        {
            Init();
            Facade.CreateRandomPlayer(this.Player);
            Assert.AreEqual(this.Player, ShipPlayers.GetPlayer(this.Player.Name));
        }

        [TestMethod()]
        public void SetBoardTest()
        {
            Init();
            ShipsBoard board = new ShipsBoard();
            board.PlaceShip(new IBoat(0, 0));

            Facade.SetBoard(board);
            Assert.AreEqual (board, Facade.GetBoard());
        }

        [TestMethod()]
        public void StartPlayersTest()
        {
            Init();

            Player player2 = new Player("Ne_Andrius_Mezencevas");
            List<Player> players = Facade.CreatePlayer(this.Player.Name);
            Assert.AreEqual(this.Player.Name, players[0].Name);
            players = Facade.CreateRandomPlayer(player2);
            Assert.AreEqual(this.Player.Name, players[0].Name);
            Assert.AreEqual(2, players.Count);
            Facade.StartPlayers(players);
            Assert.AreNotEqual(ShipPlayers.GetPlayer(this.Player.Name).GetState(), ShipPlayers.GetPlayer(player2.Name).GetState());

        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException),"Player list is empty!")]
        public void UnreadyPlayerTest()
        {
            Init();
            Facade.CreatePlayer(this.Player.Name);
            Assert.IsNotNull(ShipPlayers.GetPlayer(this.Player.Name));
            Assert.AreEqual(this.Player.Name, ShipPlayers.GetPlayer(this.Player.Name).Name );

            Facade.UnreadyPlayer(this.Player.Name);
            Assert.IsInstanceOfType(ShipPlayers.GetPlayer(this.Player.Name), typeof(InvalidOperationException));
        }

        [TestMethod()]
        public void RestorePlayerTest()
        {
            Assert.Fail("Not implemented");
        }
    }
}