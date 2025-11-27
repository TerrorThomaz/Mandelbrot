using System;
using System.Drawing;
using System.Windows.Forms;

Form scherm = new Form();
scherm.Text = "GrafAppCs2";
scherm.BackColor = Color.LightYellow;
scherm.ClientSize = new Size(500, 500);



// met een Bitmap kun je een plaatje opslaan in het geheugen
Bitmap plaatje = new Bitmap(400, 400);
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(10, 10);
afbeelding.Size = new Size(400, 400);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;

double calc_x_min = -2.0;
double calc_y_min = -2.0;
double calc_x_max = 2.0;
double calc_y_max = 2.0;

void zoom (object o, MouseEventArgs ea) {


    double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
    double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;

    double clicked_x = calc_x_min + ea.X * calc_x_stap;
    double clicked_y = calc_y_min + ea.Y * calc_y_stap;

    double factor = 0.5;

    double width = (calc_x_max - calc_x_min) * factor;
    double height = (calc_y_max - calc_y_min) * factor;
    
    calc_x_min = clicked_x - width / 2;
    calc_x_max = clicked_x + width / 2;
    calc_y_min = clicked_y - height / 2;
    calc_y_max = clicked_y + height / 2;

    mandel(null, EventArgs.Empty);
    
    
}

void mandel(object o, EventArgs ea)
{
    for (int y = 0; y < plaatje.Height; y++)
    {
        for (int x = 0; x < plaatje.Width; x++)
        {
            double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
            double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;
            double calc_x = calc_x_min + calc_x_stap * x;

            double calc_y = calc_y_min + calc_y_stap * y;
            double pyth = 0;
            double a = 0;
            double b = 0;
            int i = 0;
            while (pyth < 2 && i < 4000)
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
    afbeelding.Invalidate();
}

afbeelding.MouseClick += zoom;

Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(420, 10);
knop.Size = new Size(30, 30); 
knop.BackColor = Color.Red;
knop.Click += mandel;
Application.Run(scherm);
// maar om complexere figuren te tekenen heb je een Graphics nodig

// een Label kan ook gebruikt worden om een Bitmap te laten zien

