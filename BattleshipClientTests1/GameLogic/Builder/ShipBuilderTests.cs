using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class ShipBuilderTests
    {
        [TestMethod()]
        public void ShipBuilderTest()
        {
            Assert.IsNotNull(new ShipBuilder(new ShipsBoard()));
        }

        [TestMethod()]
        public void BuildRandomShipsTest()
        {
            ShipBuilder shipBuilder = new ShipBuilder(new ShipsBoard());
            shipBuilder.BuildRandomShips();
            Assert.IsTrue(shipBuilder.GetBoard().ContainsGreaterThan(0));
            Assert.IsTrue(shipBuilder.GetBoard().ContainsGreaterThan(1));
            Assert.IsTrue(shipBuilder.GetBoard().ContainsGreaterThan(2));
            Assert.IsTrue(shipBuilder.GetBoard().ContainsGreaterThan(3));
            Assert.IsFalse(shipBuilder.GetBoard().ContainsGreaterThan(4));
        }
    }
}