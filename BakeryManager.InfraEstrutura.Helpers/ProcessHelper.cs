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

        public static string GetRandomUID()
        {
            var guid = Guid.NewGuid().ToString().Replace("[", "").Replace("]", "").Replace("-", "").Substring(0,10);
            var randonFormater = new Random().Next(1,4);
            


            switch (randonFormater)
            {
                case 1:
                    return string.Concat(DateTime.Now.Ticks.ToString(), guid);
                case 2:
                    return string.Concat(guid, DateTime.Now.Ticks.ToString());
                case 3:
                    return string.Concat(DateTime.Now.ToString("yyyyMMddhhmmss"),guid);
                default:
                    return string.Concat(guid, DateTime.Now.ToString("yyyyMMddhhmmss"));

            }

            
        }
    }
}
