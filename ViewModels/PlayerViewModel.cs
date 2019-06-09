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
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Mvvm;

using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;
using CommonServiceLocator;
using System.Windows.Data;
using System.Windows;

namespace WpfLawyersSystem.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private ListOfPlayers _players; // поле для передачи в View
        private Player _selectedPlayer; // для передачи выбранного item в OnePlayerViewmodel
        private string _searchText; // для поиска по имени

        //Для анимации
        double _renderTransformX;
        double _pageWidth;

        private Page _playersListPage; //Страница списка
        private Page _playerInfoPage; //Страница инфы об игроке
        private Page _currentPage; // Текущая страница

        //Свойства
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
                PlayersView.Refresh();
                base.OnPropertyChanged();
            }
        }
        public ICollectionView PlayersView { get; set; } // Свойство и автополе представление коллекции (для фильтрации)
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                base.OnPropertyChanged();
            }
        }
        public double PageWidth
        {
            get { return _pageWidth; }
            set
            {
                _pageWidth = value;
                OnPropertyChanged();
            }
        }
        public double RenderTransformX
        {
            get { return _renderTransformX; }
            set
            {
                _renderTransformX = value;
                base.OnPropertyChanged();
            }
        }

        //Конструктор
        public PlayerViewModel()
        {
            Players = FactoryOfLists.ObjPlayers;
            PlayersView = CollectionViewSource.GetDefaultView(Players.List);

            _playersListPage = new PlayerPage();
            _playersListPage.DataContext = this;
            _playerInfoPage = new PlayerInfoPage();
            _playerInfoPage.DataContext = this;
            CurrentPage = _playersListPage;

            _renderTransformX = 0.5;
            _pageWidth = 600;
        }

        //Комманды
        public ICommand bOpenSelectedItem_Command
        { //Переключает на страницу с изменением/ удалением информации о выбранном игроке
            get
            {
                return new DelegateCommand(() =>
                {
                    ChangePlayerWindow newWindow = new Views.ChangePlayerWindow(SelectedPlayer);
                    newWindow.ShowDialog();
                });
            }
        }

        public ICommand bInfoSelectedPlayer_Command
        {//Переключает на страницу с информацией о выбранном игроке
            get
            {
                return new DelegateCommand(() => { ChangePageAnimated(_playerInfoPage); });
            }
        }

        public ICommand bBackToList_Command
        { // Переключает страницу со страницы информации о команде на страницу всего списка
            get
            {
                return new DelegateCommand(() => { ChangePageAnimated(_playersListPage); });
            }
        }

        public ICommand bRemovePlayer_Command
        { // Удаляет команду
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedPlayer != null)
                    {
                        SelectedPlayer.Team.TheCrew.Remove(_selectedPlayer);
                        FactoryOfLists.ObjPlayers.List.Remove(SelectedPlayer);
                        ChangePageAnimated(_playersListPage);
                    }
                });
            }
        }

        public async void ChangePageAnimated(Page page)
        { // Сам метод переключения страницы. Нужно сделать анимацию слайда влево-вправо
            if (true)
            {
                await Task.Factory.StartNew(() =>
                {
                    for (double i = 0; i <= PageWidth; i += (PageWidth/60))
                    {
                        RenderTransformX = i;
                        Thread.Sleep(1);
                    }
                    CurrentPage = page;
                    for (double i = RenderTransformX; i >= 0; i -= (PageWidth / 60))
                    {
                        RenderTransformX = i;
                        Thread.Sleep(1);
                    }
                });
            }
        }
    }
}
