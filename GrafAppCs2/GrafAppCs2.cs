using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "GrafAppCs2";
scherm.BackColor = Color.LightYellow;
scherm.ClientSize = new Size(300, 300);



// met een Bitmap kun je een plaatje opslaan in het geheugen
Bitmap plaatje = new Bitmap(200, 200);
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(10, 10);
afbeelding.Size = new Size(200, 200);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;
void mandel(object o ,EventArgs ea)
{
    for (int y = 0; y < plaatje.Height; y++)
    {
        for (int x = 0; x < plaatje.Width; x++)
        {

            double calc_x_stap = 4d / plaatje.Width;
            double calc_x = calc_x_stap * x - 2;
            double calc_y_stap = 4d / plaatje.Height;
            double calc_y = calc_y_stap * y - 2;
            double pyth = 0;
            double a = 0;
            double b = 0;
            int i = 0;
            while (pyth < 2 && i < 400)
            {
                double n_a = a * a - b * b + calc_x;
                double n_b = 2 * a * b + calc_y;
                b = n_b;
                a = n_a;
                pyth = Math.Sqrt((a * a) + (b * b));

                i = i + 1;
            }
            if (i % 2 == 0)
            {
                plaatje.SetPixel(x, y, Color.Black);
                afbeelding.Invalidate();
            }
            else
            {
                plaatje.SetPixel(x, y, Color.White);
                afbeelding.Invalidate();
            }
        }
    }
    afbeelding.Refresh();
}



Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(220, 10);
knop.Size = new Size(30, 30); 
knop.BackColor = Color.Red;
knop.Click += mandel;
Application.Run(scherm);
// maar om complexere figuren te tekenen heb je een Graphics nodig

// een Label kan ook gebruikt worden om een Bitmap te laten zien

