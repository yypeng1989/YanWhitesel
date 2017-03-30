using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SearchNameCodeFirst.Models;

namespace SearchNameCodeFirst.DAL
{
    public class SearchInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<SearchContext>
    {
        protected override void Seed(SearchContext context)
        {
            var People = new List<People>
            {
                new People{FirstName="Warren",LastName="Buffett",Age=85,Interest="Investment",Address="Unknown",ImagePath="Image/matureman1-128.png"},
                new People{FirstName="Bill",LastName="Gates",Age=65,Interest="Programming",Address="Unknown",ImagePath="Image/malecostume-128.png"},
                new People{FirstName="Yan",LastName="Peng",Age=26,Interest="Painting",Address="Unknown", ImagePath="Image/female1-128.png"},
                new People{FirstName="Tim",LastName="Cook",Age=55,Interest="Programming",Address="Unknown", ImagePath="Image/male-128.png"}
            };

            People.ForEach(s => context.People.Add(s));
            context.SaveChanges();
        }
    }
}