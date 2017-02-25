using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;
using System.Security.Cryptography;

namespace NppHashMaker
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "NppHash";
        static string iniFilePath = null;
        static bool someSetting = false;
        //static HashResultDlg frmMyDlg = null;
        //static int idMyDlg = -1;
        //static Bitmap tbBmp = Properties.Resources.star;
        //static Bitmap tbBmp_tbTab = Properties.Resources.star_bmp;
        //static Icon tbIcon = null;
        static int maxDisplayDataStringLen = 85;
        static HashResultDlg hashResultDlg = null;
        public enum HashMethod { 
            hm_MD5,
            hm_SHA1,
            hm_SHA256,
            hm_SHA384,
            hm_SHA512
        };

        #endregion
        internal static void writeLog(string str)
        {/*
            using (StreamWriter outfile = File.AppendText("c:\\nppHash.log"))
            {
                outfile.Write(str);
                outfile.Flush();
                outfile.Close();
            }
        */}

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);

            PluginBase.SetCommand(0, "MD5", md5, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(1, "SHA1", sha1);
            PluginBase.SetCommand(2, "SHA256", sha256);
            PluginBase.SetCommand(3, "SHA384", sha384);
            PluginBase.SetCommand(4, "SHA512", sha512);

            //idMyDlg = 1;
        }
        internal static void SetToolBarIcon()
        {
            /*
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
            */
        }
        internal static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }
        #endregion

        #region " Menu functions "
        
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append((char)inputArray[i]);
            }
            return output.ToString();
        }

        public static string GetTipStringFromByteArray(byte[] inputArray, int maxLen)
        {
            string s = byteArrayToString(inputArray);
            string tip = "";
            char[] sep = new char[2];
            sep[0] = '\n';
            sep[1] = '\r';
            string[] ss = s.Split(sep);

            if (ss.Length >= 2)
                tip = " ...";

            if (ss[0].Length <= maxLen)
            {
                return (ss[0] + tip);
            }
            else
            {
                tip = " ...";
            }
            return (ss[0].Substring(0, maxLen) + tip);
        }

        protected static byte[] GetSelectedText()
        {
            IntPtr hCurrScintilla = PluginBase.GetCurrentScintilla();
            int start = (int)Win32.SendMessage(hCurrScintilla, SciMsg.SCI_GETSELECTIONSTART, 0, 0);
            int end = (int)Win32.SendMessage(hCurrScintilla, SciMsg.SCI_GETSELECTIONEND, 0, 0);

            if (end < start)
            {
                int tmp = start;
                start = end;
                end = tmp;
            }

            int textLen = end - start;
            if (textLen == 0) return null;

            // Walkaround the crash problem if length is 86016
            try
            {
                IntPtr ptrText = Marshal.AllocHGlobal(textLen == 86016?86017:textLen);
                Win32.SendMessage(hCurrScintilla, SciMsg.SCI_GETSELTEXT, 0, ptrText);
                byte[] b = new byte[textLen];
                Marshal.Copy(ptrText, b, 0, textLen);
                Marshal.FreeHGlobal(ptrText);
                
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show("textLen : " + textLen + "\n" + ex.ToString());
                return null;
            }
        }

        internal static void hash(HashMethod hm)
        {
            byte [] b = GetSelectedText();
            string dlgTitle = "";
            if (b == null) // Get whole content
            {
                MessageBox.Show("No selected data to hash.");
                return;
            }
            
            byte[] hash = null;
            if (hm == HashMethod.hm_MD5)
            {
                hash = GetMD5(b);
                dlgTitle = "MD5";
            }
            else if (hm == HashMethod.hm_SHA1)
            {
                hash = GetSha1(b);
                dlgTitle = "SHA1";
            }
            else if (hm == HashMethod.hm_SHA256)
            {
                hash = GetSha256(b);
                dlgTitle = "SHA256";
            }
            else if (hm == HashMethod.hm_SHA384)
            {
                hash = GetSha384(b);
                dlgTitle = "SHA384";
            }
            else if (hm == HashMethod.hm_SHA512)
            {
                hash = GetSha512(b);
                dlgTitle = "SHA512";
            }
            else
                return;

            String hashStr = BitConverter.ToString(hash).Replace("-", "");
            String hashB64Str = Convert.ToBase64String(hash);
            String data2hash = GetTipStringFromByteArray(b, maxDisplayDataStringLen);

            if (hashResultDlg == null)
                hashResultDlg = new HashResultDlg();

            hashResultDlg.SetDlgTitle(dlgTitle);
            hashResultDlg.SetDataTipText(data2hash);
            hashResultDlg.SetHashResult(hashStr);
            hashResultDlg.SetHashB64Result(hashB64Str);
            hashResultDlg.Show();
        }

        internal static void md5()
        {
            hash(HashMethod.hm_MD5);
        }

        internal static void sha1()
        {
            hash(HashMethod.hm_SHA1);
        }

        internal static void sha256()
        {
            hash(HashMethod.hm_SHA256);
        }

        internal static void sha384()
        {
            hash(HashMethod.hm_SHA384);
        }

        internal static void sha512()
        {
            hash(HashMethod.hm_SHA512);
        }

        protected static byte[] GetMD5(byte[] input)
        {
            MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider();
            return md5hasher.ComputeHash(input);
        }

        protected static byte[] GetSha1(byte[] input)
        {
            SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
            return sha1hasher.ComputeHash(input);
        }

        protected static byte[] GetSha256(byte[] input)
        {
            SHA256Managed sha256hasher = new SHA256Managed();
            return sha256hasher.ComputeHash(input);
        }

        protected static byte[] GetSha384(byte[] input)
        {
            SHA384Managed sha384hasher = new SHA384Managed();
            return sha384hasher.ComputeHash(input);
        }

        protected static byte[] GetSha512(byte[] input)
        {
            SHA512Managed sha512hasher = new SHA512Managed();
            return sha512hasher.ComputeHash(input);
        }

        #endregion
    }
}