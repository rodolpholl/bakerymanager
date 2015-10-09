using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Infraestrutura.Helpers
{
    public static class ProcessHelper
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        public static Process GetProcessId(int AppHandler)
        {

            IntPtr i = new IntPtr(AppHandler);
            int id = 0;
            GetWindowThreadProcessId(i, out id);

            return Process.GetProcesses().FirstOrDefault(x => x.Id.Equals(id));

        }
    }
}
