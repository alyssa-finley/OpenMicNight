using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMicNight.Domain;
using OpenMicNight.Logic;
using System.Collections.Generic;

namespace OpenMicNight.Test
{
    [TestClass]
    public class MaxSignUpsTest
    {
        // Custom implementation of ISignUpLogic interface for testing purposes
        private class TestSignUpLogic 
        {
            private int _signUpCounter = 0;
            public bool MaxPerformances()
            {
                _signUpCounter++;
                return _signUpCounter <= 13;
            }

        [TestMethod]
        public void Maximum_SignUps_Reached()
        {
            // Arrange
            var signUpLogic = new TestSignUpLogic();

            // Act
            for (int i = 0; i < 13; i++)
            {
                signUpLogic.AddPerformer(new Performer()); 
            }

            // Assert
            Assert.IsFalse(signUpLogic.MaxPerformances()); 
        }
    }
}
//TO DO:
//unit testing: max number of sign ups, preventing duplicate performer entries, preventing duplicate song entries
//seeding music repository 
//song repository
//updating performers in db vs just in console app 