using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("PDFium.NET.Test")]
namespace PDFium.NET.Native
{
    public enum SandboxPolicies
    {
        FPDF_POLICY_MACHINETIME_ACCESS = 0
    }

    public enum PrintModes
    {
        FPDF_PRINTMODE_EMF = 0,
        FPDF_PRINTMODE_TEXTONLY = 1,
        FPDF_PRINTMODE_POSTSCRIPT2 = 2,
        FPDF_PRINTMODE_POSTSCRIPT3 = 3,
        FPDF_PRINTMODE_POSTSCRIPT2_PASSTHROUGH = 4,
        FPDF_PRINTMODE_POSTSCRIPT3_PASSTHROUGH = 5,
        FPDF_PRINTMODE_EMF_IMAGE_MASKS = 6
    }

    public enum ErrorCodes
    {
        FPDF_ERR_SUCCESS = 0,  // No error.
        FPDF_ERR_UNKNOWN = 1,  // Unknown error.
        FPDF_ERR_FILE = 2,     // File not found or could not be opened.
        FPDF_ERR_FORMAT = 3,   // File not in PDF format or corrupted.
        FPDF_ERR_PASSWORD = 4, // Password required or incorrect password.
        FPDF_ERR_SECURITY = 5, // Unsupported security scheme.
        FPDF_ERR_PAGE = 6,     // Page not found or content error.
        FPDF_ERR_XFALOAD = 7,  // Load XFA error.
        FPDF_ERR_XFALAYOUT = 8 // Layout XFA error.
    }

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
        private static extern void FPDF_SetSandBoxPolicy(SandboxPolicies policy, bool enable);

        public static void SetSandboxPolicy(SandboxPolicies policy, bool enable)
        {
            FPDF_SetSandBoxPolicy(policy, enable);
        }

        // TODO: check api
        public delegate void EnsureTypefaceCharactersAccessible(IntPtr font, IntPtr text, int text_length);

        [DllImport(NativeLibrary)]
        private static extern void FPDF_SetTypefaceAccessibleFunc(EnsureTypefaceCharactersAccessible func);

        [DllImport(NativeLibrary)]
        private static extern void FPDF_SetPrintTextWithGDI(bool use_gdi);

        public static void SetPrintTextWithGDI(bool useGdi)
        {
            FPDF_SetPrintTextWithGDI(useGdi);
        }

        [DllImport(NativeLibrary)]
        private static extern bool FPDF_SetPrintMode(PrintModes mode);

        public static bool SetPrintMode(PrintModes mode)
        {
            return FPDF_SetPrintMode(mode);
        }

        [DllImport(NativeLibrary)]
        private static extern DocumentHandle FPDF_LoadDocument(string file_path, string password);

        public static DocumentHandle LoadDocument(string filePath, string password)
        {
            return FPDF_LoadDocument(filePath, password);
        }

        [DllImport(NativeLibrary)]
        private static extern DocumentHandle FPDF_LoadMemDocument(DocumentHandle data_buf, int size, string password);

        public static DocumentHandle LoadMemDocument(DocumentHandle dataBuffer, int size, string password)
        {
            return FPDF_LoadMemDocument(dataBuffer, size, password);
        }

        [DllImport(NativeLibrary)]
        private static extern DocumentHandle FPDF_LoadMemDocument64(DocumentHandle data_buf, long size, string password);

        public static DocumentHandle LoadMemDocument64(DocumentHandle dataBuffer, long size, string password)
        {
            return FPDF_LoadMemDocument64(dataBuffer, size, password);
        }

        // FPDF_LoadCustomDocument(FPDF_FILEACCESS* pFileAccess, FPDF_BYTESTRING password);

        [DllImport(NativeLibrary)]
        private static extern bool FPDF_GetFileVersion(DocumentHandle doc, out int fileVersion);

        public static bool GetFileVersion(DocumentHandle document, out int fileVersion)
        {
            return FPDF_GetFileVersion(document, out fileVersion);
        }

        [DllImport(NativeLibrary)]
        private static extern ErrorCodes FPDF_GetLastError();

        public static ErrorCodes GetLastError()
        {
            return FPDF_GetLastError();
        }

        [DllImport(NativeLibrary)]
        private static extern void FPDF_CloseDocument(DocumentHandle document);

        public static void CloseDocument(DocumentHandle document)
        {
            FPDF_CloseDocument(document);
        }

        [DllImport(NativeLibrary)]
        private static extern PageHandle FPDF_LoadPage(DocumentHandle document, int page_index);

        public static PageHandle LoadPage(DocumentHandle document, int pageIndex)
        {
            return FPDF_LoadPage(document, pageIndex);
        }

        [DllImport(NativeLibrary)]
        private static extern void FPDF_ClosePage(PageHandle page);

        public static void ClosePage(PageHandle page)
        {
            FPDF_ClosePage(page);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetPageCount(DocumentHandle document);

        public static int GetPageCount(DocumentHandle document)
        {
            return FPDF_GetPageCount(document);
        }
    }
}
