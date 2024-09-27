using Microsoft.EntityFrameworkCore;
using Paper_Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Models;

namespace Paper_Project_Tests
{
    public class CustomerControllerTests
    {
        private readonly MyDbContext _context;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            // Use an In-Memory database for testing
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Create a new instance of the in-memory context
            _context = new MyDbContext(options);

            // Seed data into the in-memory database
            SeedDatabase();

            // Create an instance of the CustomerController
            _controller = new CustomerController(_context);
        }

        // Seed the database with sample data
        private void SeedDatabase()
        {
            _context.Customers.AddRange(new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Address = "Skovbakken13", Phone = "50123070", Email = "john@example.com" },
                new Customer { Id = 2, Name = "Jane Smith", Address = "Skovbakken14", Phone = "50123076", Email = "jane@example.com" }
            });

            _context.SaveChanges();
        }

        // Test for GET: api/Customer (Get all customers)
        [Fact]
        public async Task GetCustomers_ReturnsAllCustomers()
        {
            // Act
            var result = await _controller.GetCustomers();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Customer>>>(result);
            var customers = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Equal(2, customers.Count()); // We expect 2 customers (as per the SeedDatabase method)
        }

        // Test for GET: api/Customer/5 (Get customer by ID)
        [Fact]
        public async Task GetCustomer_ReturnsCustomerById()
        {
            // Act
            var result = await _controller.GetCustomer(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<Customer>>(result);
            var customer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(1, customer.Id); // We expect customer with ID 1
        }

        // Test for GET: api/Customer/999 (Non-existent customer)
        [Fact]
        public async Task GetCustomer_ReturnsNotFound_ForNonExistentCustomer()
        {
            // Act
            var result = await _controller.GetCustomer(999); // non-existent ID

            // Assert
            Assert.IsType<NotFoundResult>(result.Result); // Expect NotFound
        }

        // Test for POST: api/Customer (Add new customer)
        [Fact]
        public async Task PostCustomer_AddsNewCustomer()
        {
            // Arrange
            var newCustomer = new Customer { Name = "New Customer", Email = "new@example.com" };

            // Act
            var result = await _controller.PostCustomer(newCustomer);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdCustomer = Assert.IsType<Customer>(createdAtActionResult.Value);
            Assert.Equal("New Customer", createdCustomer.Name);
            Assert.Equal(3, _context.Customers.Count()); // Expect 3 customers now (2 from SeedDatabase + 1 new)
        }

        // Test for PUT: api/Customer/1 (Update existing customer)
        [Fact]
        public async Task PutCustomer_UpdatesExistingCustomer()
        {
            // Arrange
            var updatedCustomer = new Customer { Id = 1, Name = "Updated Name", Email = "updated@example.com" };

            // Act
            var result = await _controller.PutCustomer(1, updatedCustomer);

            // Assert
            Assert.IsType<NoContentResult>(result); // Expect NoContent on success
            var customer = await _context.Customers.FindAsync(1);
            Assert.Equal("Updated Name", customer.Name);
        }

        // Test for PUT: api/Customer/2 (Update with mismatched ID)
        [Fact]
        public async Task PutCustomer_ReturnsBadRequest_ForMismatchedId()
        {
            // Arrange
            var updatedCustomer = new Customer { Id = 1, Name = "Mismatched Name", Email = "mismatched@example.com" };

            // Act
            var result = await _controller.PutCustomer(2, updatedCustomer); // IDs do not match

            // Assert
            Assert.IsType<BadRequestResult>(result); // Expect BadRequest
        }

        // Test for DELETE: api/Customer/1 (Delete existing customer)
        [Fact]
        public async Task DeleteCustomer_RemovesCustomer()
        {
            // Act
            var result = await _controller.DeleteCustomer(1);

            // Assert
            Assert.IsType<NoContentResult>(result); // Expect NoContent on success
            Assert.Equal(1, _context.Customers.Count()); // We should have 1 customer remaining
        }

        // Test for DELETE: api/Customer/999 (Non-existent customer)
        [Fact]
        public async Task DeleteCustomer_ReturnsNotFound_ForNonExistentCustomer()
        {
            // Act
            var result = await _controller.DeleteCustomer(999); // non-existent ID

            // Assert
            Assert.IsType<NotFoundResult>(result); // Expect NotFound
        }
    }
}
