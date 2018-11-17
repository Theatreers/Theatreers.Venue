using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using theatreers.venue.Controllers;
using theatreers.shared.Interfaces;
using theatreers.shared.Models;
using theatreers.shared.Services;
using Xunit;

namespace theatreers.venue.tests
{
    public class VenuesControllerTest
    {
        VenuesController _controller;
        IVenueService _service;

        public VenuesControllerTest()
        {
            _service = new VenueServiceFake();
            _controller = new VenuesController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Venue>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(12837);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.Get(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<Venue>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Venue).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Venue()
            {
                Id = 1
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Venue testItem = new Venue()
            {
                Name = "The New Theatre"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Venue()
            {
                Name = "Kenton Theatre"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as Venue;

            // Assert
            Assert.IsType<Venue>(item);
            Assert.Equal(testItem.Name, item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            int notExistingId = 13674;

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            int existingId = 1;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            int existingId = 1;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            var validateCount = _controller.Get().Result as OkObjectResult;
            var updatedItems = Assert.IsType<List<Venue>>(validateCount.Value);
            Assert.Equal(2, updatedItems.Count);
        }

        [Fact]
        public void Update_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {            
            // Arrange
            var proposedUpdate = new Venue()
            {
                Id = 13674,
                Name = "The Prince of Wales Theatre"
            };

            // Act
            var badResponse = _controller.Put(proposedUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse.Result);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var proposedUpdate = new Venue()
            {
                Id = 1,
                Name = "The Hexagon"
            };

            // Act            
            var okResult = _controller.Put(proposedUpdate).Result as OkObjectResult;
            
            // Assert
            var validateResult = _controller.Get(proposedUpdate.Id).Result as OkObjectResult;
            Assert.IsType<Venue>(okResult.Value);
            Assert.Equal(proposedUpdate.Name, (okResult.Value as Venue).Name);
        }
    }
}
