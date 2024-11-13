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
            // 1. Navegar a la p�gina principal (Lista de pa�ses)
            _driver.Navigate().GoToUrl(baseUrl);
            _driver.Manage().Window.Maximize();

            // 2. Hacer clic en el bot�n "Agregar pa�s" para ir al formulario
            var addCountryButton = _driver.FindElement(By.XPath("//button[contains(text(), 'Agregar pais')]"));
            addCountryButton.Click();

            // 3. Completar el formulario
            var countryNameField = _driver.FindElement(By.Id("name"));
            countryNameField.SendKeys("Brasil");

            var continentDropdown = _driver.FindElement(By.Id("continente"));
            continentDropdown.SendKeys("America");

            var languageField = _driver.FindElement(By.Id("idioma"));
            languageField.SendKeys("Portugu�s");

            // 4. Hacer clic en el bot�n "Guardar"
            var saveButton = _driver.FindElement(By.XPath("//button[contains(text(), 'Guardar')]"));
            saveButton.Click();

            // Assert
            // 5. Espera hasta que la redirecci�n a la lista de pa�ses se complete
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);


            // Verificar que el t�tulo de la p�gina sea "Lista de pa�ses"
            var title = _driver.FindElement(By.TagName("h1")).Text;
            Assert.AreEqual("Lista de pa�ses", title, "La redirecci�n al listado de pa�ses no se produjo.");

            // Verificar que el nuevo pa�s aparece en la lista
            var newCountry = _driver.FindElement(By.XPath("//td[contains(text(), 'Brasil')]"));
            Assert.IsNotNull(newCountry, "El nuevo pa�s no fue creado correctamente o no aparece en la lista.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }
    }
}
