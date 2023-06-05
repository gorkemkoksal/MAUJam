using UnityEngine;
using UnityEngine.UI;

namespace Core._Common.Extensions
{
    public static class ImageExtensions
    {
        public static void SetColor(this Image img, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            img.color =
                new Color(r ?? img.color.r, g ?? img.color.g, b ?? img.color.b, a ?? img.color.a);
        }
    }
}