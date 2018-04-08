namespace KSR1.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KSR1.Properties;

    public class ReutersMetricObject
    {
        public ReutersMetricObject(string place, string title, string dateline, string body)
        {
            this.Place = place.ToUpper();
            this.Dateline = dateline.ToUpper();
            this.Title = title.ToUpper();
            this.Body = body.ToUpper();
        }

        public string Property
        {
            get
            {
                switch ((Property)Settings.Default.Property)
                {
                    case KSR1.Property.Places:
                        return this.Place;
                    default:
                        return Place;
                }
            }
        }

        public Property PropertyType
        {
            set
            {
                Settings.Default.Property = (uint)value;
            }
        }

        public Dictionary<string, double> Vector { get; set; }

        public string Body { get; }

        public string Dateline { get; }

        public string Place { get; }

        public List<string> SeparatedBody { get; private set; }

        public string Title { get; }

        public void SeparateBody()
        {
            char[] separators = { ' ', '\n', '\t' };
            this.SeparatedBody = this.Body.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !s.Contains("REUTER")).ToList();
        }
    }
}