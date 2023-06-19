using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GTAsuspender
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "la chillance pour la hasba mon deum";
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@ASCII());
            Sus();
            return 0;
        }

        static void Sus()
        {
            Process[] p = Process.GetProcessesByName("GTA5");
            Process proc = p[0];
            proc.Suspend();
            ProgressBar();
            proc.Resume();
        }

        static string ASCII()
        {
            return "                   ..^~!7????77!~:.                    \r\n               :~?YPGB###&&&&&###BGPY7~.               \r\n            :7YPG##&&&&&&&&&&&&&&&&&&#BPY!:            \r\n         .!Y5G#&&&&&&&&&&&&&&&&&&&&&&&&&#GPJ~          \r\n        !Y5G##&&&&&&&&&&&&&&&&&&&&&&&&&&&##GPY~        \r\n      :J5PGB###&&&&&&&&&&&&&&&&&&&&#&&&&###BBP5?.      \r\n     ^Y5PGBBB###BJ7###############&B!JB###BBGGP5Y:     \r\n    ^YY5PGGGP5?~:7B#################G~:~?5PGGGP5YY:    \r\n   .JY55?~^^::~?G#####################P?~::^~~?55YJ.   \r\n   7JY55YJJJ5PGBBBBBBBBBB####BBBBBBBBGGGGP5JJJY55YJ!   \r\n  .?JYY555PPPPPPGGGGBBBBBBBBBBBBBBBGGGGPPPPPP555YYJ?   \r\n  :?JJYY5Y5555PPPP?~~75GGGGGGGGG57~~?PPPP55555YYYJJ?.  \r\n  :??JJYYGPY5555P!..:.:5GPPPPPG5:.:..!P555555YYYJJ??.  \r\n  .??JJJ5@&7Y555P7.::.:5PPPPPPP5:.::.7P55555YYYJJJ?7   \r\n   !????&@@P!Y5555J!!75P5555555P57!!J55555YYYYJJJ??~   \r\n   .777G@@@&?~Y55555PP55555555555PP5555YYYYYJJJ???7.   \r\n    :75@@@@@#!~YYYYY555555555555555YYYYYYYJJJ?????:    \r\n    .?B&&&&&&5^7YYYYJ?7!!~~~~~!!7?YYYYJJJJJ?????7:     \r\n    ~J5BBBBGPJ!!JJJ!:.............:7JJJJ?????7?!.      \r\n    :5YY55YY??77??J^:!?????!?????7:~J????????7:        \r\n     .7JYYYJJ7!!77?7~JPPPP5YPPPPPJ~7???77??!:          \r\n        .....:^~77?!~JGGGGGPGGGGGJ~!???77^.            \r\n               .:~!!~75GGGGGGGGG57~!!~:.               \r\n                     .~?Y55555Y?~.                     \r\nfull hasba\U0001f975\U0001f975          .:^~^:.                        ";
        }

        static void ProgressBar()
        {
            for (int i = 0; i <= 77; i++)
            {
                for (int y = 0; y <= i; y++)
                {
                    Console.Write("█");
                }
                Console.WriteLine((i + 23) + "%/100%");
                Console.SetCursorPosition(0, 24);
                //Console.ForegroundColor = ConsoleColor.;
                Thread.Sleep(100);
            }
        }
    }

    [Flags]
    public enum ThreadAccess : int
    {
        SUSPEND_RESUME = (0x0002),
        GET_CONTEXT = (0x0008),
        SET_CONTEXT = (0x0010),
        SET_INFORMATION = (0x0020),
        QUERY_INFORMATION = (0x0040),
        SET_THREAD_TOKEN = (0x0080),
        IMPERSONATE = (0x0100),
    }

    public static class ProcessExtension
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                SuspendThread(pOpenThread);
            }
        }
        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                ResumeThread(pOpenThread);
            }
        }
    }
}
