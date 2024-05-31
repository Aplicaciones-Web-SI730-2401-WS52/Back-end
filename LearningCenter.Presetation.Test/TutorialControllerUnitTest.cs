using _1_API.Controllers;
using _1_API.Response;
using _2_Domain;
using _3_Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject1LearningCenter.Presetation.Test;

public class TutorialControllerUnitTest
{
    [Fact]
    public async Task GetAsync_ResultOk()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockTutorialData = new Mock<ITutorialData>();
        var mockTutorialDomain = new Mock<ITutorialDomain>();

        var fakeList = new List<Tutorial>();
        fakeList.Add(new Tutorial());
        mockTutorialData.Setup(t=>t.GetAllAsync()).ReturnsAsync(fakeList);
        
        var fakeResultList= new List<TutorialResponse>();
        fakeResultList.Add(new TutorialResponse());
        
        mockMapper.Setup(m=>m.Map<List<Tutorial>, List<TutorialResponse>>(It.IsAny<List<Tutorial>>())).Returns(fakeResultList);
        
        var controller = new TutorialController(mockTutorialData.Object, mockTutorialDomain.Object, mockMapper.Object);
        
        //Act
        var result = controller.GetAsync();
        
        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAsync_ResultNotFound()
    {
        //Arrange
        var mockMapper = new Mock<IMapper>();
        var mockTutorialData = new Mock<ITutorialData>();
        var mockTutorialDomain = new Mock<ITutorialDomain>();

        var fakeList = new List<Tutorial>();
        mockTutorialData.Setup(t=>t.GetAllAsync()).ReturnsAsync(fakeList);
        
        var fakeResultList= new List<TutorialResponse>();
        mockMapper.Setup(m=>m.Map<List<Tutorial>, List<TutorialResponse>>(It.IsAny<List<Tutorial>>())).Returns(fakeResultList);
        
        var controller = new TutorialController(mockTutorialData.Object, mockTutorialDomain.Object, mockMapper.Object);
        
        //Act
        var result = controller.GetAsync();
        
        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}