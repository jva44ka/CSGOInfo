using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;

namespace WpfLawyersSystem.ViewModels
{
    public class MatchViewModel : BaseViewModel
    {
        /// <summary>
        /// Поля
        /// </summary>
        private ListOfTeams _listOfTeams;
        private Match _selectedMatch;
        private Match _newMatch;
        private string _searchText;

        // Для создания и удаления записи
        Team _team1;
        Team _team2;
        Team _teamWinner;
        DateTime _time;

        //Для анимации
        double _renderTransformX;
        double _pageWidth;

        private Page _matchListPage; //Страница списка
        private Page _matchInfoPage; //Страница инфы о матче
        private Page _currentPage; // Текущая страница

        /// <summary>
        /// ///// Свойства 
        /// </summary>

        public ListOfTeams ListOfTeams
        {
            get { return _listOfTeams; }
            set
            {
                _listOfTeams = value;
                OnPropertyChanged();
            }
        }
        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                OnPropertyChanged();
            }
        }
        public Match NewMatch
        {
            get { return _newMatch; }
            set
            {
                _newMatch = value;
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
                MatchesView.Filter = ((obj) =>
                {
                    if (obj is Match match)
                    {
                        if (match.Team1.Name.ToLower().Contains(SearchText.ToLower()) || 
                        match.Team2.Name.ToLower().Contains(SearchText.ToLower()) || 
                        match.Winner.Name.ToLower().Contains(SearchText.ToLower())) return true;
                    }
                    return false;
                });
                MatchesView.Refresh();
                base.OnPropertyChanged();
            }
        }

        public ICollectionView MatchesView { get; set; } // Свойство и автополе представление коллекции (для фильтрации)
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

        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
                base.OnPropertyChanged("NewMatch");
                base.OnPropertyChanged();
            }
        }
        public Team Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
                base.OnPropertyChanged();
            }
        }
        public Team TeamWinner
        {
            get { return _teamWinner; }
            set
            {
                _teamWinner = value;
                base.OnPropertyChanged();
            }
        }
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                base.OnPropertyChanged();
            }
        }

        /// <summary>
        /// // Конструкторы
        /// </summary>
        public MatchViewModel()
        {
            //_listOfMatches = FactoryOfLists.ObjMatches;
            _listOfTeams = FactoryOfLists.ObjTeams;
            MatchesView = CollectionViewSource.GetDefaultView(FactoryOfLists.ObjMatches.List);

            _matchListPage = new MatchPage();
            _matchListPage.DataContext = this;
            _matchInfoPage = new MatchInfoPage();
            _matchInfoPage.DataContext = this;
            CurrentPage = _matchListPage;

            _renderTransformX = 0.5;
            _pageWidth = 600;
        }

        //Метод
        public void OpenCreatingWindow()
        {
            //Team1 = new Team();
            //Team2 = new Team();
            //TeamWinner = new Team();
            Time = DateTime.Now;
            CreateMatchWindow newWindow = new Views.CreateMatchWindow();
            newWindow.DataContext = this;
            newWindow.ShowDialog();
        }

        //Комманды
        public ICommand bOpenCreatingWindow_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OpenCreatingWindow();
                    /*CreateMatchWindow newWindow = new Views.CreateMatchWindow();
                    NewMatch = new Match();
                    Team1 = NewMatch.Team1;
                    Team2 = NewMatch.Team2;
                    TeamWinner = NewMatch.Winner;
                    Time = NewMatch.Time;
                    newWindow.DataContext = this;
                    newWindow.ShowDialog();*/
                });
            }
        }

        public ICommand bAddMatch_Command
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    NewMatch = new Match(Team1, Team2, TeamWinner);
                    NewMatch.Time = Time;
                    FactoryOfLists.ObjMatches.List.Add(NewMatch);
                    NewMatch = null;
                    Team1 = null;
                    Team2 = null;
                    TeamWinner = null;
                    Time = DateTime.Now;
                    w.Close();
                    MatchesView.Refresh();
                });
            }
        }
        public ICommand bOpenSelectedItem_Command
        { //Переключает на страницу с изменением/ удалением информации о выбранном игроке
            get
            {
                return new DelegateCommand(() =>
                {
                    ChangeMatchWindow newWindow = new ChangeMatchWindow(SelectedMatch);
                    Team1 = this.SelectedMatch.Team1;
                    Team2 = this.SelectedMatch.Team2;
                    TeamWinner = this.SelectedMatch.Winner;
                    Time = this.SelectedMatch.Time;
                    newWindow.DataContext = this;
                    newWindow.ShowDialog();
                });
            }
        }

        public ICommand bInfoSelectedItem_Command
        {//Переключает на страницу с информацией о выбранном игроке
            get
            {
                return new DelegateCommand(() => {
                    if (SelectedMatch != null)
                    {
                        Team1 = SelectedMatch.Team1;
                        Team2 = SelectedMatch.Team2;
                        TeamWinner = SelectedMatch.Winner;
                        Time = SelectedMatch.Time;
                        ChangePageAnimated(_matchInfoPage);
                    }
                });
            }
        }

        public ICommand bBackToList_Command
        { // Переключает страницу со страницы информации о команде на страницу всего списка
            get
            {
                return new DelegateCommand<Window>((w) => {
                    if (w != null)
                    {
                        w.Close();
                    }
                    else
                    {
                        ChangePageAnimated(_matchListPage);
                    }
                });
            }
        }

        public ICommand bRemoveItem_Command
        { // Удаляет команду
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    if (SelectedMatch != null)
                    {
                        FactoryOfLists.ObjMatches.List.Remove(SelectedMatch);
                        SelectedMatch = null;
                        if (w != null)
                        {
                            w.Close();
                            ChangePageAnimated(_matchListPage);
                        }

                        else
                        {
                            ChangePageAnimated(_matchListPage);
                        }
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
