using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SpinningDonut {
    class Donut {

        RenderWindow window;

        bool running = false;
        Clock tickLimiter;

        Text buffer;

        int k;
        float A = 0.0f, B = 0.0f, i, j;
        float[] z = new float[1760];
        char[] b = new char[1760];

        private void tick() {
            // Originally Code by Andy Sloane https://www.a1k0n.net/2011/07/20/donut-math.html

            buffer.DisplayedString = "";

            for (int f = 0; f < 1760; f++) {
                b[f] = ' ';
                z[f] = 0.0f;
            }

            for (j = 0.0f; 6.28 > j; j += 0.07f) {
                for (i = 0.0f; 6.28 > i; i += 0.02f) {
                    float c = (float)Math.Sin(i),
                          d = (float)Math.Cos(j),
                          e = (float)Math.Sin(A),
                          f = (float)Math.Sin(j),
                          g = (float)Math.Cos(A),
                          h = d + 2,
                          D = 1 / (c * h * e + f * g + 5),
                          l = (float)Math.Cos(i),
                          m = (float)Math.Cos(B),
                          n = (float)Math.Sin(B),
                          t = c * h * g - f * e;

                    int x = (int)(40 + (30 * D * ((l * h * m) - (t * n)))),
                        y = (int)(12 + (15 * D * ((l * h * n) + (t * m)))),
                        o = x + (80 * y),
                        N = (int)(8 * ((f * e - c * d * g) * m - c * d * e - f * g - l * d * n));
                    
                    if (22 > y && y > 0 && x > 0 && 80 > x && D > z[o]) {
                        z[o] = D;
                        b[o] = ".,-~:;=!*#$@"[N > 0 ? N : 0];
                    }
                }
            }

            for (k = 0; k < 1761; k++)
                buffer.DisplayedString += (k % 80 != 0 ? b[k].ToString() : "\n");

            A += 0.04f;
            B += 0.02f;
        }

        private void render() {
            buffer.Position = new Vector2f(0.0f, 0.0f);
            window.Draw(buffer);
        }

        private void run() {
            running = true;
            tickLimiter = new Clock();

            while (running) {
                if (tickLimiter.ElapsedTime.AsMilliseconds() >= 10) {
                    tickLimiter.Restart(); //limit execution to 100 ticks per second, so it runs 
                                           //uniformly on (most) systems, excluding the TI-84 Calculator

                    tick();
                }

                window.DispatchEvents();
                window.Clear();
                render();
                window.Display();
            }
        }

        private void initDisplay() {

            window = new RenderWindow((new VideoMode(800, 600)), "Graphical Character Donut", Styles.Default);
            
            window.Closed += (sender, e) => {
                running = false;
                ((Window)sender).Close();
                }; //add closing event as lambda function
        }

        private void init() {
            initDisplay();
            buffer = new Text("", new Font("Consolas.ttf"), 12);
        }

        static void Main(string[] args) {
            Donut d = new Donut();

            d.init();
            d.run();

        }
    }
}