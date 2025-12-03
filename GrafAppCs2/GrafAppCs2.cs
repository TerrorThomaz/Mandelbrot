using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

Form scherm = new Form();
scherm.Text = "GrafAppCs2";
scherm.BackColor = Color.LightYellow;
scherm.ClientSize = new Size(1920, 1080);



// met een Bitmap kun je een plaatje opslaan in het geheugen
Bitmap plaatje = new Bitmap(1200, 1200);
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(0, 0);
afbeelding.Size = new Size(1200, 900);
afbeelding.BackColor = Color.White;
afbeelding.Image = plaatje;


//min- en maximums waarmee zal worden gerekend
double calc_x_min = -2.0;
double calc_y_min = -2.0;
double calc_x_max = 2.0;
double calc_y_max = 2.0;

// originele min- en maximums voor vergelijking
double orig_x_min = calc_x_min;
double orig_y_min = calc_y_min;
double orig_x_max = calc_x_max;
double orig_y_max = calc_y_max;

// originele kleuren
Color start = Color.Red;
Color end = Color.Blue;

//maximum iteraties origineel
int max_i = 4000;

//presets
(double X, double Y, double Zoom, string StartColor, string EndColor) preset1 = (0.0, 0.0, 1, "#FF0000", "#0000FF");
(double X, double Y, double Zoom, string StartColor, string EndColor) preset2 = (-0.75, 0.1, 1, "#00FF00", "#0000FF");
(double X, double Y, double Zoom, string StartColor, string EndColor) preset3 = (0.3, -0.01, 1, "#FF00FF", "#FFFF00");
(double X, double Y, double Zoom, string StartColor, string EndColor) preset4 = (-1.25, 0.0, 1, "#00FFFF", "#FF00FF");


//label en textbox voor middelpunt X input
Label Center_X_Label = new Label();
Center_X_Label.Text = "Geef een X coördinaat voor het middelpunt (tussen -2 en 2)";
Center_X_Label.Location = new Point(1250, 10);
scherm.Controls.Add(Center_X_Label);
Center_X_Label.Size = new Size(400, 20);
TextBox Center_X_TB = new TextBox();
Center_X_TB.Location = new Point(1250, 30);
Center_X_TB.Size = new Size(100, 40);
Center_X_TB.Text = "0.0";
scherm.Controls.Add(Center_X_TB);

//label en textbox voor middelpunt Y input
Label Center_Y_Label = new Label();
Center_Y_Label.Text = "Geef een Y coördinaat voor het middelpunt (tussen -2 en 2)";
Center_Y_Label.Location = new Point(1250, 50);
scherm.Controls.Add(Center_Y_Label);
Center_Y_Label.Size = new Size(400, 20);
TextBox Center_Y_TB = new TextBox();
Center_Y_TB.Location = new Point(1250, 70);
Center_Y_TB.Size = new Size(100, 40);
Center_Y_TB.Text = "0.0";
scherm.Controls.Add(Center_Y_TB);

//label en textbox voor zoom waarde input
Label Zoom_Label = new Label();
Zoom_Label.Text = "Geef de zoom waarde aan (>0)";
Zoom_Label.Location = new Point(1250, 90);
scherm.Controls.Add(Zoom_Label);
Zoom_Label.Size = new Size(400, 20);
TextBox Zoom_TB = new TextBox();
Zoom_TB.Location = new Point(1250, 110);
Zoom_TB.Size = new Size(100, 40);
Zoom_TB.Text = "1";
scherm.Controls.Add(Zoom_TB);


//label en textbox voor zoom / click 
Label Zoom_Click_Label = new Label();
Zoom_Click_Label.Text = "Geef aan hoe snel je wil zoomen (>0)";
Zoom_Click_Label.Location = new Point(1250, 130);
scherm.Controls.Add(Zoom_Click_Label);
Zoom_Click_Label.Size = new Size(400, 20);
TextBox Zoom_Click_TB = new TextBox();
Zoom_Click_TB.Location = new Point(1250, 150);
Zoom_Click_TB.Size = new Size(100, 40);
Zoom_Click_TB.Text = "2";
scherm.Controls.Add(Zoom_Click_TB);


//labels en textbox voor input van de kleuren
Label start_kleur_label = new Label();
start_kleur_label.Text = "geef me een kleur in hex code (rgb)";
start_kleur_label.Location = new Point(1250, 170);
start_kleur_label.Size = new Size(400, 20);
scherm.Controls.Add(start_kleur_label);
TextBox start_kleur_TB = new TextBox();
start_kleur_TB.Location = new Point(1250, 190);
start_kleur_TB.Size = new Size(100, 40);
start_kleur_TB.Text = "#FF0000";
scherm.Controls.Add(start_kleur_TB);

