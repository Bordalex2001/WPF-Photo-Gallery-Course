using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF_Photo_Gallery.Commands;
using WPF_Photo_Gallery.Models;

namespace WPF_Photo_Gallery.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty DateProperty;
        private static readonly DependencyProperty AuthorProperty;
        private static readonly DependencyProperty MarkProperty;
        private static readonly DependencyProperty ImagesProperty;
        private static readonly DependencyProperty IndexProperty;
        private static readonly DependencyProperty CurrentImageProperty;

        static MainWindowViewModel()
        {
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(MainWindowViewModel));
            DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(MainWindowViewModel));
            AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(MainWindowViewModel));
            MarkProperty = DependencyProperty.Register("Mark", typeof(int), typeof(MainWindowViewModel));
            ImagesProperty = DependencyProperty.Register("Images", typeof(ObservableCollection<Image>), typeof(MainWindowViewModel));
            IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(MainWindowViewModel));
            CurrentImageProperty = DependencyProperty.Register("CurrentImage", typeof(Image), typeof(MainWindowViewModel));
    }

        public MainWindowViewModel()
        {
            Images = new ObservableCollection<Image> 
            { 
                new Image { Name = "Photo1", Date = DateTime.Now, Author = "Author1", Mark = 1, Path = "Photos/Photo1.JPG" },
                new Image { Name = "Photo2", Date = DateTime.Now, Author = "Author2", Mark = 3, Path = "Photos/Photo2.JPG" },
                new Image { Name = "Photo3", Date = DateTime.Now, Author = "Author3", Mark = 5, Path = "Photos/Photo3.JPG" }
            };
            Index = 0;
            UpdateImage();
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        public string Mark
        {
            get { return (string)GetValue(MarkProperty); }
            set { SetValue(MarkProperty, value); }
        }

        public ObservableCollection<Image> Images
        {
            get { return (ObservableCollection<Image>)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); }
        }

        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        private static void OnCurrentIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = d as MainWindowViewModel;
        }

        public string CurrentImageName => CurrentImage?.Name;
        public DateTime CurrentImageDate => CurrentImage?.Date ?? DateTime.MinValue;
        public string CurrentImageAuthor => CurrentImage?.Author;
        public string CurrentImagePath => CurrentImage?.Path;

        public Image CurrentImage
        {
            get { return (Image)GetValue(CurrentImageProperty); }
            set { SetValue(CurrentImageProperty, value); }
        }

        public bool IsMark1
        {
            get { return CurrentImage?.Mark == 1; }
            set { if (value) SetMark(1); }
        }

        public bool IsMark2 
        {
            get { return CurrentImage?.Mark == 2; }
            set { if (value) SetMark(2); }
        }

        public bool IsMark3 
        { 
            get { return CurrentImage?.Mark == 3; }
            set { if (value) SetMark(3); }
        }

        public bool IsMark4 
        { 
            get { return CurrentImage?.Mark == 4; }
            set { if (value) SetMark(4); }
        }

        public bool IsMark5 
        { 
            get { return CurrentImage?.Mark == 5; }
            set { if (value) SetMark(5); }
        }

        private void SetMark(int mark)
        {
            if(CurrentImage != null)
            {
                CurrentImage.Mark = mark;
                OnPropertyChanged(nameof(IsMark1));
                OnPropertyChanged(nameof(IsMark2));
                OnPropertyChanged(nameof(IsMark3));
                OnPropertyChanged(nameof(IsMark4));
                OnPropertyChanged(nameof(IsMark5));
            }
        }

        private RelayCommand _showFirstCommand;

        public ICommand ShowFirstCommand
        {
            get
            {
                if(_showFirstCommand == null)
                {
                    _showFirstCommand = new RelayCommand(ShowFirst, CanShowFirst);
                }
                return _showFirstCommand;
            }
        }

        private bool CanShowFirst(object par)
        {
            return true;
        }

        private void ShowFirst(object par)
        {
            Index = 0;
        }

        private RelayCommand _showPreviousCommand;

        public ICommand ShowPreviousCommand
        {
            get
            {
                if(_showPreviousCommand == null)
                {
                    _showPreviousCommand = new RelayCommand(ShowPrevious, CanShowPrevious);
                }
                return _showPreviousCommand;
            }
        }

        private bool CanShowPrevious(object par)
        {
            return true;
        }

        private void ShowPrevious(object par)
        {
            if (Index > 0)
            {
                Index--;
            }
        }

        private RelayCommand _showNextCommand;

        public ICommand ShowNextCommand
        {
            get
            {
                if(_showNextCommand == null)
                {
                    _showNextCommand = new RelayCommand(ShowNext, CanShowNext);
                }
                return _showNextCommand;
            }
        }

        private bool CanShowNext(object par)
        {
            return true;
        }

        private void ShowNext(object par)
        {
            if (Index < Images.Count - 1)
            {
                Index++;
            }
        }

        private RelayCommand _showLastCommand;

        public ICommand ShowLastCommand
        {
            get
            {
                if(_showLastCommand == null)
                {
                    _showLastCommand = new RelayCommand(ShowLast, CanShowLast);
                }
                return _showLastCommand;
            }
        }

        private bool CanShowLast(object par)
        {
            return true;
        }
        
        private void ShowLast(object par)
        {
            Index = Images.Count - 1;
        }

        private void UpdateImage()
        {
            CurrentImage = Images[Index];
            OnPropertyChanged(nameof(CurrentImageName));
            OnPropertyChanged(nameof(CurrentImageDate));
            OnPropertyChanged(nameof(CurrentImageAuthor));
            OnPropertyChanged(nameof(CurrentImagePath));
        }
    }
}