using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;
using DevExpress.Xpf.Map;

namespace MapCustomElementSettings {
    public partial class MainWindow : Window {
        const string filepath = "..//..//disasters.xml";

        List<Disaster> disasters;
        public List<Disaster> Disasters { get { return disasters; } }

        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            disasters = DeserializeXml(filepath);
            this.DataContext = this;
        }

        List<Disaster> DeserializeXml(string filepath) {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Disaster>));
            XmlReader reader = new XmlTextReader(filepath);
            List<Disaster> data = (List<Disaster>)serializer.Deserialize(reader);
            reader.Close();

            return data;
        }
    }

    class MarkerTemplateSelector : DataTemplateSelector {
        public DataTemplate AirRaidsDataTemplate { get; set; }
        public DataTemplate BushfiresDataTemplate { get; set; }
        public DataTemplate CycloneDataTemplate { get; set; }
        public DataTemplate SinkingDataTemplate { get; set; }
        public DataTemplate EpidemicDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            Disaster disaster = item as Disaster;
            if (disaster == null) return base.SelectTemplate(item, container);

            switch (disaster.Type) {
                case "Air Raids": return AirRaidsDataTemplate;
                case "Bushfires": return BushfiresDataTemplate;
                case "Cyclone": return CycloneDataTemplate;
                case "Sinking": return SinkingDataTemplate;
                case "Epidemic": return EpidemicDataTemplate;
                default: return base.SelectTemplate(item, container);
            }
        }
    }

    public class Disaster {
        public string Type { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public string LocationName { get; set; }
        public int Deaths { get; set; }
    }
}
