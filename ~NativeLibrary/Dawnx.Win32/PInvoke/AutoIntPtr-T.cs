using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Dawnx.Win32.PInvoke
{
    public sealed class AutoIntPtr<T> : IDisposable
        where T : struct
    {
        public IntPtr Ptr { get; private set; }
        public bool HasUnmanagedMemoryBeenAllocated { get; private set; }
        private T ManagedValue;

        public T Value
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (HasUnmanagedMemoryBeenAllocated)
                {
                    ManagedValue = (T)Marshal.PtrToStructure(Ptr, typeof(T));
                    Marshal.FreeHGlobal(Ptr);
                    HasUnmanagedMemoryBeenAllocated = false;
                    return ManagedValue;
                }
                else return ManagedValue;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static implicit operator IntPtr(AutoIntPtr<T> @this)
        {
            if (!@this.HasUnmanagedMemoryBeenAllocated)
            {
                @this.Ptr = Marshal.AllocHGlobal(Marshal.SizeOf(new T()));
                @this.HasUnmanagedMemoryBeenAllocated = true;
            }
            return @this.Ptr;
        }

        public void Dispose()
        {
            if (HasUnmanagedMemoryBeenAllocated) Marshal.FreeHGlobal(Ptr);
        }
    }

}
