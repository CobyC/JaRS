using System;
using System.Windows.Forms;


//found the inspiration for this at https://stackoverflow.com/questions/7079474/bindingsource-and-cross-thread-exceptions/59009375#59009375
// explanation at https://stackoverflow.com/questions/229554/whats-the-difference-between-invoke-and-begininvoke

namespace JARS.Core.WinForms.Extensions
{
    public static class WinFormControlsExtensions
    {
        /// <summary>
        /// Use this to create a method that needs to update UI if it might be executed on another thread.
        /// Executes on the UI thread, but calling thread waits for completion before continuing.
        /// This is the the method that returns the object and wraps the Invoke() method
        /// </summary>
        /// <param name="c">The current control</param>
        /// <param name="action">The method/code to execute</param>
        public static void InvokeIfRequired<T>(this T c, Action<T> action) where T : Control
        {
            if (c.InvokeRequired)            
                c.Invoke(new Action(() => action(c)));            
            else            
                action(c);            
        }

        /// <summary>
        /// Use this to create a method that needs to update UI if it might be executed on another thread.
        /// This is the the method that returns the IAsyncResult and wraps BeginInvoke()
        /// Executes asynchronously, on a threadpool thread.
        /// </summary>
        /// <param name="c">The current control</param>
        /// <param name="action">The method/code to execute</param>
        public static void BeginInvokeIfRequired<T>(this T c, Action<T> action) where T : Control
        {
            if (c.InvokeRequired)
                c.BeginInvoke(new Action(() => { action(c); }));
            else
                action(c);
        }
    }
}
