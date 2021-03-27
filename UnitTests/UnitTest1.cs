using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab3;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class InputTests
    {
        [TestMethod]
        public void CheckCities()
        {
            List<string> test = new List<string>() { "СПб", "Москва", "Хабаровск", "Владивосток" };

            string fileName = "_Test_Example.txt";
            Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);

            Assert.AreEqual(test.SequenceEqual(algTest.cities), true);
        }

        [TestMethod]
        public void CheckCorrectPath_RoadToYourself()
        {
            List<string> test = new List<string>() { "СПб", "СПб"};
            string fileName = "_Test_Example.txt";
            Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);            

            Assert.AreEqual(test.SequenceEqual(algTest.Path("СПб", "СПб")), true);
        }

        [TestMethod]
        public void CheckCorrectPath_RoadToCity_StraightWay()
        {
            List<string> test = new List<string>() { "СПб", "Москва" };
            string fileName = "_Test_Example.txt";
            Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);

            Assert.AreEqual(test.SequenceEqual(algTest.Path("СПб", "Москва")), true);
        }

        [TestMethod]
        public void CheckCorrectPath_NoWay()
        {
            try
            {
                string fileName = "_Test_NoWay.txt";
                Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);

                var testtt = algTest.Path("1", "2");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "There is no way!");                
            }            
        }

        [TestMethod]
        public void CheckCorrectPath_RoadToCity_ManyPoints()
        {
            List<string> test = new List<string>() { "Москва", "СПб", "Владивосток" };

            string fileName = "_Test_Example.txt";
            Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);

            Assert.AreEqual(test.SequenceEqual(algTest.Path("Москва", "Владивосток")), true);
        }
    }

    [TestClass]
    public class AlgoritmhTests
    {
        [TestMethod]
        public void EmptyFile()
        {
            try
            {
                string fileName = "_Test_EmptyFile.txt";
                Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Empty file!");
            }
        }

        [TestMethod]
        public void IncorrectPathFormat_Range()
        {
            try
            {
                string fileName = "_Test_IncorrectPathFormat_Range.txt";
                Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Invalid file filling format! On 1 line; 3 argument (not number)");
            }
        }

        [TestMethod]
        public void CheckCorrectPath_IncorrectNameCity()
        {
            try
            {
                string fileName = "_Test_Example.txt";
                Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);
                algTest.Path("Москва", "Сельдерей");
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "You entered incorrect name of second city!");
            }
        }

        [TestMethod]
        public void NotEnoughArgumentsInFile()
        {
            try
            {
                string fileName = "_Test_NotEnoughArguments.txt";
                Lab3.Floyd_Warshall algTest = new Floyd_Warshall(fileName);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Invalid file filling format! On 1 line; one of the arguments is an empty string!");
            }
        }
    }
}