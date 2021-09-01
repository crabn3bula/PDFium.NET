using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PDFium.NET.Test
{
    public class Tests
    {
        private const string DocumentPath = "example.pdf";

        [Fact]
        public void ApiTest()
        {
            var ex = Record.Exception(() =>
            {
                using (var document = Document.Load(DocumentPath))
                {
                    // document loaded
                    Assert.NotNull(document);

                    // checks metadata
                    Assert.Equal("This is example pdf title metatag", document.Title);
                    Assert.Equal("This is example pdf author metatag", document.Author);
                    Assert.Equal("This is example pdf subject metatag", document.Subject);
                    Assert.Equal("This is example pdf keywords metatag", document.Keywords);
                    Assert.Equal("This is example pdf creator metatag", document.Creator);
                    Assert.Equal("This is example pdf producer metatag", document.Producer);
                    Assert.Equal("01/04/2009, 21:39:25", document.CreationDate);
                    Assert.Equal("13/04/2021, 23:53:41", document.ModDate);

                    // checks version
                    Assert.Equal(15, document.Version);

                    // pages count
                    Assert.Equal(14, document.Pages.Count);
                }
            });
            Assert.Null(ex);
        }
    }
}
