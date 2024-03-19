using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ConfigureAwaitAtTheEndOfMethod
{
    internal class Program
    {
        private static ParentClass _parent;

        static async Task Main(string[] args)
        {
            //SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            //await RunBothCases();
            
            Application.Run(new MainForm());
        }

        internal static async Task RunBothCases()
        {
            ourCase();
            
            await Task.Delay(1000);

            _parent.Inner.ResetFlag();
            Console.WriteLine();

            hypotheticalCase();
        }
        
        private static void ourCase()
        {
            _parent = new ParentClass();
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Parent created");
        }

        private static async Task hypotheticalCase()
        {
            _parent.Inner.DoWorkAsync();
            await _parent.Inner.DoDependentWorkAsync().ConfigureAwait(false);

            await Task.Delay(2000).ConfigureAwait(true); // wait for result

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main.Succeeded: {_parent.Inner.Succeeded}");
        }
    }
}
