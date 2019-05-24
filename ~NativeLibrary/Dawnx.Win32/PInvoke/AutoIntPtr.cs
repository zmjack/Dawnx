using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Dawnx.Win32.PInvoke
{
    public sealed class AutoIntPtr<T> : IDisposable
    {
        public IntPtr Ptr { get; private set; }
        private T ManagedValue;
        public bool HasUnmanagedMemoryBeenAllocated { get; private set; }

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
                @this.ManagedValue = default;
                @this.Ptr = Marshal.AllocHGlobal(sizeof(int));
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
