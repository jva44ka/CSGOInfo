using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Mvvm;

using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CommonServiceLocator;
using System.Windows.Data;

namespace WpfLawyersSystem.ViewModels
{
    public class PlayerViewModel : NotifycationsPropertyChanged
    {
        private ListOfPlayers _players; // поле для передачи в View
        private Player _selectedPlayer; // для передачи выбранного item в OnePlayerViewmodel
        private string _searchText;
        //private ICollectionView _listView;
        public ListOfPlayers Players
        {
            get { return _players; }
            set
            {
                _players = value;
                base.OnPropertyChanged();
            }
        }
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                PlayersView.Filter = ((obj) =>
                {
                    if (obj is Player player)
                    {
                        return player.Name.ToLower().Contains(SearchText.ToLower());
                    }
                    return false;
                });
                Players.Redresh();
                base.OnPropertyChanged();
            }
        }
        public ICollectionView PlayersView { get; set; } // Свойство и автополе представление коллекции (для фильтрации)

        public PlayerViewModel()
        {
            Players = FactoryOfLists.ObjPlayers;
            PlayersView = CollectionViewSource.GetDefaultView(Players.List);
            //_listView = FactoryOfLists.ObjPlayers.List;
            //_players = FactoryOfLists.ObjPlayers; было раньше
        }

        public ICommand bOpenSelectedItem_Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ChangePlayerWindow newWindow = new Views.ChangePlayerWindow(SelectedPlayer);
                    newWindow.ShowDialog();
                });
            }
        }
    }
}
