using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Clases
{
    public class CircularProgressDrawable : IDrawable
    {
        private readonly float _progress;

        public CircularProgressDrawable(float progress)
        {
            _progress = progress;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float centerX = (float)(dirtyRect.Width / 2);
            float centerY = (float)(dirtyRect.Height / 2);
            float radius = Math.Min(centerX, centerY) - 10; // Margen para el círculo

            // Progreso circular
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 10;
            canvas.DrawCircle(centerX, centerY, radius);


            // Fondo del círculo
            //canvas.StrokeColor = Colors.LightGray;
            //canvas.StrokeSize = 10;
            //canvas.DrawArc(centerX - radius, centerY - radius, centerX + radius, centerY + radius, 90, 360 * _progress, true, false);
        }
    }
}