Label eind_kleur_label = new Label();
eind_kleur_label.Text = "geef me een kleur in hex code (rgb)";
eind_kleur_label.Location = new Point(1250, 210);
eind_kleur_label.Size = new Size(400, 20);
scherm.Controls.Add(eind_kleur_label);
TextBox eind_kleur_TB = new TextBox();
eind_kleur_TB.Location = new Point(1250, 230);
eind_kleur_TB.Size = new Size(100, 40);
eind_kleur_TB.Text = "#0000FF";
scherm.Controls.Add(eind_kleur_TB);

//max iteratie label en textbox
Label iteraties_label = new Label();
iteraties_label.Text = "Geef het maximum aantal iteraties (>0)";
iteraties_label.Location = new Point(1250, 250);
iteraties_label.Size = new Size(400, 20);
scherm.Controls.Add(iteraties_label);
TextBox iteraties_TB = new TextBox();
iteraties_TB.Location = new Point(1250, 270);
iteraties_TB.Size = new Size(100, 40);
iteraties_TB.Text = "4000";
scherm.Controls.Add(iteraties_TB);

//preset buttons
Button preset1_button = new Button();
preset1_button.Text = "Preset 1";
preset1_button.Location = new Point(1250, 330);
preset1_button.Size = new Size(80, 30);
scherm.Controls.Add(preset1_button);

Button preset2_button = new Button();
preset2_button.Text = "Preset 2";
preset2_button.Location = new Point(1350, 330);
preset2_button.Size = new Size(80, 30);
scherm.Controls.Add(preset2_button);

Button preset3_button = new Button();
preset3_button.Text = "Preset 3";
preset3_button.Location = new Point(1250, 370);
preset3_button.Size = new Size(80, 30);
scherm.Controls.Add(preset3_button);

Button preset4_button = new Button();
preset4_button.Text = "Preset 4";
preset4_button.Location = new Point(1350, 370);
preset4_button.Size = new Size(80, 30);
scherm.Controls.Add(preset4_button);



