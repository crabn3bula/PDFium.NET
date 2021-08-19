using PDFium.NET.Native;
using Xunit;

namespace PDFium.NET.Test
{
    public class BindingsTests
    {
        private const string DocumentPath = "example.pdf";

        [Fact]
        public void InitLibrary()
        {
            var ex = Record.Exception(() => Bindings.InitLibrary());
            Assert.Null(ex);
        }

        [Fact]
        public void LoadDocument()
        {
            Bindings.InitLibrary();
            var document = Bindings.LoadDocument(DocumentPath, "");
            Assert.NotNull(document);
            Assert.False(document.IsInvalid);
        }
    }
}
