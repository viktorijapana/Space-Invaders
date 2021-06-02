using System;
using System.Drawing;
using System.Windows.Forms;


public class DifficultySelector : Form
{
    private readonly Button veryEasy;
    private readonly Button easy;
    private readonly Button normal;
    private readonly Button hard;
    private readonly Button veryHard;
    private readonly Label title;

    public DifficultySelector()
    {
        title = new Label();
        veryEasy = new Button();
        easy = new Button();
        normal = new Button();
        hard = new Button();
        veryHard = new Button();
        Font buttonFont = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
        SuspendLayout();
            

        /* -- Title -- */
        title.Location = new Point(0, 40);
        title.Size = new Size(1030, 70);
        title.Text = "Choose Difficulty";
        title.TextAlign = ContentAlignment.TopCenter;
        title.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        title.ForeColor = Color.White;


        /* -- Very Easy button -- */
        veryEasy.Location = new Point(399, 154);
        veryEasy.Size = new Size(241, 58);
        veryEasy.Text = "Very Easy";
        veryEasy.Font = buttonFont;
        veryEasy.BackColor = Color.White;
        veryEasy.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, veryEasy));
        veryEasy.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, veryEasy));
        veryEasy.Click += new EventHandler((sender, e) => OnClick(sender, e, 5));


        /* -- Easy button -- */
        easy.Location = new Point(399, 253);
        easy.Size = new Size(241, 58);
        easy.Text = "Easy";
        easy.Font = buttonFont;
        easy.BackColor = Color.White;
        easy.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, easy));
        easy.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, easy));
        easy.Click += new EventHandler((sender, e) => OnClick(sender, e, 4));


        /* -- Normal button -- */
        normal.Location = new Point(399, 353);
        normal.Size = new Size(241, 58);
        normal.Text = "Normal";
        normal.Font = buttonFont;
        normal.BackColor = Color.White;
        normal.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, normal));
        normal.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, normal));
        normal.Click += new EventHandler((sender, e) => OnClick(sender, e, 3));


        /* -- Hard button -- */
        hard.Location = new Point(399, 455);
        hard.Size = new Size(241, 58);
        hard.Text = "Hard";
        hard.Font = buttonFont;
        hard.BackColor = Color.White;
        hard.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, hard));
        hard.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, hard));
        hard.Click += new EventHandler((sender, e) => OnClick(sender, e, 2));


        /* -- Very Hard button -- */
        veryHard.Location = new Point(399, 555);
        veryHard.Size = new Size(241, 58);
        veryHard.Text = "Extreme";
        veryHard.Font = buttonFont;
        veryHard.BackColor = Color.White;
        veryHard.MouseHover += new EventHandler((sender, e) => OnHover(sender, e, veryHard));
        veryHard.MouseLeave += new EventHandler((sender, e) => EndHover(sender, e, veryHard));
        veryHard.Click += new EventHandler((sender, e) => OnClick(sender, e, 1));


        /* -- Difficulty selector -- */
        Name = "DifficultySelector";
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
        ResumeLayout(false);
    }


    private void OnHover(object sender, System.EventArgs e, Button b)
    {
        b.ForeColor = Color.Green;
    }


    private void EndHover(object sender, System.EventArgs e, Button b)
    {
        b.ForeColor = Color.Black;
    }


    private void OnClick(object sender, System.EventArgs e, int lives)
    {
        GameWindow game = new GameWindow(lives);
        Hide();
        game.Show();
    }
}
