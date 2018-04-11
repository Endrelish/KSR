namespace KSR1.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using KSR1.Annotations;
    using KSR1.Model;
    using KSR1.View;

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, IExtractor> extractors;

        private readonly Dictionary<string, IMetric> metrics;

        private readonly List<ReutersMetricObject> reuters;

        private IExtractor chosenExtractor;

        private IMetric chosenMetric;

        private double efficiencyRatio;

        private string file;

        private bool isProcessing;

        private Command processCommand;

        private string processingText;

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
            this.ProcessingProgress = new Progress(1);
            this.Stats = new Statistics();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ChosenExtractor
        {
            set
            {
                this.chosenExtractor = this.extractors.First(m => m.Key == value).Value;
                this.OnPropertyChanged(nameof(this.ProcessingAvailable));
                this.ProcessCommand.OnCanExecuteChanged();
            }
        }

        public string ChosenMetric
        {
            set
            {
                this.chosenMetric = this.metrics.First(m => m.Key == value).Value;
                this.OnPropertyChanged(nameof(this.ProcessingAvailable));
                this.ProcessCommand.OnCanExecuteChanged();
            }
        }

        public double EfficiencyRatio
        {
            get => this.efficiencyRatio;
            set
            {
                if (value.Equals(this.efficiencyRatio))
                {
                    return;
                }

                this.efficiencyRatio = value;
                this.OnPropertyChanged();
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

        public Command ProcessCommand
        {
            get
            {
                return this.processCommand ?? (this.processCommand = new Command(
                                                   () => this.Process(),
                                                   () => this.ProcessingAvailable));
            }
        }

        public bool ProcessingAvailable => this.chosenMetric != null && this.chosenExtractor != null;

        public Progress ProcessingProgress { get; set; }

        public Statistics Stats { get; }

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

        private async void Process()
        {
            this.IsProcessing = true;
            var processing = Task.Run(() => this.Processing());
            await processing;

            this.Stats.Efficiency = processing.Result;
            this.IsProcessing = false;
        }

        private double Processing()
        {
            var dialog = new WpfFileDialog();
            var files = dialog.GetOpenFilePath("Sgm files (*.sgm)|*.sgm|Ksr files (*.ksr)|*.ksr|All files (*.*)|*.*");
            this.reuters.Clear();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var s in files)
            {
                this.reuters.AddRange(s.Contains(".ksr") ? KsrReader.ReadReuters(s) : ReutersReader.ReadReuters(s));
            }

            this.ProcessingProgress.Reset(
                this.reuters.Count + (int)(this.reuters.Count * ((100 - this.TrainingRatio) / 10.0)));

            var t1 = Task.Run(
                () => this.chosenExtractor.FeatureVector(
                    this.reuters.Take(this.reuters.Count / 3),
                    this.ProcessingProgress));
            var t2 = Task.Run(
                () => this.chosenExtractor.FeatureVector(
                    this.reuters.Skip(this.reuters.Count / 3).Take(this.reuters.Count / 3),
                    this.ProcessingProgress));
            var t3 = Task.Run(
                () => this.chosenExtractor.FeatureVector(
                    this.reuters.Skip(this.reuters.Count / 3 * 2),
                    this.ProcessingProgress));
            //this.chosenExtractor.FeatureVector(this.reuters, this.ProcessingProgress);

            t1.Wait();
            t2.Wait();
            t3.Wait();
            var training = this.reuters.Take(this.reuters.Count * this.TrainingRatio / 100);
            var test = this.reuters.Skip(this.reuters.Count * this.TrainingRatio / 100);

            this.Stats.TrainingDocuments = training.Count();
            this.Stats.TestDocuments = test.Count();

            var classified = 0;

            foreach (var o in test)
            {
                var category = Knn.Classify(o, training, this.chosenMetric);
                if (category.Equals(o.Property))
                {
                    classified++;
                }

                this.ProcessingProgress.Processed += 10;
            }

            stopwatch.Stop();
            this.Stats.ComputingTime = stopwatch.Elapsed;

            return (double)classified / test.Count();
        }
    }
}