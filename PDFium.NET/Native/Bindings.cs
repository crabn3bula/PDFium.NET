using System.Runtime.InteropServices;

namespace PDFium.NET.Native
{
    static class Bindings
    {
        private const string NativeLibrary = "pdfium.dll";

        [DllImport(NativeLibrary)]
        private static extern void FPDF_InitLibrary();

        public static void InitLibrary()
        {
            FPDF_InitLibrary();
        }

        [DllImport(NativeLibrary)]
        private static extern DocumentHandle FPDF_LoadDocument(string file_path, string password);

        public static DocumentHandle LoadDocument(string filePath, string password)
        {
            return FPDF_LoadDocument(filePath, password);
        }

        [DllImport(NativeLibrary)]
        private static extern void FPDF_CloseDocument(DocumentHandle document);

        public static void CloseDocument(DocumentHandle document)
        {
            FPDF_CloseDocument(document);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetPageCount(DocumentHandle document);

        public static int GetPageCount(DocumentHandle document)
        {
            return FPDF_GetPageCount(document);
        }

        [DllImport(NativeLibrary)]
        private static extern uint FPDF_GetLastError();

        public static uint GetLastError()
        {
            return FPDF_GetLastError();
        }
    }
}
