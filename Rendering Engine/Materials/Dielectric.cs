﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rendering_Engine.Utilities;

namespace Rendering_Engine.Materials
{
    public class Dielectric : Material
    {
        public double IOR { get; }
        private static Random random = new Random();
        public Dielectric(double indexOfRefraction)
        {
            this.IOR = indexOfRefraction;
        }

        public override Ray Scatter(Ray ray, HitRecord record, ref Color3 attenuation)
        {
            attenuation = new Color3(1.0, 1.0, 1.0);
            double ri = record.IsFrontFace ? (1.0 / this.IOR) : this.IOR;

            Vector3 unit = Vector3.UnitVector(ray.Direction);

            double cosTheta = Math.Min(Vector3.Dot(-unit, record.Normal), 1.0);
            double sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

            bool cannotRefract = ri * sinTheta > 1.0;

            Vector3 direction;

            if (cannotRefract || SchlickApprox(cosTheta, ri) > random.NextDouble())
            {
                direction = Vector3.Reflect(unit, record.Normal);
            }
            else
            {
                direction = Vector3.Refract(unit, record.Normal, ri);
            }

            return new Ray(record.Location, direction);
        }

        public static double SchlickApprox(double cos, double IOR)
        {
            double r0 = (1 - IOR) / (1 + IOR);
            r0 = r0 * r0;

            return r0 + (1-r0)*Math.Pow((1- cos), 5);
        }
    }
}
