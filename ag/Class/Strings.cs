using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ag.Class
{
     
    internal class mcyAy
        {

            public static string AppName = AppDomain.CurrentDomain.FriendlyName;
         
        private static Random random = new Random();
         
        public static string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
         
        public static string UpperReturn = null;
         
        public static void UpperCase(string Convert)
            {
                UpperReturn = "";

                string str1 = Convert;

                string upperstr1 = str1.ToUpper();

                UpperReturn = upperstr1;
            }
         
        public partial class NativeMethods
            {
                [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
                [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
                public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);
            }

    }
}
