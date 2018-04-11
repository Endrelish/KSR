namespace KSR1.Model
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using KSR1.Annotations;

    public class Statistics : INotifyPropertyChanged
    {
        private TimeSpan computingTime;

        private double efficiency;

        private int testDocuments;

        private int trainingDocuments;

        public TimeSpan ComputingTime
        {
            get
            {
                return this.computingTime;
            }
            set
            {
                if (value.Equals(this.computingTime)) return;
                this.computingTime = value;
                this.OnPropertyChanged();
            }
        }

        public double Efficiency
        {
            get
            {
                return this.efficiency;
            }
            set
            {
                if (value.Equals(this.efficiency)) return;
                this.efficiency = value;
                this.OnPropertyChanged();
            }
        }

        public int TestDocuments
        {
            get
            {
                return this.testDocuments;
            }
            set
            {
                if (value == this.testDocuments) return;
                this.testDocuments = value;
                this.OnPropertyChanged();
            }
        }

        public int TrainingDocuments
        {
            get
            {
                return this.trainingDocuments;
            }
            set
            {
                if (value == this.trainingDocuments) return;
                this.trainingDocuments = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}