using Microsoft.Practices.Prism.ViewModel;

namespace Movies.Model
{
    public class ItemDto : NotificationObject
    {
        private int _episode;
        private int _season;

        public int Episode
        {
            get { return _episode; }
            set
            {
                if (value == _episode)
                {
                    return;
                }
                _episode = value;
                RaisePropertyChanged(() => Episode);
            }
        }

        public string Name { get; set; }

        public int Season
        {
            get { return _season; }
            set
            {
                if (value == _season)
                {
                    return;
                }
                _season = value;
                RaisePropertyChanged(() => Season);
            }
        }
    }
}