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

    [Flags]
    public enum PageRenderingFlags
    {
        FPDF_ANNOT = 0x01,
        // Set if using text rendering optimized for LCD display. This flag will only
        // take effect if anti-aliasing is enabled for text.
        FPDF_LCD_TEXT = 0x02,
        // Don't use the native text output available on some platforms
        FPDF_NO_NATIVETEXT = 0x04,
        // Grayscale output.
        FPDF_GRAYSCALE = 0x08,
        // Obsolete, has no effect, retained for compatibility.
        FPDF_DEBUG_INFO = 0x80,
        // Obsolete, has no effect, retained for compatibility.
        FPDF_NO_CATCH = 0x100,
        // Limit image cache size.
        FPDF_RENDER_LIMITEDIMAGECACHE = 0x200,
        // Always use halftone for image stretching.
        FPDF_RENDER_FORCEHALFTONE = 0x400,
        // Render for printing.
        FPDF_PRINTING = 0x800,
        // Set to disable anti-aliasing on text. This flag will also disable LCD
        // optimization for text rendering.
        FPDF_RENDER_NO_SMOOTHTEXT = 0x1000,
        // Set to disable anti-aliasing on images.
        FPDF_RENDER_NO_SMOOTHIMAGE = 0x2000,
        // Set to disable anti-aliasing on paths.
        FPDF_RENDER_NO_SMOOTHPATH = 0x4000,
        // Set whether to render in a reverse Byte order, this flag is only used when
        // rendering to a bitmap.
        FPDF_REVERSE_BYTE_ORDER = 0x10,
        // Set whether fill paths need to be stroked. This flag is only used when
        // FPDF_COLORSCHEME is passed in, since with a single fill color for paths the
        // boundaries of adjacent fill paths are less visible.
        FPDF_CONVERT_FILL_TO_STROKE = 0x20
    }

    public class FS_RECTF
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    }

    public class FS_SIZEF
    {
        public float width;
        public float height;
    }

    public class FPDF_COLORSCHEME
    {
        int path_fill_color;
        int path_stroke_color;
        int text_fill_color;
        int text_stroke_color;
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
        private static extern DocumentHandle
            FPDF_LoadMemDocument64(DocumentHandle data_buf, long size, string password);

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
        private static extern bool FPDF_DocumentHasValidCrossReferenceTable(DocumentHandle document);

        public static bool DocumentHasValidCrossReferenceTable(DocumentHandle document)
        {
            return FPDF_DocumentHasValidCrossReferenceTable(document);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetTrailerEnds(DocumentHandle document, out byte[] buffer, long length);

        public static int GetTrailerEnds(DocumentHandle document, out byte[] buffer, long length)
        {
            return FPDF_GetTrailerEnds(document, out buffer, length);
        }

        [DllImport(NativeLibrary)]
        private static extern long FPDF_GetDocPermissions(DocumentHandle document);

        public static long GetDocPermissions(DocumentHandle document)
        {
            return FPDF_GetDocPermissions(document);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetSecurityHandlerRevision(DocumentHandle document);

        public static int GetSecurityHandlerRevision(DocumentHandle document)
        {
            return FPDF_GetSecurityHandlerRevision(document);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetPageCount(DocumentHandle document);

        public static int GetPageCount(DocumentHandle document)
        {
            return FPDF_GetPageCount(document);
        }


        [DllImport(NativeLibrary)]
        private static extern PageHandle FPDF_LoadPage(DocumentHandle document, int page_index);

        public static PageHandle LoadPage(DocumentHandle document, int pageIndex)
        {
            return FPDF_LoadPage(document, pageIndex);
        }

        [DllImport(NativeLibrary)]
        private static extern float FPDF_GetPageWidthF(PageHandle page);

        public static float GetPageWidthF(PageHandle page)
        {
            return FPDF_GetPageWidthF(page);
        }

        [DllImport(NativeLibrary)]
        private static extern double FPDF_GetPageWidth(PageHandle page);

        public static double GetPageWidth(PageHandle page)
        {
            return FPDF_GetPageWidth(page);
        }

        [DllImport(NativeLibrary)]
        private static extern float FPDF_GetPageHeightF(PageHandle page);

        public static float GetPageHeightF(PageHandle page)
        {
            return FPDF_GetPageHeightF(page);
        }

        [DllImport(NativeLibrary)]
        private static extern double FPDF_GetPageHeight(PageHandle page);

        public static double GetPageHeight(PageHandle page)
        {
            return FPDF_GetPageHeight(page);
        }

        [DllImport(NativeLibrary)]
        private static extern bool FPDF_GetPageBoundingBox(PageHandle page, out FS_RECTF rect);

        public static bool GetPageBoundingBox(PageHandle page, out FS_RECTF rect)
        {
            return FPDF_GetPageBoundingBox(page, out rect);
        }

        [DllImport(NativeLibrary)]
        private static extern bool FPDF_GetPageSizeByIndexF(DocumentHandle document, int page_index, out FS_SIZEF size);

        public static bool GetPageSizeByIndexF(DocumentHandle document, int page_index, out FS_SIZEF size)
        {
            return FPDF_GetPageSizeByIndexF(document, page_index, out size);
        }

        [DllImport(NativeLibrary)]
        private static extern int FPDF_GetPageSizeByIndex(DocumentHandle document, int page_index, out double width, out double height);

        public static int GetPageSizeByIndex(DocumentHandle document, int page_index, out double width,
            out double height)
        {
            return FPDF_GetPageSizeByIndex(document, page_index, out width, out height);
        }

        [DllImport(NativeLibrary)]
        private static extern void FPDF_CloseDocument(DocumentHandle document);

        public static void CloseDocument(DocumentHandle document)
        {
            FPDF_CloseDocument(document);
        }

        [DllImport(NativeLibrary)]
        private static extern void FPDF_ClosePage(PageHandle page);

        public static void ClosePage(PageHandle page)
        {
            FPDF_ClosePage(page);
        }
    }
}
