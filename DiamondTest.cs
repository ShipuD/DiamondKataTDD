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
        private readonly string _lowerCaseAlphabets = "abcdefghijklmnopqrstuvwxyz";
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
            foreach (char ch1 in _lowerCaseAlphabets)
            {
                List<string> lst = _diamond.Generate(ch1);

                Assert.IsTrue(lst.All(r =>
                {
                    return r.Trim().All(ch => char.IsUpper(ch) || char.IsWhiteSpace(ch));
                }));
            }
            
        }
        [TestMethod]
        public void LastRowShouldAlwaysBeA()
        {
            foreach (char ch in _lowerCaseAlphabets)
            {
                List<string> lst = _diamond.Generate(ch);
                //Console.WriteLine(lst[0]);
                Assert.IsTrue(lst.Count > 0 && lst[lst.Count - 1].Trim() == "A");
            }
        }

        [TestMethod]
        public void LastRowShouldAlwaysBeSameAsFirstWithSpaces()
        {
            foreach (char ch in _lowerCaseAlphabets)
            {
                List<string> lst = _diamond.Generate(ch);
                Assert.IsTrue(lst.Count > 0 && lst[lst.Count - 1] == lst[0]);
            }
        }

        [TestMethod]
        public void AShouldBeIntheMiddleOfFirstRow()
        {   
            var topRow = _diamond.Generate('I').ToArray()[0];
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
            var diamondInArray = _diamond.Generate('G').ToArray();
            var half = diamondInArray.Length / 2;
            var topHalf = diamondInArray[..half];
            var bottomHalf = diamondInArray[(half + 1)..];

            Assert.IsTrue(topHalf.Reverse().SequenceEqual(bottomHalf));
        }

        [TestMethod]
        public void ShouldBeVerticallySymmetric()
        {
            Assert.IsTrue(_diamond.Generate('F').ToArray()
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
