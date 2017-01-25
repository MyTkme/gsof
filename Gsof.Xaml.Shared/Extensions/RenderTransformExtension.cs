﻿using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gsof.Xaml.Extensions
{
    public static class RenderTransformExtension
    {
        public static T GetRenderTransform<T>(this FrameworkElement p_element) where T : Transform, new()
        {
            var element = p_element;

            T t;
            do
            {
                t = element.RenderTransform as T;
                if (t != null)
                {
                    break;
                }

                var group = element.RenderTransform as TransformGroup;
                if (group != null)
                {
                    t = group.Children.FirstOrDefault(i => i is T) as T;
                    break;
                }

                t = new T();
                var tg = new TransformGroup();
                tg.Children.Add(t);
                element.RenderTransform = tg;

            } while (false);

            return t;
        }

        public static void CreateRenderTransform(this FrameworkElement p_element)
        {
            var element = p_element;
            if (element == null)
            {
                return;
            }

            if (element.RenderTransform is TransformGroup)
            {
                return;
            }

            element.RenderTransform = new TransformGroup()
            {
                Children = new TransformCollection()
                        {
                            new ScaleTransform(),
                            new SkewTransform(),
                            new RotateTransform(),
                            new TranslateTransform(),
                        }
            };
        }
    }
}
