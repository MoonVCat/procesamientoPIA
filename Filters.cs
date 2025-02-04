﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Procesamiento
{
    public static class Filters
    {
        public static Bitmap grayFilter(this Bitmap bmp)
        {
            int rgb;
            Color colour;

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    colour = bmp.GetPixel(x, y);
                    rgb = (int)Math.Round(.299 * colour.R + .587 * colour.G + .114 * colour.B);
                    bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }

            return bmp;
        }
        

        public static Bitmap sepiaFilter(Bitmap Bmp)
        {
            //get image dimension
            int width = Bmp.Width;
            int height = Bmp.Height;

            //Color of pixel
            Color p;

            //sepia
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    p = Bmp.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //calculate temp value
                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    //set new RGB value
                    if (tr > 255) { r = 255; } else { r = tr; }
                    if (tg > 255) { g = 255; } else { g = tg; }
                    if (tb > 255) { b = 255; } else { b = tb; }

                    //set the new RGB value in image pixel
                    Bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }


            return Bmp;
        }


        public static Bitmap negativeFilter(Bitmap Bmp)
        {
            //get image dimension
            int width = Bmp.Width;
            int height = Bmp.Height;

            //Color of pixel
            Color p;

            //negative
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    p = Bmp.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //find negative value
                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;

                    //set new ARGB value in pixel
                    Bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return Bmp;
        }

        public static Bitmap blurFilter(Bitmap Bmp, int blurAmmount)
        {
            int red = 0, blue = 0, green = 0, alpha = 0;
            int blurPixelCount = 0;
            Bitmap blurimg = new Bitmap(Bmp.Width, Bmp.Height);
            for (int i = blurAmmount; i < Bmp.Width - blurAmmount; i++)
            {

                for (int j = blurAmmount; j < Bmp.Height - blurAmmount; j++)
                {

                    Color clr = new Color();
                    for (int k = i - blurAmmount; k <= i + blurAmmount; k++)
                    {
                        for (int l = j - blurAmmount; l <= j + blurAmmount; l++)
                        {
                            red += Bmp.GetPixel(k, l).R;
                            green += Bmp.GetPixel(k, l).G;
                            blue += Bmp.GetPixel(k, l).B;

                            blurPixelCount++;

                        }
                    }
                    red = red / blurPixelCount;
                    green = green / blurPixelCount;
                    blue = blue / blurPixelCount;
                    clr = Color.FromArgb(red, green, blue);
                    blurimg.SetPixel(i, j, clr);
                    red = 0;
                    blue = 0; green = 0; alpha = 0; blurPixelCount = 0;

                }
            }


            return blurimg;
        }

        public static Bitmap blackAndWhiteFilter(Bitmap Bmp)
        {
            //get image dimension
            int width = Bmp.Width;
            int height = Bmp.Height;
            int Value;

            //Color of pixel
            Color p;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //get pixel value
                    p = Bmp.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    Value = (r + g + b) / 3;
                    if (Value > 128) { Value = 255; } else { Value = 0; }

                    //set new ARGB value in pixel
                    Bmp.SetPixel(x, y, Color.FromArgb(a, Value, Value, Value));
                }
            }

            return Bmp;
        }



        public static Bitmap mirrorFilter(Bitmap Bmp)
        {
            //get image dimension
            int width = Bmp.Width;
            int height = Bmp.Height;

            //Color of pixel
            //mirror image
            Bitmap mimg = new Bitmap(width * 2, height);


            for (int y = 0; y < height; y++)
            {
                for (int lx = 0, rx = width * 2 - 1; lx < width; lx++, rx--)
                {
                    //get source pixel value
                    Color p = Bmp.GetPixel(lx, y);

                    //set mirror pixel value
                    mimg.SetPixel(lx, y, p);
                    mimg.SetPixel(rx, y, p);
                }
            }

            return mimg;
        }

        public static Bitmap noiseFilter(Bitmap Bmp)
        {
            int Percetage = 15;
            int Min = 85;
            int Max = 255;
            float Brightness = 0;

            Random Random = new Random();
            Color rColor = new Color();
            Color p = new Color();
            int r, g, b;

            for (int x = 0; x < Bmp.Width; x++)
            {
                for (int y = 0; y < Bmp.Height; y++)
                {
                    if (Random.Next(1, 100) <= Percetage)
                    {
                        rColor = Color.FromArgb(Random.Next(Max, Max),
                                                Random.Next(Min, Max),
                                                Random.Next(Min, Max));

                        Brightness = Random.Next(Min, Max) / 100.0f;
                        p = Bmp.GetPixel(x, y);
                        r = (int)(p.R * Brightness);
                        g = (int)(p.G * Brightness);
                        b = (int)(p.B * Brightness);

                        if (r > 255) r = 255;
                        else if (r < 0) r = 0;

                        if (g > 255) g = 255;
                        else if (g < 0) g = 0;

                        if (b > 255) b = 255;
                        else if (b < 0) b = 0;
                    }
                    else
                    {
                        rColor = Bmp.GetPixel(x, y);
                    }

                    Bmp.SetPixel(x, y, rColor);
                }
            }
            return Bmp;
        }




    }
}
