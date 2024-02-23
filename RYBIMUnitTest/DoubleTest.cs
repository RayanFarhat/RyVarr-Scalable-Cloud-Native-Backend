using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RYBIM.Mathematics;

namespace RYBIMUnitTest
{
    [TestClass]
    public class DoubleTest
    {
        [TestMethod]
        public void IsEqual()
        {
            Assert.IsTrue(Num.IsEqual(3.00223, 3.00228) && !Num.IsEqual(3.00223, 3.00238));
        }
        [TestMethod]
        public void IsFirstBiggerThanSecond()
        {
            Assert.IsTrue(Num.IsFirstBiggerThanSecond(3.0023, 3.0021) && !Num.IsFirstBiggerThanSecond(3.00229, 3.0022));
        }
        [TestMethod]
        public void IsFirstBiggerOrEqualThanSecond()
        {
            Assert.IsTrue(Num.IsFirstBiggerOrEqualThanSecond(3.00223, 3.00228) && !Num.IsFirstBiggerOrEqualThanSecond(3.00209, 3.0022));
        }
        [TestMethod]
        public void IsFirstSmallerThanSecond()
        {
            Assert.IsTrue(Num.IsFirstSmallerThanSecond(3.0024, 3.0026) && !Num.IsFirstSmallerThanSecond(3.00223, 3.00228));
        }
        [TestMethod]
        public void IsFirstSmallerOrEqualThanSecond()
        {
            Assert.IsTrue(Num.IsFirstSmallerOrEqualThanSecond(3.00223, 3.00228) && !Num.IsFirstSmallerOrEqualThanSecond(3.0023, 3.0021));
        }
    }
}
