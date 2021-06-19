using System;
using System.Drawing;
using System.Windows.Forms;


public class GameOverScreen : Form
{
    readonly Label title;
    readonly Label restart;
    readonly Label quit;

    public GameOverScreen()
    {
        Font textFont = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point);

        title = new Label() 
        {
            Location = new Point(122, 69),
            Size = new Size(255, 57),
            Text = "GAME OVER",
            TextAlign = ContentAlignment.TopCenter,
            Font = textFont,
            ForeColor = Color.White
        };

        restart = new Label() 
        {
            Location = new Point(150, 157),
            Size = new Size(200, 45),
            Text = "Try Again",
            TextAlign = ContentAlignment.TopCenter,
            Font = textFont,
            ForeColor = Color.White
        };
        restart.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, restart));
        restart.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, restart));
        restart.Click += new EventHandler((sender, e) => Restart(sender, e));

        quit = new Label()
        {
            Location = new Point(180, 226),
            Size = new Size(200, 45),
            Text = "Give Up",
            Font = textFont,
            ForeColor = Color.White
        };
        quit.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, quit));
        quit.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, quit));
        quit.Click += new EventHandler((sender, e) => Application.Exit());


        // Form properties
        BackColor = Color.Black;
        ClientSize = new Size(515, 355);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.None;
        Controls.Add(quit);
        Controls.Add(restart);
        Controls.Add(title);
    }

    private void OnHover(object sender, EventArgs e, Label l)
    {
        l.ForeColor = Color.Green;
    }

    private void EndHover(object sender, EventArgs e, Label l)
    {
        l.ForeColor = Color.White;
    }

    private void Restart(object sender, EventArgs e)
    {
        //RestartGame();
    }
}
