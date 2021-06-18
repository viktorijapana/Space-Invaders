using System;
using System.Drawing;
using System.Windows.Forms;


public class DifficultySelector : Form
{
    readonly Button veryEasy;
    readonly Button easy;
    readonly Button normal;
    readonly Button hard;
    readonly Button veryHard;
    readonly Label title;

    public DifficultySelector()
    {
        Font buttonFont = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);

        title = new Label() 
        {
            Location = new Point(0, 40),
            Size = new Size(1030, 70),
            Text = "Choose Difficulty",
            TextAlign = ContentAlignment.TopCenter,
            Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
        };

        veryEasy = new Button() 
        {
            Location = new Point(399, 154),
            Size = new Size(241, 58),
            Text = "Very Easy",
            Font = buttonFont,
            BackColor = Color.White,
        };
        veryEasy.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, veryEasy));
        veryEasy.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, veryEasy));
        veryEasy.Click += new EventHandler((sender, e) => OnClick(sender, e, 5, 12));

        easy = new Button()
        {
            Location = new Point(399, 253),
            Size = new Size(241, 58),
            Text = "Easy",
            Font = buttonFont,
            BackColor = Color.White,
        };
        easy.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, easy));
        easy.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, easy));
        easy.Click += new EventHandler((sender, e) => OnClick(sender, e, 4, 16));

        normal = new Button()
        {
            Location = new Point(399, 353),
            Size = new Size(241, 58),
            Text = "Normal",
            Font = buttonFont,
            BackColor = Color.White,
        };
        normal.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, normal));
        normal.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, normal));
        normal.Click += new EventHandler((sender, e) => OnClick(sender, e, 3, 20));

        hard = new Button()
        {
            Location = new Point(399, 455),
            Size = new Size(241, 58),
            Text = "Hard",
            Font = buttonFont,
            BackColor = Color.White,
        };
        hard.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, hard));
        hard.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, hard));
        hard.Click += new EventHandler((sender, e) => OnClick(sender, e, 2, 26));

        veryHard = new Button()
        {
            Location = new Point(399, 555),
            Size = new Size(241, 58),
            Text = "Extreme",
            Font = buttonFont,
            BackColor = Color.White,
        };
        veryHard.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, veryHard));
        veryHard.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, veryHard));
        veryHard.Click += new EventHandler((sender, e) => OnClick(sender, e, 1, 32));


        Name = "Space Invaders";
        BackColor = Color.Black;
        ClientSize = new Size(1030, 710);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Controls.Add(title);
        Controls.Add(veryEasy);
        Controls.Add(easy);
        Controls.Add(normal);
        Controls.Add(hard);
        Controls.Add(veryHard);

        // event to shut down the entire program when the window is closed
        FormClosing += new FormClosingEventHandler(DifficultySelector_FormClosing);
    }


    private void OnHover(object sender, EventArgs e, Button b)
    {
        b.ForeColor = Color.Green;
    }

    private void EndHover(object sender, EventArgs e, Button b)
    {
        b.ForeColor = Color.Black;
    }

    private void OnClick(object sender, EventArgs e, int lives, int lazerSpeed)
    {
        GameWindow game = new GameWindow(lives, lazerSpeed);
        Hide();
        game.Show();
    }

    private void DifficultySelector_FormClosing(object sender, FormClosingEventArgs e)
    {
        Application.Exit();
    }
}
