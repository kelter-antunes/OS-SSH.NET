using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.OS.SshNet.Tests.Common;
using System;

namespace Renci.OS.SshNet.Tests.Classes
{
    /// <summary>
    /// Provides functionality to perform keyboard interactive authentication.
    /// </summary>
    [TestClass]
    public partial class KeyboardInteractiveAuthenticationMethodTest : TestBase
    {
        [TestMethod]
        [TestCategory("AuthenticationMethod")]
        [Owner("Kenneth_aa")]
        [Description("KeyboardInteractiveAuthenticationMethod: Pass null as username.")]
        [ExpectedException(typeof(ArgumentException))]
        public void Keyboard_Test_Pass_Null()
        {
            new KeyboardInteractiveAuthenticationMethod(null);
        }

        [TestMethod]
        [TestCategory("AuthenticationMethod")]
        [Owner("Kenneth_aa")]
        [Description("KeyboardInteractiveAuthenticationMethod: Pass String.Empty as username.")]
        [ExpectedException(typeof(ArgumentException))]
        public void Keyboard_Test_Pass_Whitespace()
        {
            new KeyboardInteractiveAuthenticationMethod(string.Empty);
        }
    }
}
