using System.Drawing;
using System.Windows.Forms;

namespace DuplicateVidDetector.Controls;

/// <summary>
/// This class is a custom button that displays a symbol (like an icon) instead of text.
/// See: https://chatgpt.com/share/67f98f08-1960-8010-b44b-e66fa93c9e67
/// </summary>
public class SymbolButton : Button
{
  private readonly Color _normalColor = Color.Black;
  private readonly Color _hoverColor = Color.DarkSlateBlue;
  private readonly Color _pressedColor = Color.MidnightBlue;
  private bool _isHovered = false;
  private bool _isPressed = false;
  private readonly string? _tooltipText = null;
  private readonly ToolTip? _tooltip;

  public SymbolButton(string symbol, string? tooltipText = null)
  {
    FlatStyle = FlatStyle.Flat;
    FlatAppearance.BorderSize = 0;
    BackColor = Color.Transparent;

    Font = new Font("Segoe UI Symbol", 14f);
    Text = symbol;
    ForeColor = _normalColor;
    TextAlign = ContentAlignment.MiddleCenter;
    Size = new Size(40, 40);

    Cursor = Cursors.Hand;
    DoubleBuffered = true;

    _tooltipText = tooltipText;
    if (!string.IsNullOrEmpty(tooltipText))
    {
      _tooltip = new ToolTip();
      _tooltip.SetToolTip(this, tooltipText);
    }

    SetStyle(ControlStyles.Selectable, false); // remove focus border

    MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
    MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };
    MouseDown += (s, e) => { _isPressed = true; Invalidate(); };
    MouseUp += (s, e) => { _isPressed = false; Invalidate(); };
  }

  protected override void OnPaint(PaintEventArgs args)
  {
    base.OnPaint(args);
    var g = args.Graphics;
    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

    Color fillColor = _isPressed ? _pressedColor :
                      _isHovered ? _hoverColor :
                      _normalColor;

    TextRenderer.DrawText(
        g,
        Text,
        Font,
        ClientRectangle,
        fillColor,
        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
    );
  }
}

// Usage Example:
// var rotateBtn = new SymbolButton("⭮", "Rotate");
// rotateBtn.Location = new Point(10, 10);
// rotateBtn.Click += (s, e) => RotateSomething();
// this.Controls.Add(rotateBtn);
