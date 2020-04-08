using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingAppWPF.ViewModel
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //ObservableCollection<Product> _Products;

        //public ObservableCollection<Product> Products
        //{
        //    get { return _Products; }
        //    set { _Products = value; }
        //}

        //DataAccess objDs;


        //ObservableCollection<string> _Properties;

        //public ObservableCollection<string> Properties
        //{
        //    get { return _Properties; }
        //    set { _Properties = value; }
        //}

        //void LoadProperties()
        //{
        //    Properties.Add("ProductName");
        //    Properties.Add("Manufacturer");
        //    Properties.Add("DealerName");
        //}

        public FilterViewModel()
        {
            //Products = new ObservableCollection<Product>();
            //Properties = new ObservableCollection<string>();
            //objDs = new DataAccess();
            //LoadProperties();
            //parameter action will be richtextbox edition-coloring a letter
            FilterCommand = new RelayCommand(Get);
        }

        public RelayCommand FilterCommand { get; set; }

        string _Criteria;

        public string Criteria
        {
            get { return _Criteria; }
            set
            {
                _Criteria = value;
                OnPropertyChanged("Criteria");
            }
        }
        //Lesson text written by user
        string _typping;
        public string Typping
        {
            get => _typping;
            set { _typping = value; OnPropertyChanged("Typping"); }
        }

        //Lesson text colored
        string _lessonText;
        public string LessonText
        {
            get => _lessonText;
            set { _lessonText = value; OnPropertyChanged("LessonText"); }
        }

        string _Filter;
        //this input textbox,  Text="{Binding Filter,UpdateSourceTrigger=PropertyChanged} ... text property
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;
                OnPropertyChanged("Filter");
            }
        }


        //in this method will be defined execution of the text which will be displayed in the richtextbox
        void Get()
        {
            //Products.Clear();
            //foreach (var item in objDs.GetProducts(Criteria, Filter))
            //{
            //    Products.Add(item);
            //}
            LessonText = Filter;
        }

        void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pName));
            }
        }
    }
}
