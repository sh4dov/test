using System.Windows;
using System.Windows.Input;

namespace Movies.Views
{
    public partial class ItemView
    {
        public static readonly DependencyProperty NewSeasonCommandProperty = DependencyProperty.Register("NewSeasonCommand", typeof(ICommand), typeof(ItemView), new PropertyMetadata(default(ICommand)));

        public ItemView()
        {
            InitializeComponent();
        }

        public ICommand NewSeasonCommand
        {
            get { return (ICommand)GetValue(NewSeasonCommandProperty); }
            set { SetValue(NewSeasonCommandProperty, value); }
        }
    }
}