using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Movies.Model;

namespace Movies.ViewModels
{
    public class ItemsViewModel
    {
        public ItemsViewModel()
        {
            Items = new List<ItemDto> { new ItemDto { Name = "aaaaa" }, new ItemDto { Name = "bbbbb" } };
            NewSeasonCommand = new DelegateCommand<ItemDto>(
                item =>
                {
                    item.Season++;
                    item.Episode = 0;
                });
        }

        public ICommand NewSeasonCommand { get; private set; }

        public IEnumerable<ItemDto> Items { get; set; }
    }
}