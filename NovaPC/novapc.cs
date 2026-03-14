// novapc.cs — Nova PC Stats Module
// Compile: & "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /target:library /out:novapc.dll novapc.cs
//
// Provides:
//   NovaPc.cpu()        -> string   e.g. "CPU: 14.2%"
//   NovaPc.ram()        -> string   e.g. "RAM: 3.1 GB / 16.0 GB (19%)"
//   NovaPc.gpu()        -> string   e.g. "GPU: 42%"
//   NovaPc.all_pc()     -> string   e.g. "CPU: 14.2% | RAM: 3.1 GB / 16.0 GB (19%) | GPU: 42%"
//   NovaPc.cpu_val()    -> float    raw CPU % (0-100)
//   NovaPc.ram_used()   -> float    used RAM in GB
//   NovaPc.ram_total()  -> float    total RAM in GB
//   NovaPc.gpu_val()    -> float    raw GPU % (0-100), or -1 if unavailable

using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

public static class NovaPc
{
    // ── CPU: sampled on a background thread so it never blocks the UI ─────────
    // PerformanceCounter needs two reads ~1 s apart for a real value.
    // A background thread refreshes the cached value every second.

    private static PerformanceCounter _cpuCounter;
    private static float              _cachedCpu  = 0f;
    private static bool               _cpuStarted = false;
    private static readonly object    _cpuLock    = new object();

    private static void EnsureCpuThread()
    {
        lock (_cpuLock)
        {
            if (_cpuStarted) return;
            _cpuStarted = true;
        }
        Thread t = new Thread(CpuLoop);
        t.IsBackground = true;
        t.Start();
    }

    private static void CpuLoop()
    {
        try
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _cpuCounter.NextValue(); // first read is always 0 — discard it
        }
        catch { _cpuCounter = null; return; }

        while (true)
        {
            Thread.Sleep(1000);
            try
            {
                float v = _cpuCounter.NextValue();
                lock (_cpuLock) { _cachedCpu = v; }
            }
            catch { return; }
        }
    }

    public static float cpu_val()
    {
        EnsureCpuThread();
        lock (_cpuLock) { return _cachedCpu; }
    }

    public static string cpu()
    {
        return string.Format("CPU: {0:F1}%", cpu_val());
    }

    // ── RAM: instant WinAPI call, never blocks ────────────────────────────────

    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORYSTATUSEX
    {
        public uint  dwLength;
        public uint  dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [DllImport("kernel32.dll")]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    private static bool GetMemStatus(out MEMORYSTATUSEX ms)
    {
        ms = new MEMORYSTATUSEX();
        ms.dwLength = (uint)Marshal.SizeOf(ms);
        return GlobalMemoryStatusEx(ref ms);
    }

    public static float ram_total()
    {
        MEMORYSTATUSEX ms;
        if (!GetMemStatus(out ms)) return -1f;
        return (float)(ms.ullTotalPhys / 1073741824.0);
    }

    public static float ram_used()
    {
        MEMORYSTATUSEX ms;
        if (!GetMemStatus(out ms)) return -1f;
        return (float)((ms.ullTotalPhys - ms.ullAvailPhys) / 1073741824.0);
    }

    public static string ram()
    {
        MEMORYSTATUSEX ms;
        if (!GetMemStatus(out ms)) return "RAM: N/A";
        float total = (float)(ms.ullTotalPhys / 1073741824.0);
        float used  = (float)((ms.ullTotalPhys - ms.ullAvailPhys) / 1073741824.0);
        return string.Format("RAM: {0:F1} GB / {1:F1} GB ({2}%)", used, total, ms.dwMemoryLoad);
    }

    // ── GPU: WMI on a background thread, result cached ────────────────────────
    // WMI can take 1-3 s on first call — running it in the background means
    // the UI thread is never blocked. First click returns "GPU: N/A" (loading),
    // subsequent clicks return the real value.

    private static float           _cachedGpu   = -1f;
    private static bool            _gpuStarted  = false;
    private static readonly object _gpuLock     = new object();

    private static void EnsureGpuThread()
    {
        lock (_gpuLock)
        {
            if (_gpuStarted) return;
            _gpuStarted = true;
        }
        Thread t = new Thread(GpuLoop);
        t.IsBackground = true;
        t.Start();
    }

    private static void GpuLoop()
    {
        while (true)
        {
            float result = -1f;
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT Name, UtilizationPercentage FROM " +
                    "Win32_PerfFormattedData_GPUPerformanceCounters_GPUEngine"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        object nameObj = obj["Name"];
                        string name = nameObj != null ? nameObj.ToString() : "";
                        if (name.IndexOf("engtype_3D", StringComparison.OrdinalIgnoreCase) < 0 &&
                            name.IndexOf("Graphics",   StringComparison.OrdinalIgnoreCase) < 0)
                            continue;
                        float v = Convert.ToSingle(obj["UtilizationPercentage"]);
                        if (v > result) result = v;
                    }
                }
            }
            catch { result = -1f; }

            lock (_gpuLock) { _cachedGpu = result; }
            Thread.Sleep(2000);
        }
    }

    public static float gpu_val()
    {
        EnsureGpuThread();
        lock (_gpuLock) { return _cachedGpu; }
    }

    public static string gpu()
    {
        float v = gpu_val();
        return v < 0 ? "GPU: N/A" : string.Format("GPU: {0:F0}%", v);
    }

    // ── Convenience ──────────────────────────────────────────────────────────

    public static string all_pc()
    {
        return cpu() + " | " + ram() + " | " + gpu();
    }
}
