using System;
using System.Drawing;
using System.Windows.Forms;


public class GameOverScreen : Form
{
    readonly Label text;

    public GameOverScreen()
    {
        Font textFont = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);

        text = new Label() 
        {
            Location = new Point(122, 120),
            Size = new Size(255, 57),
            Text = "GAME OVER",
            TextAlign = ContentAlignment.TopCenter,
            Font = textFont,
            ForeColor = Color.White
        };


        // Form properties
        BackColor = Color.Black;
        ClientSize = new Size(515, 355);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.None;
        Controls.Add(text);
    }
}
