using System;
using BlindSocial.Helpers;
using System.Collections.Generic;

namespace BlindSocial.Models
{
    public class Category
    {
        public string Name { get; set; }
        public double Score { get; set; }
    }

    public class Caption
    {
        public string Text { get; set; }
        public double Confidence { get; set; }
    }

    public class Description
    {
        public Description()
        {
            Tags = new List<string>();
            Captions = new List<Caption>();
        }

        public List<string> Tags { get; set; }
        public List<Caption> Captions { get; set; }
    }

    public class Metadata
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
    }

    public class Color
    {
        public Color()
        {
            DominantColors = new List<string>();
        }

        public string DominantColorForeground { get; set; }
        public string DominantColorBackground { get; set; }
        public List<string> DominantColors { get; set; }
        public string AccentColor { get; set; }
        public bool IsBWImg { get; set; }
    }

    public class RootObject
    {
        public RootObject()
        {
            Categories = new List<Category>();
            Description = new Description();
            Metadata = new Metadata();
            this.Color = new Color();
        }

        public List<Category> Categories { get; set; }
        public Description Description { get; set; }
        public string RequestId { get; set; }
        public Metadata Metadata { get; set; }
        public Color Color { get; set; }
    }
}
