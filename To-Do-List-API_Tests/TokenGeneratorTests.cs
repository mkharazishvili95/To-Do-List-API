using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Services;

namespace To_Do_List_API_Tests
{
    public class TokenGeneratorTests
    {
        [Test]
        public void GenerateToken_Returns_Valid_Token()
        {
            var appSettings = new AppSettings { Secret = "Do you want to break my secret key?! Kiss my ASS! =P" };
            var options = Options.Create(appSettings);
            var user = new User { UserName = "testUser", Id = 1, Role = "user" };
            var tokenGenerator = new TokenGenerator(options.Value);

            var generatedToken = tokenGenerator.GenerateToken(user);
            Assert.IsNotNull(generatedToken);
        }
    }

}
