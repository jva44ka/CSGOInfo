using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;
using DevExpress.Mvvm;
using System.Threading;

namespace WpfLawyersSystem.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        private ListOfTeams _teams;
        private Team _selectedTeam;
        private string _searchText;

        Page _teamsListPage;
        Page _teamInfoPage;
        Page _currentPage;

        double _pageWidth;
        double _renderTransformX;

        public ListOfTeams Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                base.OnPropertyChanged();
            }
        }
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                TeamsView.Filter = ((obj) =>
                {
                    if (obj is Team team)
                    {
                        return team.Name.ToLower().Contains(SearchText.ToLower());
                    }
                    return false;
                });
                TeamsView.Refresh();
                OnPropertyChanged();
            }
        }
        public ICollectionView TeamsView { get; set; } // Свойство и автополе представление коллекции (для фильтрации)
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
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

        public TeamViewModel() // ctor
        {
            _teams = FactoryOfLists.ObjTeams;
            TeamsView = CollectionViewSource.GetDefaultView(Teams.List);

            _teamsListPage = new TeamPage();
            _teamsListPage.DataContext = this;
            _teamInfoPage = new TeamInfoPage();
            _teamInfoPage.DataContext = this;
            CurrentPage = _teamsListPage;

            _renderTransformX = 0.5;
            _pageWidth = 600;
        }

        public ICommand bOpenSelectedItem_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Views.ChangeTeamWindow newWindow = new Views.ChangeTeamWindow(SelectedTeam);
                    newWindow.ShowDialog();
                });
            }
        }

        public ICommand bInfoSelectedPlayer_Command
        {//Переключает на страницу с информацией о выбранном игроке
            get
            {
                return new DelegateCommand(() => { ChangePageAnimated(_teamInfoPage); });
            }
        }

        public ICommand bBackToList_Command
        { // Переключает страницу со страницы информации об игроке на страницу всего списка
            get
            {
                return new DelegateCommand(() => { ChangePageAnimated(_teamsListPage); });
            }
        }

        public ICommand bRemoveItem_Command
        { // Удаляет игрока
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedTeam.TheCrew != null)
                    {
                        foreach (var item in SelectedTeam.TheCrew)
                        {
                            if (item != null)
                            {
                                item.Team = null;
                            }
                        }
                    }
                    FactoryOfLists.ObjTeams.List.Remove(SelectedTeam);
                    ChangePageAnimated(_teamsListPage);
                });
            }
        }

        async void ChangePageAnimated(Page page)
        {
            if (true)
            {
                await Task.Factory.StartNew(() =>
                {

                    for (double i = 0; i <= PageWidth; i += (PageWidth / 60))
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
