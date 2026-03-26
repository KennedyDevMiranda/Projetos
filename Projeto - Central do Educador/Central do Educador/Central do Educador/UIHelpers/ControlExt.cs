public static class ControlExt
{
    public static void SetTextSafe(this Control c, string text)
    {
        if (c == null) return;
        c.Text = text ?? "";
    }
}