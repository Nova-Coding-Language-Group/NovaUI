using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static class NovaUI
{
    const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
    const uint WS_THICKFRAME       = 0x00040000;
    const uint WS_MAXIMIZEBOX      = 0x00010000;
    const uint WS_VISIBLE          = 0x10000000;
    const int  CS_HREDRAW          = 0x0002;
    const int  CS_VREDRAW          = 0x0001;
    const int  CS_OWNDC            = 0x0020;
    const uint WM_DESTROY          = 0x0002;
    const uint WM_PAINT            = 0x000F;
    const uint WM_LBUTTONDOWN      = 0x0201;
    const uint WM_LBUTTONUP        = 0x0202;
    const uint WM_MOUSEMOVE        = 0x0200;
    const uint WM_CLOSE            = 0x0010;
    const uint WM_ERASEBKGND       = 0x0014;
    const uint WM_SETCURSOR        = 0x0020;
    const uint WM_SETICON          = 0x0080;
    const uint WM_CHAR             = 0x0102;
    const uint WM_KEYDOWN          = 0x0100;
    const uint WM_LBUTTONDBLCLK    = 0x0203;
    const int  VK_BACK             = 0x08;
    const int  VK_RETURN           = 0x0D;
    const int  IDC_ARROW           = 32512;
    const int  IDC_HAND            = 32649;
    const int  IDC_IBEAM           = 32513;
    const int  SW_SHOW             = 5;
    const int  PS_SOLID            = 0;
    const int  NULL_PEN            = 8;
    const int  CLEARTYPE_QUALITY   = 5;
    const int  FW_NORMAL           = 400;
    const int  FW_BOLD             = 700;
    const int  DT_CENTER           = 0x00000001;
    const int  DT_VCENTER          = 0x00000004;
    const int  DT_SINGLELINE       = 0x00000020;
    const int  DT_LEFT             = 0x00000000;
    const int  DT_WORD_ELLIPSIS    = 0x00040000;
    const uint SRCCOPY             = 0x00CC0020;
    const int  IMAGE_ICON          = 1;
    const int  LR_LOADFROMFILE     = 0x0010;
    const int  ICON_SMALL          = 0;
    const int  ICON_BIG            = 1;

    [StructLayout(LayoutKind.Sequential)]
    struct WNDCLASSEX {
        public int cbSize; public uint style; public IntPtr lpfnWndProc;
        public int cbClsExtra; public int cbWndExtra; public IntPtr hInstance;
        public IntPtr hIcon; public IntPtr hCursor; public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)] public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)] public string lpszClassName;
        public IntPtr hIconSm;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct MSG { public IntPtr hwnd; public uint message; public IntPtr wParam; public IntPtr lParam; public uint time; public POINT pt; }
    [StructLayout(LayoutKind.Sequential)]
    struct POINT { public int x; public int y; }
    [StructLayout(LayoutKind.Sequential)]
    struct RECT  { public int left; public int top; public int right; public int bottom; }
    [StructLayout(LayoutKind.Sequential)]
    struct PAINTSTRUCT { public IntPtr hdc; public bool fErase; public RECT rcPaint; [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)] public byte[] rgbReserved; }
    [StructLayout(LayoutKind.Sequential)]
    struct SIZE { public int cx; public int cy; }

    delegate IntPtr WndProcDelegate(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")] static extern ushort RegisterClassExW(ref WNDCLASSEX wc);
    [DllImport("user32.dll")] static extern IntPtr CreateWindowExW(uint ex, [MarshalAs(UnmanagedType.LPWStr)] string cls, [MarshalAs(UnmanagedType.LPWStr)] string title, uint style, int x, int y, int w, int h, IntPtr parent, IntPtr menu, IntPtr inst, IntPtr param);
    [DllImport("user32.dll")] static extern bool   ShowWindow(IntPtr h, int n);
    [DllImport("user32.dll")] static extern bool   UpdateWindow(IntPtr h);
    [DllImport("user32.dll")] static extern bool   GetMessage(out MSG m, IntPtr h, uint f, uint l);
    [DllImport("user32.dll")] static extern bool   TranslateMessage(ref MSG m);
    [DllImport("user32.dll")] static extern IntPtr DispatchMessage(ref MSG m);
    [DllImport("user32.dll")] static extern void   PostQuitMessage(int n);
    [DllImport("user32.dll")] static extern IntPtr DefWindowProcW(IntPtr h, uint m, IntPtr w, IntPtr l);
    [DllImport("user32.dll")] static extern IntPtr BeginPaint(IntPtr h, out PAINTSTRUCT p);
    [DllImport("user32.dll")] static extern bool   EndPaint(IntPtr h, ref PAINTSTRUCT p);
    [DllImport("user32.dll")] static extern bool   InvalidateRect(IntPtr h, IntPtr r, bool e);
    [DllImport("user32.dll")] static extern bool   GetClientRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] static extern IntPtr LoadCursorW(IntPtr inst, IntPtr name);
    [DllImport("user32.dll")] static extern IntPtr SetCursor(IntPtr h);
    [DllImport("user32.dll")] static extern bool   GetCursorPos(out POINT p);
    [DllImport("user32.dll")] static extern bool   ScreenToClient(IntPtr h, ref POINT p);
    [DllImport("user32.dll")] static extern bool   PostMessage(IntPtr h, uint m, IntPtr w, IntPtr l);
    [DllImport("user32.dll")] static extern int    MessageBoxW(IntPtr h, [MarshalAs(UnmanagedType.LPWStr)] string t, [MarshalAs(UnmanagedType.LPWStr)] string c, uint f);
    [DllImport("user32.dll")] static extern bool   DrawTextW(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string s, int n, ref RECT r, uint f);
    [DllImport("user32.dll")] static extern IntPtr SendMessage(IntPtr h, uint m, IntPtr w, IntPtr l);
    [DllImport("user32.dll")] static extern IntPtr LoadImageW(IntPtr inst, [MarshalAs(UnmanagedType.LPWStr)] string name, uint type, int cx, int cy, uint load);
    [DllImport("user32.dll")] static extern bool   FillRect(IntPtr hdc, ref RECT r, IntPtr br);
    [DllImport("user32.dll")] static extern bool   SetCaretPos(int x, int y);
    [DllImport("user32.dll")] static extern bool   CreateCaret(IntPtr h, IntPtr bmp, int w, int ht);
    [DllImport("user32.dll")] static extern bool   ShowCaret(IntPtr h);
    [DllImport("user32.dll")] static extern bool   DestroyCaret();

    [DllImport("gdi32.dll")] static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")] static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int w, int h);
    [DllImport("gdi32.dll")] static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
    [DllImport("gdi32.dll")] static extern bool   DeleteObject(IntPtr obj);
    [DllImport("gdi32.dll")] static extern bool   DeleteDC(IntPtr hdc);
    [DllImport("gdi32.dll")] static extern bool   BitBlt(IntPtr dst, int x, int y, int w, int h, IntPtr src, int sx, int sy, uint op);
    [DllImport("gdi32.dll")] static extern IntPtr CreateSolidBrush(uint c);
    [DllImport("gdi32.dll")] static extern IntPtr CreatePen(int style, int w, uint c);
    [DllImport("gdi32.dll")] static extern bool   RoundRect(IntPtr hdc, int x1, int y1, int x2, int y2, int rx, int ry);
    [DllImport("gdi32.dll")] static extern uint   SetTextColor(IntPtr hdc, uint c);
    [DllImport("gdi32.dll")] static extern int    SetBkMode(IntPtr hdc, int m);
    [DllImport("gdi32.dll")] static extern IntPtr CreateFontW(int h, int w, int e, int o, int wt, uint i, uint u, uint s, uint cs, uint op, uint cp, uint q, uint pf, [MarshalAs(UnmanagedType.LPWStr)] string face);
    [DllImport("gdi32.dll")] static extern IntPtr GetStockObject(int obj);
    [DllImport("gdi32.dll")] static extern bool   MoveToEx(IntPtr hdc, int x, int y, IntPtr pt);
    [DllImport("gdi32.dll")] static extern bool   LineTo(IntPtr hdc, int x, int y);
    [DllImport("gdi32.dll")] static extern int    GetTextExtentPoint32W(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string s, int n, out SIZE sz);

    [DllImport("kernel32.dll")] static extern IntPtr GetModuleHandleW(string s);

    static uint MakeRGB(byte r, byte g, byte b)
    {
        return (uint)(r | (g << 8) | (b << 16));
    }

    static uint ParseHex(string hex)
    {
        if (hex == null) return 0xFFFFFFFF;
        string h = hex.TrimStart('#');
        if (h.Length != 6) return 0xFFFFFFFF;
        try
        {
            byte r = Convert.ToByte(h.Substring(0, 2), 16);
            byte g = Convert.ToByte(h.Substring(2, 2), 16);
            byte b = Convert.ToByte(h.Substring(4, 2), 16);
            return MakeRGB(r, g, b);
        }
        catch { return 0xFFFFFFFF; }
    }

    static bool IsValidHex(string hex)
    {
        if (hex == null) return false;
        string h = hex.TrimStart('#');
        if (h.Length != 6) return false;
        try { Convert.ToByte(h.Substring(0, 2), 16); Convert.ToByte(h.Substring(2, 2), 16); Convert.ToByte(h.Substring(4, 2), 16); return true; }
        catch { return false; }
    }

    static uint Darken(uint c, float f)
    {
        byte r = (byte)((c & 0xFF) * f);
        byte g = (byte)(((c >> 8) & 0xFF) * f);
        byte b = (byte)(((c >> 16) & 0xFF) * f);
        return MakeRGB(r, g, b);
    }

    static uint Lighten(uint c, float f)
    {
        int r = Math.Min(255, (int)((c & 0xFF) * f));
        int g = Math.Min(255, (int)(((c >> 8) & 0xFF) * f));
        int b = Math.Min(255, (int)(((c >> 16) & 0xFF) * f));
        return MakeRGB((byte)r, (byte)g, (byte)b);
    }

    static float Luminance(uint c)
    {
        float r = (c & 0xFF) / 255f;
        float g = ((c >> 8) & 0xFF) / 255f;
        float b = ((c >> 16) & 0xFF) / 255f;
        return r * 0.299f + g * 0.587f + b * 0.114f;
    }

    static uint AutoTextOn(uint bg)
    {
        return Luminance(bg) > 0.5f ? MakeRGB(20, 20, 20) : MakeRGB(235, 235, 235);
    }

    // Theme state — bg, accent, text are the three user-facing colours.
    // Everything else is derived.
    static uint s_bgColor     = MakeRGB(255, 255, 255);
    static uint s_accentColor = MakeRGB(0, 183, 255);
    static uint s_textColor   = MakeRGB(30, 30, 30);
    static uint s_borderColor = MakeRGB(200, 200, 210);
    static uint s_btnHov      = MakeRGB(0, 210, 255);
    static uint s_btnDn       = MakeRGB(0, 140, 200);
    static uint s_btnTxt      = MakeRGB(255, 255, 255);
    static uint s_shadowColor = MakeRGB(180, 180, 190);
    static uint s_topBarColor = MakeRGB(0, 183, 255);
    static bool s_topBarIsAccent = true;

    static void RebuildDerived()
    {
        s_btnHov      = Lighten(s_accentColor, 1.18f);
        s_btnDn       = Darken(s_accentColor, 0.75f);
        s_btnTxt      = AutoTextOn(s_accentColor);
        s_shadowColor = Darken(s_bgColor, 0.88f);
        byte br = (byte)(s_bgColor & 0xFF);
        byte bg = (byte)((s_bgColor >> 8) & 0xFF);
        byte bb = (byte)((s_bgColor >> 16) & 0xFF);
        s_borderColor = MakeRGB(
            (byte)Math.Max(0, br - 40),
            (byte)Math.Max(0, bg - 40),
            (byte)Math.Max(0, bb - 40));
        s_topBarColor = s_topBarIsAccent ? s_accentColor : Darken(s_bgColor, 0.82f);
    }

    // Apply a named preset. Returns false if name not recognised.
    static bool ApplyPreset(string name)
    {
        if (name == null) return false;
        switch (name.ToLower())
        {
            case "light":
                s_bgColor      = MakeRGB(255, 255, 255);
                s_accentColor  = MakeRGB(0, 183, 255);
                s_textColor    = MakeRGB(30, 30, 30);
                s_topBarIsAccent = false;
                break;
            case "dark":
                s_bgColor      = MakeRGB(28, 28, 30);
                s_accentColor  = MakeRGB(10, 132, 255);
                s_textColor    = MakeRGB(220, 220, 220);
                s_topBarIsAccent = false;
                break;
            case "nova":
                s_bgColor     = MakeRGB(18, 18, 18);
                s_accentColor = MakeRGB(138, 43, 226);
                s_textColor   = MakeRGB(220, 220, 220);
                s_topBarIsAccent = true;
                break;
            case "midnight":
                s_bgColor     = MakeRGB(15, 20, 40);
                s_accentColor = MakeRGB(80, 140, 255);
                s_textColor   = MakeRGB(200, 210, 240);
                s_topBarIsAccent = true;
                break;
            case "forest":
                s_bgColor     = MakeRGB(240, 245, 240);
                s_accentColor = MakeRGB(34, 139, 34);
                s_textColor   = MakeRGB(20, 40, 20);
                s_topBarIsAccent = true;
                break;
            case "ember":
                s_bgColor     = MakeRGB(30, 18, 12);
                s_accentColor = MakeRGB(220, 80, 20);
                s_textColor   = MakeRGB(240, 210, 180);
                s_topBarIsAccent = true;
                break;
            default:
                return false;
        }
        RebuildDerived();
        return true;
    }

    // Set all three theme colours at once.
    public static void set_colors(string bgHex, string accentHex, string textHex)
    {
        if (IsValidHex(bgHex))     s_bgColor     = ParseHex(bgHex);
        if (IsValidHex(accentHex)) s_accentColor = ParseHex(accentHex);
        if (IsValidHex(textHex))   s_textColor   = ParseHex(textHex);
        RebuildDerived();
        Repaint();
    }

    public static void set_bg(string hex)     { if (IsValidHex(hex)) { s_bgColor     = ParseHex(hex); RebuildDerived(); Repaint(); } }
    public static void set_accent(string hex) { if (IsValidHex(hex)) { s_accentColor = ParseHex(hex); RebuildDerived(); Repaint(); } }
    public static void set_text(string hex)   { if (IsValidHex(hex)) { s_textColor   = ParseHex(hex); RebuildDerived(); Repaint(); } }

    // Called from Nova's colors() block. Applies a preset then overrides.
    public static void _apply_preset(string preset)
    {
        ApplyPreset(preset);
    }

    enum WidgetKind { Button, Label, Textbox }
    enum BtnState   { Idle, Hover, Down }

    class Widget
    {
        public WidgetKind Kind;
        public int X, Y, W, H;
        public string Text;
        public Action   Callback;
        public string   GridTag;
        public int      GridRow;
        public int      GridCol;
        public BtnState State;
        public uint     BgColor;
        public bool     HasBg;
        public uint     FgColor;
        public bool     HasFg;
        public int      Id;
        public string   Value   = "";
        public bool     Focused = false;

        // Legacy single-colour slot — maps to BgColor for buttons, FgColor for labels
        public uint     Color    { get { return HasBg ? BgColor : FgColor; } }
        public bool     HasColor { get { return HasBg || HasFg; } }

        public Widget()
        {
            Text    = "";
            GridTag = "";
            State   = BtnState.Idle;
        }
    }

    static IntPtr       s_hwnd      = IntPtr.Zero;
    static IntPtr       s_hInstance = IntPtr.Zero;
    static bool         s_running   = false;
    static List<Widget> s_widgets   = new List<Widget>();
    static int          s_nextId    = 0;
    static Dictionary<string, Action<int,int>> s_gridCallbacks = new Dictionary<string, Action<int,int>>();
    static WndProcDelegate s_proc;
    static IntPtr       s_fontNormal = IntPtr.Zero;
    static IntPtr       s_fontBold   = IntPtr.Zero;
    static Dictionary<string, int> s_labelMap = new Dictionary<string, int>();

    const int DEFAULT_BTN_W = 140;
    const int DEFAULT_BTN_H = 38;
    const int TEXTBOX_H     = 32;

    public static void use_novaui() { }

    // ui_window — no colours: white/light theme
    public static void ui_window(string title, int width, int height, Action body)
    {
        RebuildDerived();
        RunWindow(title, width, height, body);
    }

    // ui_window — one colour: treated as accent (legacy behaviour preserved)
    public static void ui_window(string title, int width, int height, string accentHex, Action body)
    {
        if (IsValidHex(accentHex)) s_accentColor = ParseHex(accentHex);
        RebuildDerived();
        RunWindow(title, width, height, body);
    }

    // ui_window — three colours: bg, accent, text
    public static void ui_window(string title, int width, int height, string bgHex, string accentHex, string textHex, Action body)
    {
        if (IsValidHex(bgHex))     s_bgColor     = ParseHex(bgHex);
        if (IsValidHex(accentHex)) s_accentColor = ParseHex(accentHex);
        if (IsValidHex(textHex))   s_textColor   = ParseHex(textHex);
        RebuildDerived();
        RunWindow(title, width, height, body);
    }

    static void RunWindow(string title, int width, int height, Action body)
    {
        if (s_hwnd != IntPtr.Zero) return;

        s_hInstance = GetModuleHandleW(null);
        s_proc      = WndProc;

        WNDCLASSEX wc = new WNDCLASSEX();
        wc.cbSize        = Marshal.SizeOf(typeof(WNDCLASSEX));
        wc.style         = (uint)(CS_HREDRAW | CS_VREDRAW | CS_OWNDC);
        wc.lpfnWndProc   = Marshal.GetFunctionPointerForDelegate(s_proc);
        wc.hInstance     = s_hInstance;
        wc.hCursor       = LoadCursorW(IntPtr.Zero, (IntPtr)IDC_ARROW);
        wc.hbrBackground = IntPtr.Zero;
        wc.lpszClassName = "NovaUIWnd";

        RegisterClassExW(ref wc);

        uint style = (WS_OVERLAPPEDWINDOW & ~WS_THICKFRAME & ~WS_MAXIMIZEBOX) | WS_VISIBLE;
        s_hwnd = CreateWindowExW(0, "NovaUIWnd", title, style,
            unchecked((int)0x80000000), unchecked((int)0x80000000),
            width, height, IntPtr.Zero, IntPtr.Zero, s_hInstance, IntPtr.Zero);

        if (s_hwnd == IntPtr.Zero) return;

        s_fontNormal = CreateFontW(-16, 0, 0, 0, FW_NORMAL, 0, 0, 0, 0, 0, 0, CLEARTYPE_QUALITY, 0, "Segoe UI");
        s_fontBold   = CreateFontW(-16, 0, 0, 0, FW_BOLD,   0, 0, 0, 0, 0, 0, CLEARTYPE_QUALITY, 0, "Segoe UI");

        ShowWindow(s_hwnd, SW_SHOW);
        UpdateWindow(s_hwnd);

        if (body != null) body();

        s_running = true;
        MSG msg;
        while (s_running && GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }

        DestroyCaret();
        if (s_fontNormal != IntPtr.Zero) DeleteObject(s_fontNormal);
        if (s_fontBold   != IntPtr.Zero) DeleteObject(s_fontBold);
        s_fontNormal = IntPtr.Zero;
        s_fontBold   = IntPtr.Zero;
        s_hwnd       = IntPtr.Zero;
    }

    public static void icon(string path)
    {
        if (s_hwnd == IntPtr.Zero || string.IsNullOrEmpty(path)) return;
        IntPtr h = LoadImageW(IntPtr.Zero, path, IMAGE_ICON, 32, 32, LR_LOADFROMFILE);
        if (h != IntPtr.Zero)
        {
            SendMessage(s_hwnd, WM_SETICON, (IntPtr)ICON_SMALL, h);
            SendMessage(s_hwnd, WM_SETICON, (IntPtr)ICON_BIG,   h);
        }
    }

    public static int label(string text, int x, int y, int w, int h)
    {
        return label(text, x, y, w, h, null);
    }

    public static int label(string text, int x, int y, int w, int h, string fgHex)
    {
        int id        = s_nextId++;
        Widget widget = new Widget();
        widget.Kind   = WidgetKind.Label;
        widget.X      = x;
        widget.Y      = y;
        widget.W      = w;
        widget.H      = h;
        widget.Text   = text != null ? text : "";
        widget.Id     = id;
        if (IsValidHex(fgHex))
        {
            widget.FgColor = ParseHex(fgHex);
            widget.HasFg   = true;
        }
        s_widgets.Add(widget);
        Repaint();
        return id;
    }

    public static void set_label(int id, string text)
    {
        foreach (Widget w in s_widgets)
        {
            if (w.Kind == WidgetKind.Label && w.Id == id)
            {
                w.Text = text != null ? text : "";
                Repaint();
                return;
            }
        }
    }

    public static void button(string text, int x, int y, Action callback)
    {
        button(text, x, y, DEFAULT_BTN_W, DEFAULT_BTN_H, null, callback);
    }

    public static void button(string text, int x, int y, int w, int h, Action callback)
    {
        button(text, x, y, w, h, null, callback);
    }

    public static void button(string text, int x, int y, string bgHex, Action callback)
    {
        button(text, x, y, DEFAULT_BTN_W, DEFAULT_BTN_H, bgHex, callback);
    }

    public static void button(string text, int x, int y, int w, int h, string bgHex, Action callback)
    {
        Widget widget   = new Widget();
        widget.Kind     = WidgetKind.Button;
        widget.X        = x;
        widget.Y        = y;
        widget.W        = w;
        widget.H        = h;
        widget.Text     = text != null ? text : "";
        widget.Callback = callback;
        if (IsValidHex(bgHex))
        {
            widget.BgColor = ParseHex(bgHex);
            widget.HasBg   = true;
        }
        s_widgets.Add(widget);
        Repaint();
    }

    public static void button_grid(int rows, int cols, int startX, int startY, int btnW, int btnH, string tag)
    {
        button_grid(rows, cols, startX, startY, btnW, btnH, tag, null);
    }

    public static void button_grid(int rows, int cols, int startX, int startY, int btnW, int btnH, string tag, Func<int,int,string> labelFn)
    {
        const int padX = 6;
        const int padY = 6;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int row = r;
                int col = c;
                int x   = startX + col * (btnW + padX);
                int y   = startY + row * (btnH + padY);
                string lbl = labelFn != null ? labelFn(row, col) : row + "," + col;

                string capturedTag = tag;
                int    capturedRow = row;
                int    capturedCol = col;

                Widget widget    = new Widget();
                widget.Kind      = WidgetKind.Button;
                widget.X         = x;
                widget.Y         = y;
                widget.W         = btnW;
                widget.H         = btnH;
                widget.Text      = lbl;
                widget.GridTag   = tag != null ? tag : "";
                widget.GridRow   = row;
                widget.GridCol   = col;
                widget.Callback  = delegate()
                {
                    Action<int,int> cb;
                    if (s_gridCallbacks.TryGetValue(capturedTag ?? "", out cb))
                        cb(capturedRow, capturedCol);
                };
                s_widgets.Add(widget);
            }
        }
        Repaint();
    }

    public static void on_button(string tag, Action<int,int> callback)
    {
        s_gridCallbacks[tag ?? ""] = callback;
    }

    public static void popup(string message)
    {
        MessageBoxW(s_hwnd, message ?? "", "Nova", 0x40);
    }

    public static int textbox(string caption, Action callback)
    {
        return textbox(caption, 20, NextAutoY(), 200, null, null, callback);
    }

    public static int textbox(string caption, int x, int y, int w, Action callback)
    {
        return textbox(caption, x, y, w, null, null, callback);
    }

    // Full overload — bgHex is the box fill, fgHex is the text colour inside.
    public static int textbox(string caption, int x, int y, int w, string bgHex, string fgHex, Action callback)
    {
        if (caption != null && caption.Length > 0)
        {
            Widget lbl  = new Widget();
            lbl.Kind    = WidgetKind.Label;
            lbl.X       = x;
            lbl.Y       = y - 20;
            lbl.W       = w;
            lbl.H       = 18;
            lbl.Text    = caption;
            lbl.Id      = s_nextId++;
            s_widgets.Add(lbl);
        }

        int id      = s_nextId++;
        Widget tb   = new Widget();
        tb.Kind     = WidgetKind.Textbox;
        tb.X        = x;
        tb.Y        = y;
        tb.W        = w;
        tb.H        = TEXTBOX_H;
        tb.Text     = caption != null ? caption : "";
        tb.Value    = "";
        tb.Id       = id;
        tb.Callback = callback;
        if (IsValidHex(bgHex)) { tb.BgColor = ParseHex(bgHex); tb.HasBg = true; }
        if (IsValidHex(fgHex)) { tb.FgColor = ParseHex(fgHex); tb.HasFg = true; }
        s_widgets.Add(tb);
        Repaint();
        return id;
    }

    public static string get_textbox(int id)
    {
        foreach (Widget w in s_widgets)
            if (w.Kind == WidgetKind.Textbox && w.Id == id)
                return w.Value;
        return "";
    }

    public static void set_textbox(int id, string value)
    {
        foreach (Widget w in s_widgets)
        {
            if (w.Kind == WidgetKind.Textbox && w.Id == id)
            {
                w.Value = value ?? "";
                Repaint();
                return;
            }
        }
    }

    static int NextAutoY()
    {
        int maxY = 60;
        foreach (Widget w in s_widgets)
            if (w.Y + w.H > maxY) maxY = w.Y + w.H;
        return maxY + 10;
    }

    public static void _RegisterLabelName(string name, int id)
    {
        if (name != null) s_labelMap[name] = id;
    }

    public static int _GetLabelId(string name)
    {
        int id;
        if (name != null && s_labelMap.TryGetValue(name, out id)) return id;
        return -1;
    }

    public static void _SetLabelTextByName(string name, string text)
    {
        int id = _GetLabelId(name);
        if (id >= 0) set_label(id, text);
    }

    static void Repaint()
    {
        if (s_hwnd != IntPtr.Zero)
            InvalidateRect(s_hwnd, IntPtr.Zero, false);
    }

    static Widget HitTest(int mx, int my)
    {
        for (int i = s_widgets.Count - 1; i >= 0; i--)
        {
            Widget w = s_widgets[i];
            if ((w.Kind == WidgetKind.Button || w.Kind == WidgetKind.Textbox) &&
                mx >= w.X && mx < w.X + w.W &&
                my >= w.Y && my < w.Y + w.H)
                return w;
        }
        return null;
    }

    static POINT DecodeLParam(IntPtr lp)
    {
        int v  = lp.ToInt32();
        POINT p = new POINT();
        p.x = (short)(v & 0xFFFF);
        p.y = (short)((v >> 16) & 0xFFFF);
        return p;
    }

    static Widget FocusedTextbox()
    {
        foreach (Widget w in s_widgets)
            if (w.Kind == WidgetKind.Textbox && w.Focused) return w;
        return null;
    }

    static void SetFocusedTextbox(Widget target)
    {
        foreach (Widget w in s_widgets)
            w.Focused = (w == target);
        if (target != null)
        {
            CreateCaret(s_hwnd, IntPtr.Zero, 1, target.H - 10);
            ShowCaret(s_hwnd);
        }
        else
        {
            DestroyCaret();
        }
        Repaint();
    }

    static IntPtr WndProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam)
    {
        if (uMsg == WM_PAINT)
        {
            PAINTSTRUCT ps;
            IntPtr hdc = BeginPaint(hwnd, out ps);
            PaintAll(hwnd, hdc);
            EndPaint(hwnd, ref ps);
            return IntPtr.Zero;
        }
        else if (uMsg == WM_ERASEBKGND)
        {
            return (IntPtr)1;
        }
        else if (uMsg == WM_MOUSEMOVE)
        {
            POINT pt   = DecodeLParam(lParam);
            bool dirty = false;
            foreach (Widget w in s_widgets)
            {
                if (w.Kind != WidgetKind.Button) continue;
                bool over = pt.x >= w.X && pt.x < w.X + w.W && pt.y >= w.Y && pt.y < w.Y + w.H;
                BtnState ns = over ? (w.State == BtnState.Down ? BtnState.Down : BtnState.Hover) : BtnState.Idle;
                if (ns != w.State) { w.State = ns; dirty = true; }
            }
            if (dirty) InvalidateRect(hwnd, IntPtr.Zero, false);
            return IntPtr.Zero;
        }
        else if (uMsg == WM_LBUTTONDOWN)
        {
            POINT pt   = DecodeLParam(lParam);
            Widget hit = HitTest(pt.x, pt.y);
            if (hit != null)
            {
                if (hit.Kind == WidgetKind.Button)
                    hit.State = BtnState.Down;
                else if (hit.Kind == WidgetKind.Textbox)
                    SetFocusedTextbox(hit);
                InvalidateRect(hwnd, IntPtr.Zero, false);
            }
            else
            {
                SetFocusedTextbox(null);
            }
            return IntPtr.Zero;
        }
        else if (uMsg == WM_LBUTTONUP)
        {
            POINT pt   = DecodeLParam(lParam);
            Widget hit = HitTest(pt.x, pt.y);
            foreach (Widget w in s_widgets)
            {
                if (w.Kind == WidgetKind.Button && w.State == BtnState.Down)
                {
                    w.State = BtnState.Idle;
                    if (w == hit && w.Callback != null) w.Callback();
                }
            }
            InvalidateRect(hwnd, IntPtr.Zero, false);
            return IntPtr.Zero;
        }
        else if (uMsg == WM_CHAR)
        {
            Widget tb = FocusedTextbox();
            if (tb != null)
            {
                char ch = (char)wParam.ToInt32();
                if (ch == (char)VK_BACK)
                {
                    if (tb.Value.Length > 0)
                        tb.Value = tb.Value.Substring(0, tb.Value.Length - 1);
                }
                else if (ch == (char)VK_RETURN)
                {
                    if (tb.Callback != null) tb.Callback();
                }
                else if (ch >= 32)
                {
                    tb.Value += ch;
                }
                Repaint();
            }
            return IntPtr.Zero;
        }
        else if (uMsg == WM_SETCURSOR)
        {
            POINT pt = new POINT();
            GetCursorPos(out pt);
            ScreenToClient(hwnd, ref pt);
            Widget hit = HitTest(pt.x, pt.y);
            if (hit != null)
            {
                if (hit.Kind == WidgetKind.Textbox)
                    SetCursor(LoadCursorW(IntPtr.Zero, (IntPtr)IDC_IBEAM));
                else
                    SetCursor(LoadCursorW(IntPtr.Zero, (IntPtr)IDC_HAND));
                return (IntPtr)1;
            }
        }
        else if (uMsg == WM_CLOSE)
        {
            s_running = false;
            PostQuitMessage(0);
            return IntPtr.Zero;
        }
        else if (uMsg == WM_DESTROY)
        {
            return IntPtr.Zero;
        }

        return DefWindowProcW(hwnd, uMsg, wParam, lParam);
    }

    static void PaintAll(IntPtr hwnd, IntPtr screen)
    {
        RECT rc;
        GetClientRect(hwnd, out rc);
        int W = rc.right  - rc.left;
        int H = rc.bottom - rc.top;
        if (W <= 0 || H <= 0) return;

        IntPtr memDC  = CreateCompatibleDC(screen);
        IntPtr memBMP = CreateCompatibleBitmap(screen, W, H);
        IntPtr oldBMP = SelectObject(memDC, memBMP);

        IntPtr bgBr = CreateSolidBrush(s_bgColor);
        FillRect(memDC, ref rc, bgBr);
        DeleteObject(bgBr);

        DrawHLine(memDC, 0, 0, W, s_topBarColor, 3);

        foreach (Widget w in s_widgets)
        {
            if      (w.Kind == WidgetKind.Button)  DrawButton(memDC, w);
            else if (w.Kind == WidgetKind.Label)   DrawLabel(memDC, w);
            else if (w.Kind == WidgetKind.Textbox) DrawTextbox(memDC, w);
        }

        BitBlt(screen, 0, 0, W, H, memDC, 0, 0, SRCCOPY);
        SelectObject(memDC, oldBMP);
        DeleteObject(memBMP);
        DeleteDC(memDC);

        Widget focused = FocusedTextbox();
        if (focused != null)
        {
            PAINTSTRUCT dummy;
            IntPtr scrDC = BeginPaint(hwnd, out dummy);
            int cx = MeasureText(scrDC, focused.Value, s_fontNormal) + focused.X + 6;
            int cy = focused.Y + 5;
            SetCaretPos(cx, cy);
            EndPaint(hwnd, ref dummy);
        }
    }

    static int MeasureText(IntPtr hdc, string text, IntPtr font)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        IntPtr old = SelectObject(hdc, font);
        SIZE sz;
        GetTextExtentPoint32W(hdc, text, text.Length, out sz);
        SelectObject(hdc, old);
        return sz.cx;
    }

    static void DrawButton(IntPtr hdc, Widget w)
    {
        uint baseColor = w.HasBg ? w.BgColor : s_accentColor;
        uint hovColor  = Lighten(baseColor, 1.18f);
        uint dnColor   = Darken(baseColor, 0.75f);
        uint txtColor  = w.HasFg ? w.FgColor : AutoTextOn(baseColor);

        uint fill;
        if      (w.State == BtnState.Hover) fill = hovColor;
        else if (w.State == BtnState.Down)  fill = dnColor;
        else                                fill = baseColor;

        int dy = w.State == BtnState.Down ? 2 : 0;

        if (w.State != BtnState.Down)
        {
            IntPtr shBr    = CreateSolidBrush(s_shadowColor);
            IntPtr nullPen = GetStockObject(NULL_PEN);
            IntPtr op      = SelectObject(hdc, nullPen);
            IntPtr ob      = SelectObject(hdc, shBr);
            RoundRect(hdc, w.X + 2, w.Y + 3, w.X + w.W + 2, w.Y + w.H + 3, 8, 8);
            SelectObject(hdc, op);
            SelectObject(hdc, ob);
            DeleteObject(shBr);
        }

        IntPtr bodyBr  = CreateSolidBrush(fill);
        IntPtr bodyPen = CreatePen(PS_SOLID, 1, s_borderColor);
        IntPtr ob2     = SelectObject(hdc, bodyBr);
        IntPtr op2     = SelectObject(hdc, bodyPen);
        RoundRect(hdc, w.X, w.Y + dy, w.X + w.W, w.Y + w.H + dy, 8, 8);
        SelectObject(hdc, ob2);
        SelectObject(hdc, op2);
        DeleteObject(bodyBr);
        DeleteObject(bodyPen);

        SetBkMode(hdc, 1);
        SetTextColor(hdc, txtColor);
        IntPtr of = SelectObject(hdc, s_fontBold);
        RECT tr   = new RECT();
        tr.left   = w.X;
        tr.top    = w.Y + dy;
        tr.right  = w.X + w.W;
        tr.bottom = w.Y + w.H + dy;
        DrawTextW(hdc, w.Text, -1, ref tr, (uint)(DT_CENTER | DT_VCENTER | DT_SINGLELINE));
        SelectObject(hdc, of);
    }

    static void DrawLabel(IntPtr hdc, Widget w)
    {
        uint col = w.HasFg ? w.FgColor : s_textColor;
        SetBkMode(hdc, 1);
        SetTextColor(hdc, col);
        IntPtr of = SelectObject(hdc, s_fontNormal);
        RECT tr   = new RECT();
        tr.left   = w.X;
        tr.top    = w.Y;
        tr.right  = w.X + w.W;
        tr.bottom = w.Y + w.H;
        DrawTextW(hdc, w.Text, -1, ref tr, (uint)(DT_LEFT | DT_VCENTER | DT_SINGLELINE | DT_WORD_ELLIPSIS));
        SelectObject(hdc, of);
    }

    static void DrawTextbox(IntPtr hdc, Widget w)
    {
        uint boxBg     = w.HasBg ? w.BgColor : Lighten(s_bgColor, 0.97f);
        uint boxFg     = w.HasFg ? w.FgColor : s_textColor;
        uint borderCol = w.Focused ? s_accentColor : s_borderColor;

        IntPtr bgBr = CreateSolidBrush(boxBg);
        IntPtr pen  = CreatePen(PS_SOLID, w.Focused ? 2 : 1, borderCol);
        IntPtr ob   = SelectObject(hdc, bgBr);
        IntPtr op   = SelectObject(hdc, pen);
        RoundRect(hdc, w.X, w.Y, w.X + w.W, w.Y + w.H, 6, 6);
        SelectObject(hdc, ob);
        SelectObject(hdc, op);
        DeleteObject(bgBr);
        DeleteObject(pen);

        SetBkMode(hdc, 1);
        SetTextColor(hdc, boxFg);
        IntPtr of = SelectObject(hdc, s_fontNormal);
        RECT tr   = new RECT();
        tr.left   = w.X + 6;
        tr.top    = w.Y;
        tr.right  = w.X + w.W - 4;
        tr.bottom = w.Y + w.H;
        DrawTextW(hdc, w.Value, -1, ref tr, (uint)(DT_LEFT | DT_VCENTER | DT_SINGLELINE | DT_WORD_ELLIPSIS));
        SelectObject(hdc, of);
    }

    static void DrawHLine(IntPtr hdc, int x1, int y, int x2, uint color, int thick)
    {
        IntPtr pen = CreatePen(PS_SOLID, thick, color);
        IntPtr op  = SelectObject(hdc, pen);
        MoveToEx(hdc, x1, y, IntPtr.Zero);
        LineTo(hdc, x2, y);
        SelectObject(hdc, op);
        DeleteObject(pen);
    }
}