// functie om de gegeven waardes te checken en de nieuwe waardes te implementeren
void lees_TB()
{

    //checken of de gegeven waardes in de juiste vorm zijn
    if (!double.TryParse(Center_X_TB.Text, out double center_X))
    {
        MessageBox.Show("Middelpunt X niet goed, geef een getal", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
    if (!double.TryParse(Center_Y_TB.Text, out double center_Y))
    {
        MessageBox.Show("Middelpunt Y waarde niet goed, geef een getal", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
    if (!double.TryParse(Zoom_TB.Text, out double zoom) || zoom <= 0)
    {
        MessageBox.Show("Zoom waarde te klein. Geef een waarde groter dan 0", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
    if (!int.TryParse(iteraties_TB.Text, out int max_i) || max_i <= 0)
    {
        MessageBox.Show("Ongeldige waarde voor maximum iteraties. Geef een getal groter dan 0.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }



    // start kleur
    Color startColor;
    try
    {
        startColor = ColorTranslator.FromHtml(start_kleur_TB.Text);
    }
    catch
    {
        MessageBox.Show(
            "Ongeldige startkleur. Gebruik bijv. 'FF0000', '#FF0000', 'Blue' of '128,0,255'.",
            "Input error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );
        return;
    }

    //eind kleur
    Color endColor;
    try
    {
        endColor = ColorTranslator.FromHtml(eind_kleur_TB.Text);
    }
    catch
    {
        MessageBox.Show(
            "Ongeldige eindkleur. Gebruik bijv. '00FF00', '#00FF00', 'Red' of '128,255,0'.",
            "Input error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );
        return;
    }

    // kleuren toepassen
    start = startColor;
    end = endColor;


    //originele waardes
    double orig_width = orig_x_max - orig_x_min;
    double orig_height = orig_y_max - orig_y_min;
    //nieuwe waardes door input
    double new_width = orig_width / zoom;
    double new_height = orig_height / zoom;
    //centreren
    calc_x_min = center_X - new_width / 2.0;
    calc_x_max = center_X + new_width / 2.0;
    calc_y_min = center_Y - new_height / 2.0;
    calc_y_max = center_Y + new_height / 2.0;

}

//veranderingen om te zorgen dat de textboxes worden geüpdatet
void zoom(object o, MouseEventArgs ea)
{

    //checken of de gegeven zoom waarde wel correct is 
    if (!double.TryParse(Zoom_Click_TB.Text, out double click_zoom) || click_zoom <= 0)
    {
        MessageBox.Show("Zoom waarde te klein of geen getal. Geef een getal groter dan 0", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }


    //rechtermuisclick zorgt voor uitzoomen 
    double actual_zoom = (ea.Button == MouseButtons.Right) ? 1.0 / click_zoom : click_zoom;


    double calc_x_stap = (calc_x_max - calc_x_min) / plaatje.Width;
    double calc_y_stap = (calc_y_max - calc_y_min) / plaatje.Height;

    
    double clicked_x = calc_x_min + ea.X * calc_x_stap;
    double clicked_y = calc_y_min + ea.Y * calc_y_stap;
 

    double currentWidth = calc_x_max - calc_x_min;
    double origWidth = orig_x_max - orig_x_min;
    double currentZoom = origWidth / currentWidth;


    double newZoom = currentZoom * actual_zoom;


    double newWidth = origWidth / newZoom;
    double newHeight = (orig_y_max - orig_y_min) / newZoom;


    calc_x_min = clicked_x - newWidth / 2.0;
    calc_x_max = clicked_x + newWidth / 2.0;
    calc_y_min = clicked_y - newHeight / 2.0;
    calc_y_max = clicked_y + newHeight / 2.0;

    //verander text in textboxes
    Center_X_TB.Text = clicked_x.ToString("G6");
    Center_Y_TB.Text = clicked_y.ToString("G6");
    Zoom_TB.Text = newZoom.ToString("G6");
    Zoom_Click_TB.Text = click_zoom.ToString("G6");
    
    mandel(null, EventArgs.Empty);
}



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

// enter zorgt voor berekening
Center_X_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };
Center_Y_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };
Zoom_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };
start_kleur_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };
eind_kleur_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };
iteraties_TB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { lees_TB(); mandel(null, EventArgs.Empty); } };


// preset buttons click functionaliteit
preset1_button.Click += (s, e) =>
{
    Center_X_TB.Text = preset1.X.ToString("G6");
    Center_Y_TB.Text = preset1.Y.ToString("G6");
    Zoom_TB.Text = preset1.Zoom.ToString("G6");
    start_kleur_TB.Text = preset1.StartColor;
    eind_kleur_TB.Text = preset1.EndColor;
    lees_TB();
    mandel(null, EventArgs.Empty);
};

preset2_button.Click += (s, e) =>
{
    Center_X_TB.Text = preset2.X.ToString("G6");
    Center_Y_TB.Text = preset2.Y.ToString("G6");
    Zoom_TB.Text = preset2.Zoom.ToString("G6");
    start_kleur_TB.Text = preset2.StartColor;
    eind_kleur_TB.Text = preset2.EndColor;
    lees_TB();
    mandel(null, EventArgs.Empty);
};

preset3_button.Click += (s, e) =>
{
    Center_X_TB.Text = preset3.X.ToString("G6");
    Center_Y_TB.Text = preset3.Y.ToString("G6");
    Zoom_TB.Text = preset3.Zoom.ToString("G6");
    start_kleur_TB.Text = preset3.StartColor;
    eind_kleur_TB.Text = preset3.EndColor;
    lees_TB();
    mandel(null, EventArgs.Empty);
};

preset4_button.Click += (s, e) =>
{
    Center_X_TB.Text = preset4.X.ToString("G6");
    Center_Y_TB.Text = preset4.Y.ToString("G6");
    Zoom_TB.Text = preset4.Zoom.ToString("G6");
    start_kleur_TB.Text = preset4.StartColor;
    eind_kleur_TB.Text = preset4.EndColor;
    lees_TB();
    mandel(null, EventArgs.Empty);
};



Button knop = new Button();
scherm.Controls.Add(knop);
knop.Location = new Point(1220, 10);
knop.Size = new Size(30, 30); 
knop.BackColor = Color.Black;
knop.Click += mandel;
knop.Click += (s, e) =>
{
    lees_TB();
    mandel(null, EventArgs.Empty);
};

lees_TB();
mandel(null, EventArgs.Empty);

// maar om complexere figuren te tekenen heb je een Graphics nodig

Button save_knop = new Button();
save_knop.Location = new Point(1220, 70);
save_knop.Size = new Size(30, 30);
save_knop.BackColor = Color.Black;
scherm.Controls.Add(save_knop);
save_knop.Click += opslaan;


// een Label kan ook gebruikt worden om een Bitmap te laten zien

Application.Run(scherm);