using inventoryAppDomain.Services;
using Moq;
using NUnit.Framework;

namespace InventoryAppWebUi.Test.Tests
{
    [TestFixture]
    public class ProfileTests
    {
        private Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();
        private Mock<IProfileService> _mockProfileService = new Mock<IProfileService>();
        private Mock<INotificationService> _mockNotificationService = new Mock<INotificationService>();

        [Test]
        public void RemoveUser()
        {
            
        }
    }
}