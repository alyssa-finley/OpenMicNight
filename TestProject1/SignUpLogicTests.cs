using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMicNight.Data;
using OpenMicNight.Domain;
using OpenMicNight.Logic;
using System.Collections.Generic;
using Moq;

namespace OpenMicNight.Test
{
    [TestClass]
    public class SignUpLogicTests
    {
        private readonly Mock <IPerformerRepository> _performerRepository = new Mock <IPerformerRepository> ();
        private readonly SignUpLogic _target; 
        public SignUpLogicTests()
        {
            _target = new SignUpLogic(_performerRepository.Object);
        }
      
        [TestMethod]

        public void MaxPerformances_LessThan13_True()
        {
            ///Arrange

            ///Act
             for (int i = 0; i < 10; i++)
            {
                _target.AddPerformanceToSignUpList(new Performer());
            }
            var result = _target.MaxPerformances();

            ///Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MaxPerformances_LessThan13_False()
        {
            ///Arrange

            ///Act
            for (int i = 0; i < 14; i++)
            {
                _target.AddPerformanceToSignUpList(new Performer());
            }
            var result = _target.MaxPerformances();

            ///Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetPerformersById_CallsRepository()
        {
            ///Arrange 
            var TestId = 1;
            ///Act
            _performerRepository.Setup(x => x.GetPerformersById(It.Is<int>(y => y == TestId))).Returns(new Performer()
            {
                PerformerName = "Two For One"
            }); ;
            var result = _target.GetPerformersById(1);
            ///Assert
            Assert.AreEqual("Two For One",result.PerformerName);
        }
    }
}

