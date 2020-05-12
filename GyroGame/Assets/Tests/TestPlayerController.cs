using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestPlayerController
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestPlayerControllerSimplePasses()
        {
            //Can only test unity specific functions
            //Cannot interact with external Classes
            //aka cannot test MonoBehaviours
            //aka cannot test use cases
            Assert.IsTrue(true);            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestPlayerControllerWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            Assert.IsTrue(true);
            yield return null;
        }
    }
}
