using System;
using System.Runtime.InteropServices;

namespace NoaDebugger
{
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    class FileDialog : IDisposable
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public string filter = null;
        public string customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public string file = null;
        public int maxFile = 0;
        public string fileTitle = null;
        public int maxFileTitle = 0;
        public string initialDir = null;
        public string title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public string defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public string templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;

        public static readonly int OFN_ALLOWMULTISELECT = 0x00000200;
        public static readonly int OFN_CREATEPROMPT = 0x00000200;
        public static readonly int OFN_DONTADDTORECENT = 0x02000000;
        public static readonly int OFN_ENABLEHOOK = 0x00000020;
        public static readonly int OFN_ENABLEINCLUDENOTIFY = 0x00400000;
        public static readonly int OFN_ENABLESIZING = 0x00800000;
        public static readonly int OFN_ENABLETEMPLATE = 0x00000040;
        public static readonly int OFN_ENABLETEMPLATEHANDLE = 0x00000080;
        public static readonly int OFN_EXPLORER = 0x00080000;
        public static readonly int OFN_EXTENSIONDIFFERENT = 0x00000400;
        public static readonly int OFN_FILEMUSTEXIST = 0x00001000;
        public static readonly int OFN_FORCESHOWHIDDEN = 0x10000000;
        public static readonly int OFN_HIDEREADONLY = 0x00000004;
        public static readonly int OFN_LONGNAMES = 0x00200000;
        public static readonly int OFN_NOCHANGEDIR = 0x00000008;
        public static readonly int OFN_NODEREFERENCELINKS = 0x00100000;
        public static readonly int OFN_NOLONGNAMES = 0x00040000;
        public static readonly int OFN_NONETWORKBUTTON = 0x00020000;
        public static readonly int OFN_NOREADONLYRETURN = 0x00008000;
        public static readonly int OFN_NOTESTFILECREATE = 0x00010000;
        public static readonly int OFN_NOVALIDATE = 0x00000100;
        public static readonly int OFN_OVERWRITEPROMPT = 0x00000002;
        public static readonly int OFN_PATHMUSTEXIST = 0x00000800;
        public static readonly int OFN_READONLY = 0x00000001;
        public static readonly int OFN_SHAREAWARE = 0x00004000;
        public static readonly int OFN_SHOWHELP = 0x00000010;

        public FileDialog()
        {
            structSize = Marshal.SizeOf(this);
            flags = OFN_EXPLORER | OFN_OVERWRITEPROMPT;
        }

        public void Dispose()
        {
            filter = null;
            customFilter = null;
            file = null;
            fileTitle = null;
            initialDir = null;
            title = null;
            defExt = null;
            templateName = null;
        }

        public void SetFile(string fileName)
        {
            var buffer = new char[4096];
            for (var i = 0; i < fileName.Length; ++i)
            {
                buffer[i] = fileName[i];
            }
            buffer[fileName.Length] = '\0';
            file = new string(buffer);
            maxFile = buffer.Length;
        }

        public void SetFilter(string description, string extension)
        {
            filter = $"{description}\0*{extension}\0\0";
        }

        public char[] CreateBufferedString(string text, int capacity)
        {
            var buffer = new char[capacity];
            for (var i = 0; i < text.Length; ++i)
            {
                buffer[i] = text[i];
            }
            buffer[text.Length] = '\0';
            return buffer;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    sealed class WindowsFileDialog : FileDialog
    {
        public bool ShowDialog()
        {
            return Commdlg.GetSaveFileName(this);
        }

        public string FilePath()
        {
            return file;
        }
    }

    sealed class Commdlg
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] FileDialog ofn);
    }
#endif
}
