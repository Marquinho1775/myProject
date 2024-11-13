using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;



namespace UIAutomationTests
{
    public class Selenium
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-web-security");
            options.AddArgument("--allow-insecure-localhost");
            _driver = new ChromeDriver(options);
        }

        [Test]
        public void CreateCountry_Test()
        {
            // Arrange
            var baseUrl = "http://localhost:8080/";

            // Act
            // 1. Navegar a la página principal (Lista de países)
            _driver.Navigate().GoToUrl(baseUrl);
            _driver.Manage().Window.Maximize();

            // 2. Hacer clic en el botón "Agregar país" para ir al formulario
            var addCountryButton = _driver.FindElement(By.XPath("//button[contains(text(), 'Agregar pais')]"));
            addCountryButton.Click();

            // 3. Completar el formulario
            var countryNameField = _driver.FindElement(By.Id("name"));
            countryNameField.SendKeys("Brasil");

            var continentDropdown = _driver.FindElement(By.Id("continente"));
            continentDropdown.SendKeys("America");

            var languageField = _driver.FindElement(By.Id("idioma"));
            languageField.SendKeys("Portugués");

            // 4. Hacer clic en el botón "Guardar"
            var saveButton = _driver.FindElement(By.XPath("//button[contains(text(), 'Guardar')]"));
            saveButton.Click();

            // Assert
            // 5. Espera hasta que la redirección a la lista de países se complete
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);


            // Verificar que el título de la página sea "Lista de países"
            var title = _driver.FindElement(By.TagName("h1")).Text;
            Assert.AreEqual("Lista de países", title, "La redirección al listado de países no se produjo.");

            // Verificar que el nuevo país aparece en la lista
            var newCountry = _driver.FindElement(By.XPath("//td[contains(text(), 'Brasil')]"));
            Assert.IsNotNull(newCountry, "El nuevo país no fue creado correctamente o no aparece en la lista.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }
    }
}
