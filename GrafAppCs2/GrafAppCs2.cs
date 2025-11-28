using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

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

int max_i = 4000;
int mandel_berekening(double calc_x_schaal ,double calc_y_schaal, int x, int y)
{
    double calc_x = calc_x_min + calc_x_schaal * x;
    double calc_y = calc_y_min + calc_y_schaal * y;
    double pyth = 0;
    double a = 0;
    double b = 0;
    int i = 0;

    while ((a*a + b*b) < 4 && i < max_i)
    {
        double n_a = a * a - b * b + calc_x;
        b = 2 * a * b + calc_y;
        a = n_a;
        i = i + 1;
    }
    return (i);
}

Color start = Color.Red;
Color end = Color.Blue;

double power = 0.2;


Color[] GeneratePalette(Color start, Color end, int steps = 256)
{
    Color[] palette = new Color[steps];

    double stepR = (end.R - start.R) / (double)(steps - 1);
    double stepG = (end.G - start.G) / (double)(steps - 1);
    double stepB = (end.B - start.B) / (double)(steps - 1);

    for (int i = 0; i < steps; i++)
    {
        int r = (int)(start.R + stepR * i);
        int g = (int)(start.G + stepG * i);
        int b = (int)(start.B + stepB * i);

        palette[i] = Color.FromArgb(r, g, b);
    }

    return palette;
}


void mandel(object o, EventArgs ea)
{

    Color[] palette = GeneratePalette(start, end, 256);
    int N = palette.Length;
    double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
    double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;
    for (int y = 0; y < plaatje.Height; y++)
    {
        for (int x = 0; x < plaatje.Width; x++)
        {
            int i = mandel_berekening(calc_x_stap, calc_y_stap, x, y);


            double t = (double)i / max_i;       // normalized 0–1
            double v = Math.Pow(t, power) * N;  // nonlinear mapping
            int index = (int)v % N;      
            plaatje.SetPixel(x, y, palette[index]);
            if (i == max_i)
            {
                plaatje.SetPixel(x, y, Color.Black);
                continue;
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

