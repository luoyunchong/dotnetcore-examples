using System;
using System.Collections.Generic;
using System.Text;
using DataBase.Areas.Plat.Controllers;
using Xunit;

namespace OvOv.Db.Test
{
    public class ValuesControllerTest
    {
        [Fact]
        public void Get()
        {
           string value= new ValuesController().Get();
            Assert.Equal("ok", value);
        }
    }
}
