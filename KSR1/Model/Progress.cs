namespace KSR1.Model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using KSR1.Annotations;

    public class Progress : INotifyPropertyChanged
    {
        private int processed;

        private int all;

        public Progress(int all)
        {
            this.Reset(all);
        }

        public int Processed
        {
            get
            {
                return this.processed;
            }
            set
            {
                if (value == this.processed) return;
                this.processed = value;
                this.OnPropertyChanged();
            }
        }

        public int All
        {
            get
            {
                return this.all;
            }
            set
            {
                if (value == this.all) return;
                this.all = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Reset(int all)
        {
            this.Processed = 0;
            this.All = all;
        }
    }
}