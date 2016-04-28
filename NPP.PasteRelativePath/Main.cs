using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace NPP.PasteRelativePath
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "NPP.PasteRelativePath";
        static string iniFilePath = null;
        //static bool someSetting = false;
        static frmMyDlg frmMyDlg = null;
        static int idMyDlg = -1;
        static Bitmap tbBmp = Properties.Resources.star;
        static Icon tbIcon = null;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            //StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            //Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            //iniFilePath = sbIniFilePath.ToString();
            //if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            //iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            //someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);

            PluginBase.SetCommand(0, "Paste Relative", myMenuFunction, new ShortcutKey(true, false, true, Keys.V));
        }
        internal static void SetToolBarIcon()
        {
            //toolbarIcons tbIcons = new toolbarIcons();
            //tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            //IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            //Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            //Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
            //Marshal.FreeHGlobal(pTbIcons);
        }
        internal static void PluginCleanUp()
        {
            //Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }
        #endregion

        #region " Menu functions "
        internal static string RelativePath(string absPath, string relTo)
        {
            var absDirs = absPath.Split('\\');
            var relDirs = relTo.Split('\\');
            // Get the shortest of the two paths 
            var len = absDirs.Length < relDirs.Length ? absDirs.Length : relDirs.Length;
            // Use to determine where in the loop we exited 
            var lastCommonRoot = -1; int index;
            // Find common root 
            for (index = 0; index < len; index++)
            {
                if (absDirs[index] == relDirs[index])
                    lastCommonRoot = index;
                else break;
            }
            // If we didn't find a common prefix then throw 
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }
            // Build up the relative path 
            var relativePath = new StringBuilder();
            // Add on the .. 
            for (index = lastCommonRoot + 1; index < absDirs.Length; index++)
            {
                if (absDirs[index].Length > 0) relativePath.Append("..\\");
            }
            // Add on the folders 
            for (index = lastCommonRoot + 1; index < relDirs.Length - 1; index++)
            {
                relativePath.Append(relDirs[index] + "\\");
            }
            relativePath.Append(relDirs[relDirs.Length - 1]);
            return relativePath.ToString();
        }

        internal static void myMenuFunction()
        {
            var clipboardText = Clipboard.GetText();
            var filePath = new StringBuilder(Win32.MAX_PATH);

            try
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDIRECTORY, Win32.MAX_PATH, filePath);

                var relativePath = RelativePath(filePath.ToString(), Clipboard.GetText());

                Win32.SendMessage(PluginBase.nppData._scintillaMainHandle, SciMsg.SCI_ADDTEXT, relativePath.Length, relativePath);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ooops!\r\n\r\n{clipboardText}\r\n{filePath}\r\n\r\n{e}");
            }
        }
        #endregion
    }
}