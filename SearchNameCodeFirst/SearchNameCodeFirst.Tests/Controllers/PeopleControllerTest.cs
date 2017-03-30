using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchNameCodeFirst.Controllers;
using SearchNameCodeFirst.DAL;
using SearchNameCodeFirst.Models;
using SearchNameCodeFirst.Tests.Controllers;


namespace SearchNameCodeFirst.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTest
    {
        private SearchContext db = new SearchContext();

        [TestMethod]
        public void Index()
        {
            // Arrange
            PeopleController controller = new PeopleController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
       
            List<People> PeopleList = (List<People>)result.ViewData.Model;
            // Assert
            Assert.AreNotEqual(0, PeopleList.Count());
        }


        [TestMethod]
        public void Create()
        {
            // Arrange
            PeopleController controller = new PeopleController();
            // Act
            ViewResult result = controller.Create() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void CreateRedirect()
        {
            // Arrange
            PeopleController controller = new PeopleController();
            // Act
            People NewPeople = new People();
            NewPeople.FirstName = "TestUser";
            NewPeople.LastName = "Jonson";
            NewPeople.Age = 25;
            NewPeople.Interest = "Golfing";
            NewPeople.Address = "Unknown";
            RedirectToRouteResult result = controller.Create(NewPeople, null) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }


        [TestMethod()]
        public void EditSaved()
        {
            // Arrange
            PeopleController controller = new PeopleController();

            People PeopleBefore = new People();
            PeopleBefore = db.People.Where(w => w.ID == 5).First();
            var PeopleBeforeFirstName = PeopleBefore.FirstName;

            PeopleBefore.FirstName = "Brian";
            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(PeopleBefore,null);
            People PeopleAfter = db.People.Where(w => w.ID == 5).First();
            // Assert
            Assert.AreNotEqual(PeopleBeforeFirstName, PeopleAfter.FirstName);
        }


        [TestMethod()]
        public void Details()
        {
            // Arrange
            PeopleController controller = new PeopleController();
            // Act
            ViewResult result = (ViewResult)controller.Details(1);
            People People = (People)result.ViewData.Model;
            // Assert
            Assert.AreEqual("Warren", People.FirstName);
        }

        [TestMethod()]
        public void SearchTest()
        {
            PeopleController controller = new PeopleController();

            var result = controller.Search("Yan") as PartialViewResult;

            Assert.AreEqual("PeoplePartial", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<People>));
            Assert.AreNotEqual(0, ((List<People>)result.ViewData.Model).Count());
        }
    }
}
