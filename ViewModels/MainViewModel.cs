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
using LightMessageBus;

using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using DevExpress.Mvvm;
using CommonServiceLocator;

namespace WpfLawyersSystem.ViewModels
{
    class MainViewModel : NotifycationsPropertyChanged
    {
        //страницы (таблицы из бд)
        private Page _matchMV;
        private Page _playerMV;
        private Page _teamMV;
        private Page _top10MV;
        private Page _tournamentMV;

        //Прочие поля
        private string _searchText; //Поле для ввода поискового запроса
        private Page _curretPage; //Активная страница
        private double _frameOpacity; // Прозрачность для анимации
        private string _qwerryLabel; //

        //их свойства
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_curretPage == _playerMV)
                {
                    playerViewModelInstaince.SearchText = value;
                }
                if (_curretPage == _teamMV)
                {
                    teamViewModelInstaince.SearchText = value;
                }
                if (_curretPage == _matchMV)
                {
                    matchViewModelInstaince.SearchText = value;
                }
                if (_curretPage == _top10MV)
                {

                }
                if (_curretPage == _tournamentMV)
                {

                }
                _searchText = value;
                //playerViewModelInstaince.SearchText = value;
                base.OnPropertyChanged();
            }
        }
        public string QwerryLabel
        {
            get
            {
                return _qwerryLabel;
            }
            set
            {
                _qwerryLabel = value;
                OnPropertyChanged();
            }
        }
        public Page CurretPage
        {
            get
            {
                return _curretPage;
            }
            set
            {
                _curretPage = value;
                base.OnPropertyChanged();
            }
        }
        public double FrameOpacity
        {
            get
            {
                return _frameOpacity;
            }
            set
            {
                _frameOpacity = value;
                base.OnPropertyChanged();
            }
        }

        //Поля для передачи ссылки vm для изменения запроса
        PlayerViewModel playerViewModelInstaince;
        TeamViewModel teamViewModelInstaince;
        MatchViewModel matchViewModelInstaince;
        TournamentsViewModel tournamentsViewModelInstaince;

        public MainViewModel()
        {//Конструктор
            _matchMV = new Views.MatchPage(out matchViewModelInstaince);
            _playerMV = new Views.PlayerPage(out playerViewModelInstaince);
            _teamMV = new Views.TeamPage(out teamViewModelInstaince);
            _top10MV = new Views.Top10Page();
            _tournamentMV = new Views.TournamentPage(out tournamentsViewModelInstaince);
            _frameOpacity = 1;
            _curretPage = _playerMV;
            _qwerryLabel = "Введите имя игрока";

        } //Конструктор

        ///Команды кнопок меню
        public ICommand bMenuMatch_Command
        {
            get
            {
                return new RelayCommand(() => ChangeAnimated(_matchMV, "Введите название одной из команд"));
            }
        }
        public ICommand bMenuPlayer_Command
        {
            get
            {
                return new RelayCommand(() => ChangeAnimated(_playerMV, "Введите имя игрока"));
            }
        }
        public ICommand bMenuTeam_Command
        {
            get
            {
                return new RelayCommand(() => ChangeAnimated(_teamMV, "Введите название команды"));
            }
        }
        public ICommand bMenuTop10_Command
        {
            get
            {
                return new RelayCommand(() => ChangeAnimated(_top10MV, "Введите название команды"));
            }
        }
        public ICommand bMenuTournament_Command
        {
            get
            {
                return new RelayCommand(() => ChangeAnimated(_tournamentMV, "Введите название турнира"));
            }
        }

        public ICommand bAddRecord_Click
        { // Выбирает окно для создания item в ListView
            get
            {
                return new RelayCommand(() =>
                {
                    if (_curretPage == _playerMV)
                    {
                        CreatePlayerWindow newWindow = new Views.CreatePlayerWindow();
                        newWindow.ShowDialog();
                    }
                    if (_curretPage == _teamMV)
                    {
                        CreateTeamWindow newWindow = new CreateTeamWindow();
                        newWindow.ShowDialog();
                    }
                });
            }
        }

        /*public ICommand TBChangingText_Command
        { // Выбирает окно для создания item в ListView
            get
            {
                return new RelayCommand(() =>
                {
                    if (_curretPage == _playerMV)
                    {
                        Views.CreatePlayerWindow newWindow = new Views.CreatePlayerWindow();
                        newWindow.ShowDialog();
                    }
                    if (_curretPage == _teamMV)
                    {
                        Views.CreateTeamWindow newWindow = new Views.CreateTeamWindow();
                        newWindow.ShowDialog();
                    }
                    if (_curretPage == _matchMV)
                    {
                        
                    }
                    if (_curretPage == _top10MV)
                    {
                        
                    }
                    if (_curretPage == _tournamentMV)
                    {
                        
                    }
                });
            }
        }*/

        private async void ChangeAnimated(Page page, string QwerryParam) // Анимация и смена страницы
        {
            await Task.Factory.StartNew(() =>
            {
                if (CurretPage != page)
                {
                    QwerryLabel = QwerryParam;
                    for (double i = 1.0; i <= 0.0; i -= 0.1)
                    {
                        FrameOpacity = i;
                        Thread.Sleep(20);
                    }
                    CurretPage = page;
                    for (double i = 0.0; i <= 1.0; i += 0.1)
                    {
                        FrameOpacity = i;
                        Thread.Sleep(20);
                    }
                }
            });
        }
    }
}
