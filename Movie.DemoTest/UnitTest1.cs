using Movie.ServiceHost.API.Business;
using System;
using Moq;
using Xunit;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using System.Collections.Generic;

namespace Movie.DemoTest
{
    public class UnitTest1
    {      
        [Theory, AutoMoqData]
        public void Test1([Frozen]Mock<IFilmBusiness> assembler,IEnumerable<Movie.ServiceHost.API.Models.Film> film)
        {
          //  assembler.Setup(c => c.GetAllAsync()).Returns(film);  
        }
        //Method parameter Automoq attribute
        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute()
                : base(new Fixture().Customize(new AutoMoqCustomization()))
            {
            }
        }
    }
}
