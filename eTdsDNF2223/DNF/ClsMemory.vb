Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.IO

Public Class clsMemory
    '<DllImport("KERNEL32.DLL", EntryPoint:="SetProcessWorkingSetSize", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)> _
    'Friend Shared Function SetProcessWorkingSetSize(ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Integer, ByVal dwMaximumWorkingSetSize As Integer) As Boolean
    'End Function

    '<DllImport("KERNEL32.DLL", EntryPoint:="GetCurrentProcess", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)> _
    'Friend Shared Function GetCurrentProcess() As IntPtr
    'End Function

    <DllImport("KERNEL32.DLL", EntryPoint:=
      "SetProcessWorkingSetSize", SetLastError:=True,
      CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function SetProcessWorkingSetSize32Bit _
      (ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize _
      As Integer, ByVal dwMaximumWorkingSetSize As Integer) _
      As Boolean
    End Function

    <DllImport("KERNEL32.DLL", EntryPoint:= _
       "SetProcessWorkingSetSize", SetLastError:=True, _
       CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function SetProcessWorkingSetSize64Bit _
      (ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize _
      As Long, ByVal dwMaximumWorkingSetSize As Long) As Boolean
    End Function

    Public Sub FlushMem()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        'If Environment.Is64BitProcess Then '
        '    Console.WriteLine("64-bit process") '
        'Else '
        '    Console.WriteLine("32-bit process") '
        'End If '
        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            SetProcessWorkingSetSize32Bit(System.Diagnostics _
               .Process.GetCurrentProcess().Handle, -1, -1)
        Else
            SetProcessWorkingSetSize64Bit(System.Diagnostics _
               .Process.GetCurrentProcess().Handle, -1, -1)
        End If
    End Sub
End Class

