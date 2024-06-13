using AppMaui.Service;
using AppMaui.ViewModels;
using Moq;

public class MainPageViewModelTests
{
    [Fact]
    public void DeviceModel_ShouldReturnCorrectValue()
    {
        // Arrange
        var mockDeviceService = new Mock<IDeviceService>();
        mockDeviceService.Setup(service => service.GetDeviceModel()).Returns("Test Device");

        var viewModel = new MainPageViewModel(mockDeviceService.Object);

        // Act
        var result = viewModel.DeviceModel;

        // Assert
        Assert.Equal("Test Device", result);
    }
}