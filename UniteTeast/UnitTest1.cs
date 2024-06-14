using System;
using System.Collections.Generic;
using Xunit;
using ContactManager;

namespace UnitTestProject
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            // Ensure contacts are cleared before each test
            Program.ClearContacts();
        }

        [Fact]
        public void AddContact_ShouldAddNewContact()
        {
            // Act
            var result = Program.AddContact("Alice", "Friend");

            // Assert
            Assert.Single(result);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal("Friend", result[0].Cat);
        }

        [Fact]
        public void AddContact_ShouldNotAddDuplicateContact()
        {
            // Arrange
            Program.AddContact("Alice", "Friend");

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => Program.AddContact("Alice", "Friend"));
            Assert.Equal("Contact already exists.", ex.Message);
        }

        [Fact]
        public void RemoveContact_ShouldRemoveContact()
        {
            // Arrange
            Program.AddContact("Alice", "Friend");

            // Act
            var result = Program.RemoveContact("Alice");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void RemoveContact_ShouldThrowExceptionIfContactNotFound()
        {
            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => Program.RemoveContact("Bob"));
            Assert.Equal("Contact not found.", ex.Message);
        }

        [Fact]
        public void ViewAllContacts_ShouldReturnAllContacts()
        {
            // Arrange
            Program.AddContact("Alice", "Friend");
            Program.AddContact("Bob", "Work");

            // Act
            var result = Program.ViewAllContacts();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal("Friend", result[0].Cat);
            Assert.Equal("Bob", result[1].Name);
            Assert.Equal("Work", result[1].Cat);
        }

        [Fact]
        public void SearchContact_ShouldReturnContactDetailsIfFound()
        {
            // Arrange
            Program.AddContact("Alice", "Friend");

            // Act
            var result = Program.SearchContact("Alice");

            // Assert
            Assert.Equal("Name: Alice, Category: Friend", result);
        }

        [Fact]
        public void SearchContact_ShouldReturnNotFoundMessageIfNotFound()
        {
            // Act
            var result = Program.SearchContact("Bob");

            // Assert
            Assert.Equal("Contact not found.", result);
        }

        [Fact]
        public void ClearContacts_ShouldClearAllContacts()
        {
            // Arrange
            Program.AddContact("Alice", "Friend");

            // Act
            Program.ClearContacts();
            var result = Program.ViewAllContacts();

            // Assert
            Assert.Empty(result);
        }
    }
}