using PDFium.NET.Native;
using Xunit;

namespace PDFium.NET.Test
{
    public class BindingsTests
    {
        private const string DocumentPath = "example.pdf";

        private const string DocumentPath2 = "example-dest.pdf";

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

        [Fact]
        public void ConcurrentAccess()
        {
            // TODO: add checks
        }
    }
}
