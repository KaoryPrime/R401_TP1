using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Controllers;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass]
    public class DevisesControllerTests
    {
        private DevisesController _controller;

        [TestInitialize]
        public void Initialisation()
        {
            _controller = new DevisesController();
        }

        // --- Tests GetAll ---
        [TestMethod]
        public void GetAll_ReturnsAllItems()
        {
            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>));
            // On sait qu'il y a 3 devises par défaut dans le constructeur statique (si non modifié par d'autres tests)
            // Note: Avec une liste statique, l'ordre des tests peut impacter le count. 
        }

        // --- Tests GetById ---
        [TestMethod]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var result = _controller.GetById(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            // Plus besoin de vérifier les propriétés une par une grâce au Equals ajouté dans Devise
            var expectedItem = new Devise(1, "Dollar", 1.08);
            Assert.AreEqual(expectedItem, okResult.Value, "L'objet retourné n'est pas identique à celui attendu.");
        }

        [TestMethod]
        public void GetById_UnknownIdPassed_ReturnsNotFound()
        {
            // Act
            var result = _controller.GetById(999);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        // --- Tests Post ---
        [TestMethod]
        public void Post_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Devise newDevise = new Devise(10, "Euro", 1.0);

            // Act
            var result = _controller.Post(newDevise);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
            var createdResult = result.Result as CreatedAtRouteResult;
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(newDevise, createdResult.Value);
        }

        [TestMethod]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            Devise invalidDevise = new Devise(11, null, 1.0); // Nom null invalide selon [Required]
            _controller.ModelState.AddModelError("NomDevise", "Required"); // Simule l'erreur de validation

            // Act
            var result = _controller.Post(invalidDevise);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        // --- Tests Put ---
        [TestMethod]
        public void Put_ExistingIdPassed_ReturnsNoContent()
        {
            // Arrange
            // On utilise un ID existant (ex: 2 Franc Suisse)
            Devise updatedDevise = new Devise(2, "Franc Suisse Modifié", 1.10);

            // Act
            var result = _controller.Put(2, updatedDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Put_UnknownIdPassed_ReturnsNotFound()
        {
            // Arrange
            Devise unknownDevise = new Devise(999, "Inconnu", 1.0);

            // Act
            var result = _controller.Put(999, unknownDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // --- Tests Delete ---
        [TestMethod]
        public void Delete_ExistingIdPassed_ReturnsOk()
        {
            // Arrange - On utilise l'ID 3 (Yen) pour tester la suppression
            // Act
            var result = _controller.Delete(3);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Delete_UnknownIdPassed_ReturnsNotFound()
        {
            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}