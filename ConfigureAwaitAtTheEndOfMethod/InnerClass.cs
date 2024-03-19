using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigureAwaitAtTheEndOfMethod
{
    internal class InnerClass
    {
        private readonly ParentClass _parentClass;
        private bool _succeeded;
        
        public InnerClass(ParentClass parentClass)
        {
            _parentClass = parentClass;
            
            doDependentWorkAsync();

            doWorkAndSetFlag();
        }

        public bool Succeeded => _succeeded;

        private void doWorkAndSetFlag()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] doWork.Begin");
            Thread.Sleep(500); // do work
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] doWork.End");
            _succeeded = true;
            
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] SetFlag: {_succeeded}");
        }

        public void ResetFlag()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ResetFlag");
            _succeeded = false;
        }

        private async Task doDependentWorkAsync()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] doDependentWork.Begin");

            //await Task.Delay(100).ConfigureAwait(false); // Both cases are failed
            await Task.Delay(100).ConfigureAwait(true); // Both cases are succeeded

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] doDependentWork.End work.Succeeded: {_parentClass.Inner?.Succeeded}");
        }

        public async Task DoDependentWorkAsync()
        {
            await doDependentWorkAsync().ConfigureAwait(false);
        }

        public async Task DoWorkAsync()
        {
            await Task.Delay(1).ConfigureAwait(true);
            
            doWorkAndSetFlag();
        }
    }
}