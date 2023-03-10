using NUnit.Framework;
using Operations.ValidationServices;
using Operations.ValidationServices.MaxWinAmountValidationServices;

namespace UnitTests.Application.Operations.ValidationServices;

[TestFixture]
public class WinAmountValidationServiceTests
{
    private WinAmountValidationService _validationService;

    [SetUp]
    public void SetUp()
    {
        _validationService = new WinAmountValidationService();
    }

    [Test]
    public void Validate_ShouldReturnErrorResponse_WhenWinAmountExceedsMaxWinAmount()
    {
        // Arrange
        decimal winAmount = ValidationConstants.MaxWinAmount + 1;

        // Act
        var result = _validationService.Validate(winAmount);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(ValidationConstants.MaxWinAmountCode, result!.Code);
        Assert.AreEqual(ValidationConstants.MaxWinAmountMessage, result.Message);
    }

    [Test]
    public void Validate_ShouldNotReturnNull_WhenWinAmountDoesNotExceedMaxWinAmount()
    {
        // Arrange
        decimal winAmount = ValidationConstants.MaxWinAmount;

        // Act
        var result = _validationService.Validate(winAmount);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Validate_ShouldNotReturnNull_WhenWinAmountIsLessThanMaxWinAmount()
    {
        // Arrange
        decimal winAmount = ValidationConstants.MaxWinAmount - 1;

        // Act
        var result = _validationService.Validate(winAmount);

        // Assert
        Assert.IsNull(result);
    }
}