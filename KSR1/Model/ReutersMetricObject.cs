namespace KSR1.Model
{
    using System;

    public class ReutersMetricObject : IMetricObject
    {
        public ReutersMetricObject(string place, string title, string dateline, string body)
        {
            this.Place = place;
            this.Dateline = dateline;
            this.Title = title;
            this.Body = body;
        }
        public double Resemblance(object other)
        {
            if (other is ReutersMetricObject obj)
            {
                // TODO Porownanie wszystkich cech i zwrocenie jakiejs miary podobienstwa
                throw new NotImplementedException();
            }
            else
            {
                throw new ArgumentException(); // TODO 
            }
        }

        public string Place { get; private set; }
        public string Dateline { get; private set; }

        public string Title { get; private set; }

        public string Body { get; private set; }
    }
}