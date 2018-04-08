namespace KSR1.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows.Input;

    using KSR1.Annotations;
    using KSR1.Model;
    using KSR1.View;

    public class MainViewModel : INotifyPropertyChanged
    {
        private ICommand browseCommand;

        private IExtractor chosenExtractor;

        private IMetric chosenMetric;

        private readonly Dictionary<string, IExtractor> extractors;

        private string file;

        private bool isProcessing;

        private readonly Dictionary<string, IMetric> metrics;

        private string processingText;

        private List<ReutersMetricObject> reuters;

        private readonly Timer timer;

        private int timerIndex;

        private int trainingRatio;

        public MainViewModel()
        {
            this.trainingRatio = 60;
            this.metrics = new Dictionary<string, IMetric>();
            this.metrics.Add("Chebyshev metric", new ChebyshevMetric());
            this.metrics.Add("Euclidean metric", new EuclideanMetric());
            this.metrics.Add("Manhattan metric", new ManhattanMetric());

            this.extractors = new Dictionary<string, IExtractor>();
            this.extractors.Add("TF extractor", new TfExtractor());
            this.extractors.Add("TF-IDF extractor", new TfidfExtractor());

            this.reuters = new List<ReutersMetricObject>();
            this.timer = new Timer(500);
            this.timer.Elapsed += (sender, e) => this.HandleTimer();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BrowseCommand
        {
            get
            {
                return this.browseCommand ?? (this.browseCommand = new Command(() => this.Browse(), true));
            }
        }

        public string ChosenExtractor
        {
            set
            {
                this.chosenExtractor = this.extractors.First(m => m.Key == value).Value;
            }
        }

        public string ChosenMetric
        {
            set
            {
                this.chosenMetric = this.metrics.First(m => m.Key == value).Value;
            }
        }

        public IEnumerable<string> Extractors
        {
            get
            {
                return this.extractors.Select(e => e.Key);
            }
        }

        public bool IsProcessing
        {
            get => this.isProcessing;
            set
            {
                if (value == this.isProcessing)
                {
                    return;
                }

                this.isProcessing = value;
                if (this.isProcessing)
                {
                    this.timer.Start();
                }
                else
                {
                    this.timer.Stop();
                    this.ProcessingText = string.Empty;
                }

                this.OnPropertyChanged();
            }
        }

        public IEnumerable<string> Metrics
        {
            get
            {
                return this.metrics.Select(m => m.Key);
            }
        }

        public string ProcessingText
        {
            get => this.processingText;
            set
            {
                if (value == this.processingText)
                {
                    return;
                }

                this.processingText = value;
                this.OnPropertyChanged();
            }
        }

        public int TrainingRatio
        {
            get => this.trainingRatio;
            set
            {
                if (value == this.trainingRatio)
                {
                    return;
                }

                this.trainingRatio = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Browse()
        {
            
            await Task.Run(() => this.ProcessAsync());
            this.IsProcessing = true;
        }

        private void HandleTimer()
        {
            string[] tab =
                { "Processing.    ", "Processing..   ", "Processing...  ", "Processing.... ", "Processing....." };
            this.ProcessingText = tab[this.timerIndex];
            this.timerIndex = (this.timerIndex + 1) % 5;
        }

        private async void Process()
        {
            Task.Delay(5000);
        }

        private async void ProcessAsync()
        {
            var dialog = new WpfFileDialog();
            var files = dialog.GetOpenFilePath("Sgm files (*.sgm)|*.sgm|Ksr files (*.ksr)|*.ksr|All files (*.*)|*.*");

            foreach (var s in files)
            {
                this.reuters.AddRange(ReutersReader.ReadReuters(s));
            }
            await Task.Run(() => Process());
            IsProcessing = false;
        }
    }
}