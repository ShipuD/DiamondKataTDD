using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DiamondKata;
using System;
using System.Linq;

namespace DiamondKataTest
{
    [TestClass]
    public class DiamondTest
    {
        private readonly Diamond _diamond;
        private const string InvalidCharMessage = "Invalid character"; //Should be in const file

        public DiamondTest()
        {
            _diamond = new Diamond();
        }

        [TestMethod]
        public void NonEmpty()
        {
           List<string> lst = _diamond.Generate('C');
           Assert.IsTrue(lst.Count > 0);
        }

        [TestMethod]
        public void ShouldThrowExceptionForCharStar()
        {
            try
            {
                _diamond.Generate('*');
            }
            catch (Exception e)
            {
                Assert.AreEqual<string>(InvalidCharMessage, e.Message);
            }
        }

        [TestMethod]
        public void DiamondShouldContainFirstCharForInputA()
        {
            List<string> lst = _diamond.Generate('A');
            //Console.WriteLine(lst[0]);
            Assert.IsTrue(lst.Count ==1 && lst[0] == "A");
        }

        [TestMethod]
        public void DiamondShouldContainOnlyUppercasesForLowerCaseInput()
        {
            List<string> lst = _diamond.Generate('c');
            
            lst.All(r => 
            {
                return r.Trim().All(ch => char.IsUpper(ch));
            });
            
        }
        [TestMethod]
        public void LastRowShouldAlwaysBeA()
        {
            List<string> lst = _diamond.Generate('D');
            //Console.WriteLine(lst[0]);
            Assert.IsTrue(lst.Count > 0 && lst[lst.Count -1].Trim() == "A");
        }

        [TestMethod]
        public void LastRowShouldAlwaysBeSameAsFirstWithSpaces()
        {
            List<string> lst = _diamond.Generate('D');            
            Assert.IsTrue(lst.Count > 0 && lst[lst.Count - 1] == lst[0]);
        }

        [TestMethod]
        public void AShouldBeIntheMiddleOfFirstRow()
        {   
            var topRow = _diamond.Generate('D').ToArray()[0];
            var leftHalf = topRow.Substring(0, topRow.Length / 2);
            var righttHalf = topRow.Substring((topRow.Length / 2) + 1);

            Assert.IsTrue(leftHalf.Reverse().SequenceEqual(righttHalf));
        }
        [TestMethod]
        public void ShouldHaveSameWidthAndHeight()
        {
            var diamondInList = _diamond.Generate('E').ToList();
            Assert.IsTrue(diamondInList.All(r => r.Length == diamondInList.Count));
        }

        [TestMethod]
        public void ShouldBeHorizontallySymmetric()
        {
            var diamondInArray = _diamond.Generate('D').ToArray();
            var half = diamondInArray.Length / 2;
            var topHalf = diamondInArray[..half];
            var bottomHalf = diamondInArray[(half + 1)..];

            Assert.IsTrue(topHalf.Reverse().SequenceEqual(bottomHalf));
        }

        [TestMethod]
        public void ShouldBeVerticallySymmetric()
        {
            Assert.IsTrue(_diamond.Generate('D').ToArray()
                        .All(r =>
                        {
                            var half = r.Length / 2;
                            var leftRowOfHalf = r[..half];
                            var rightHalfOfRow = r[(half + 1)..];
                            return leftRowOfHalf.Reverse().SequenceEqual(rightHalfOfRow);
                        })
                        );
        }


    }
}
