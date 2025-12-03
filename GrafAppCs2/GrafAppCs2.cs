using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

Form scherm = new Form();
scherm.Text = "GrafAppCs2";
scherm.BackColor = Color.LightYellow;
scherm.ClientSize = new Size(1400, 1400);



// met een Bitmap kun je een plaatje opslaan in het geheugen
Bitmap plaatje = new Bitmap(1200, 1200);
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(0, 0);
afbeelding.Size = new Size(1200, 900);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;


double calc_x_min = -2.0;
double calc_y_min = -2.0;
double calc_x_max = 2.0;
double calc_y_max = 2.0;
int zoom_count = 1;
double factor = 0.5;
void zoom (object o, MouseEventArgs ea) {

    
    double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
    double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;
    // coordinaten in mandelbrot wordt bepaald via de muis coordinaat op de afbeelding maal de schaal
    double clicked_x = calc_x_min + ea.X * calc_x_stap;
    double clicked_y = calc_y_min + ea.Y * calc_y_stap;
    //inzoom factor
    //totale nieuwe breedte/hoogte voor de uitvoering berekening
    double width = (calc_x_max - calc_x_min) * factor;
    double height = (calc_y_max - calc_y_min) * factor;
    //meest linker en rechter
    calc_x_min = clicked_x - width / 2;
    calc_x_max = clicked_x + width / 2;
    calc_y_min = clicked_y - height / 2;
    calc_y_max = clicked_y + height / 2;

    mandel(null, EventArgs.Empty);
    zoom_count += 1;
    
}

int max_i = 400;
int mandel_berekening(double calc_x_schaal ,double calc_y_schaal, int x, int y)
{
    // bepaling x coordinaten wat berekent moet worden voor pixel x/y
    double calc_x = calc_x_min + calc_x_schaal * x;
    double calc_y = calc_y_min + calc_y_schaal * y;
    double a = 0;
    double b = 0;
    int i = 0;
    // via stelling van pythagoras en een maximaal
    while ((a*a + b*b) < 4 && i < max_i)
    {
        double n_a = a * a - b * b + calc_x;
        b = 2 * a * b + calc_y;
        a = n_a;
        i = i + 1;
    }
    return (i);
}

Color start = Color.AntiqueWhite;
Color end = Color.Blue;

double power = 0.15;

//generatie van een array waarbij het absolute verschil wordt berekent en dan de increment van rgb


void mandel(object o, EventArgs ea)
{

    int N = 256;
    double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
    double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;
    for (int y = 0; y < plaatje.Height; y++)
    {
        for (int x = 0; x < plaatje.Width; x++)
        {
            int i = mandel_berekening(calc_x_stap, calc_y_stap, x, y);

            //berekening vanuit wikipedia "Plotting algorithms for the Mandelbrot set"
            int steps = 10000;
            double t = (double)i / max_i;       // waarde van 0–1
            double v = Math.Pow(t, power) * N;  
            double index = v % N;
            int rgb = (int)v % 256;

            plaatje.SetPixel(x, y, Color.FromArgb((int)(start.R*rgb)%256, (int)(start.G * rgb)%256, (int)(start.B * rgb)%256));
            if (i > max_i)
            {
                plaatje.SetPixel(x, y, Color.Black);
                continue;
            }

        }
    }
    afbeelding.Invalidate();
}

void opslaan(object o, EventArgs ea) {
    plaatje.Save("output.bmp", ImageFormat.Bmp);

}
afbeelding.MouseClick += zoom;

Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(1220, 10);
knop.Size = new Size(30, 30); 
knop.BackColor = Color.Red;
knop.Click += mandel;

// maar om complexere figuren te tekenen heb je een Graphics nodig

Button save_knop = new Button();
save_knop.Location = new Point(1220, 70);
save_knop.Size = new Size(30, 30);
save_knop.BackColor = Color.Black;
scherm.Controls.Add(save_knop);
save_knop.Click += opslaan;


// een Label kan ook gebruikt worden om een Bitmap te laten zien

Application.Run(scherm);