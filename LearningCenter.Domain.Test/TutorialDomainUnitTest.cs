using _3_Data;
using Moq;
using NSubstitute;

namespace _2_Domain.Test;

public class TutorialDomainUnitTest
{
    [Fact]
    public async Task SaveAsync_ValidInput_ReturnValidId()
    {
        //Arrange
        var input = new Tutorial()
        {
            Description = "Description",
            Name = "Name",
            Sections = new List<Section>()
            {
                new Section() { Title = "Section 1" },
                new Section() { Title = "Section 2" },
            }
        };

        var mock = new Mock<ITutorialData>();

        mock.Setup(data => data.GetByNameAsync(input.Name)).ReturnsAsync((Tutorial)null);
        mock.Setup(data => data.GetAllAsync()).ReturnsAsync(new List<Tutorial>());
        mock.Setup(data => data.SaveAsync(input)).ReturnsAsync(1);

        TutorialDomain tutorialDomain = new TutorialDomain(mock.Object);

        //ACt
        var result = await tutorialDomain.SaveAsync(input);

        //Assert
        Assert.Equal(1, result);

    }
    
    [Fact]
    public void DeletAsync_ExistingId_ReturnsTrue()
    {
        //Arrange
        var id = 10;
        var tutorialDataMock = Substitute.For<ITutorialData>();
        
        tutorialDataMock.GetById(id).Returns(new Tutorial());
        tutorialDataMock.Delete(id).Returns(true);
        
        TutorialDomain tutorialDomain = new TutorialDomain(tutorialDataMock);

        //Act
        var result=  tutorialDomain.Delete(id);

        //Assert
        //Assert.Equal(true, result);
        Assert.True(result);
    }
    
    [Fact]
    public void DeletAsync_NotExistingId_ReturnsFalse()
    {
        //Arrange
        var id = 10;
        var tutorialDataMock = Substitute.For<ITutorialData>();
        
        tutorialDataMock.GetById(id).Returns((Tutorial)null);
        
        TutorialDomain tutorialDomain = new TutorialDomain(tutorialDataMock);
        
        //Act and Assert
        Assert.Throws<Exception>( () =>  tutorialDomain.Delete(id));
        
    }
}