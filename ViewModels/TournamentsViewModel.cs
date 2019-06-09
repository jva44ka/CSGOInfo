using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfLawyersSystem.ViewModels
{
    public class TournamentsViewModel : BaseViewModel
    {
        //Общие поля списка, выбранного или нового турнира и запроса
        private ListOfTournaments _items; // items в ListView
        private Tournament _selectedItem;
        private Tournament _newTournament;
        private string _searchText;

        // Для создания и удаления ОДНОЙ записи
        string _name;
        DateTime _tournamentTime;
        Player _mvp;
        ObservableCollection<Match> _itemMatches;

        // Для создания итема в коллекцию _matches конкретного item (tournament)
        ObservableCollection<Player> _players; // Для комбобокса
        Match _newMatch;
        Match _selectedMatch; //
        Team _team1;
        Team _team2;
        Team _teamWinner;
        DateTime _time;

        // Для комбобокса при создании добавлении уже существующего матча
        ObservableCollection<Match> _matches;
        // Для комбобокса при создании / редактировании матча в турнире
        ListOfTeams _listOfTeams;

        //Для анимации
        double _renderTransformX;
        double _pageWidth;

        //Для смены страницы
        private Page _tournamentsListPage; //Страница списка
        private Page _tournamentInfoPage; //Страница инфы о турнире
        private Page _createTournamentPage; //Страница создания турнира
        private Page _tournamentChangePage; // Страница изменения турнира
        private Page _currentPage; // Текущая страница

        /// <summary>
        /// ///// Свойства 
        /// </summary>


        public ListOfTournaments Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        public Tournament SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        public Tournament NewItem
        {
            get { return _newTournament; }
            set
            {
                _newTournament = value;
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
                TournamentsView.Filter = ((obj) =>
                {
                    if (obj is Tournament tournament)
                    {
                        if (/*(tournament.Matches.ToList().Find(match => match.Team1.Name.ToLower() == _searchText.ToLower()) != null) ||
                        (tournament.Matches.ToList().Find(match => match.Team2.Name.ToLower() == _searchText.ToLower()) != null) ||
                        (tournament.Matches.ToList().Find(match => match.Winner.Name.ToLower() == _searchText.ToLower()) != null) ||*/
                        tournament.Name.ToLower().Contains(_searchText.ToLower()) ||
                        tournament.Mvp.Name.ToLower().Contains(_searchText.ToLower())) return true;
                    }
                    return false;
                });
                TournamentsView.Refresh();
                base.OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }

        }
        public DateTime TournamentTime
        {
            get { return _tournamentTime; }
            set
            {
                _tournamentTime = value;
                OnPropertyChanged();
            }
        }
        public Player Mvp
        {
            get { return _mvp; }
            set
            {
                _mvp = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Match> ItemMatches
        {
            get { return _itemMatches; }
            set
            {
                _itemMatches = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                base.OnPropertyChanged();
            }
        }
        public Match NewMatch
        {
            get { return _newMatch; }
            set
            {
                _newMatch = value;
                base.OnPropertyChanged();
            }
        }
        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                _selectedMatch = value;
                base.OnPropertyChanged();
            }
        }
        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
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

        // Для комбобоксов
        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
                base.OnPropertyChanged();
            }
        }
        public ListOfTeams ListOfTeams
        {
            get { return _listOfTeams; }
            set
            {
                _listOfTeams = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// //// Конструкторы
        /// </summary>
        public ICollectionView TournamentsView { get; set; } // Свойство и автополе представление коллекции (для фильтрации)
        public TournamentsViewModel()
        {
            _items = FactoryOfLists.ObjTournaments;
            TournamentsView = CollectionViewSource.GetDefaultView(FactoryOfLists.ObjTournaments.List);

            _tournamentsListPage = new Views.TournamentPage();
            _tournamentsListPage.DataContext = this;
            _tournamentInfoPage = new Views.TournamentInfoPage();
            _tournamentInfoPage.DataContext = this;
            _createTournamentPage = new Views.CreateTournamentPage();
            _createTournamentPage.DataContext = this;
            _tournamentChangePage = new Views.ChangeTournamentPage();
            _tournamentChangePage.DataContext = this;
            CurrentPage = _tournamentsListPage;

            _renderTransformX = 0.5;
            _pageWidth = 600;

            _players = FactoryOfLists.ObjPlayers.List;
            _matches = FactoryOfLists.ObjMatches.List;
            _listOfTeams = FactoryOfLists.ObjTeams;
            _time = DateTime.Now;
        }

        //Метод
        public void OpenCreatingWindow()
        {
            Name = null;
            TournamentTime = DateTime.Now;
            Mvp = null;
            ItemMatches = new ObservableCollection<Match>();
            /*NewMatch = new Match();
            NewMatch.Team1 = Team1;
            NewMatch.Team2 = Team2;
            NewMatch.Winner = TeamWinner;*/
            /*Views.CreateTournamentWindow newWindow = new Views.CreateTournamentWindow();
            newWindow.DataContext = this;
            newWindow.ShowDialog();*/
            ChangePageAnimated(_createTournamentPage);
        }

        //Комманды
        public ICommand bOpenCreatingWindow_Command
        {// Открытие окна создания записи (турнира)
            get
            {
                return new DelegateCommand(() =>
                {
                    OpenCreatingWindow();
                });
            }
        }

        public ICommand bAddItem_Command
        {// Добавление в общий список
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    NewItem = new Tournament(Name, Time, Mvp, ItemMatches);
                    NewItem.Date = Time;
                    FactoryOfLists.ObjTournaments.List.Add(NewItem);
                    NewItem = null;
                    Name = null;
                    Time = DateTime.Now;
                    Mvp = null;
                    ItemMatches = new ObservableCollection<Match>();
                    if (w != null) w.Close();
                    ChangePageAnimated(_tournamentsListPage);
                    TournamentsView.Refresh();
                });
            }
        }

        public ICommand bAddMatch_Command
        { // Добавление нового матча
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    NewMatch = new Match();
                    NewMatch.Team1 = Team1;
                    NewMatch.Team2 = Team2;
                    NewMatch.Winner = TeamWinner;
                    NewMatch.Time = Time;
                    if (ItemMatches == null) ItemMatches = new ObservableCollection<Match>();
                    ItemMatches.Add(NewMatch);
                    if (w != null) w.Close();
                });
            }
        }
        public ICommand AddExistenceMatch
        { // Добавление матча на уже существующий
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    if (ItemMatches == null) ItemMatches = new ObservableCollection<Match>();
                    if (SelectedMatch != null) ItemMatches.Add(SelectedMatch);
                    w.Close();
                });
            }
        }

        public ICommand OpenAddMatchInTournamentWindow
        { // Добавление нового матча
            get
            {
                return new DelegateCommand(() =>
                {
                    Views.CreateMatchWindow newWindow = new Views.CreateMatchWindow();
                    newWindow.DataContext = this;
                    newWindow.ShowDialog();
                });
            }
        }

        public ICommand bOpenSelectedItem_Command
        { //Переключает на страницу с изменением/ удалением информации о выбранной записи
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedItem.Matches != null)
                    {
                        ItemMatches = SelectedItem.Matches;
                    }
                    else
                    {
                        ItemMatches = new ObservableCollection<Match>();
                    }
                    ChangePageAnimated(_tournamentChangePage);
                });
            }
        }

        public ICommand bInfoSelectedItem_Command
        {//Переключает на страницу с информацией о выбранном игроке
            get
            {
                return new DelegateCommand(() => {
                    if (SelectedItem != null)
                    {
                        Name = SelectedItem.Name;
                        Time = SelectedItem.Date;
                        Mvp = SelectedItem.Mvp;
                        Time = SelectedItem.Date;
                        if (SelectedItem.Matches != null)
                        {
                            ItemMatches = SelectedItem.Matches;
                        }
                        else
                        {
                            ItemMatches = new ObservableCollection<Match>();
                        }
                        ChangePageAnimated(_tournamentInfoPage);
                    }
                });
            }
        }

        public ICommand bBackToList_Command
        { // Переключает фрэйм со страницы информации о команде на страницу всего списка
            get
            {
                return new DelegateCommand(() => {
                    ChangePageAnimated(_tournamentsListPage);
                    TournamentsView.Refresh();
                });
            }
        }
        public ICommand bBackToInfo_Command
        { // Переключает фрэйм со страницы информации о команде на страницу всего списка
            get
            {
                return new DelegateCommand(() => {
                    SelectedItem.Name = Name;
                    SelectedItem.Date = TournamentTime;
                    SelectedItem.Mvp = Mvp;
                    SelectedItem.Matches = Matches;
                    ChangePageAnimated(_tournamentInfoPage);
                });
            }
        }

        public ICommand bChangeMatchInItem_Command
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Views.ChangingMatchInTournamentsWindow newWindow = new Views.ChangingMatchInTournamentsWindow();
                    newWindow.DataContext = this;
                    newWindow.ShowDialog();
                });
            }
        }

        public ICommand bRemoveSelectedMatch_Command
        {
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    SelectedItem.Matches.Remove(SelectedMatch);
                    if (w != null)
                    {
                        w.Close();
                    }
                });
            }
        }
        public ICommand bRemoveItem_Command
        { // Удаляет item из общего сиска
            get
            {
                return new DelegateCommand<Window>((w) =>
                {
                    if (SelectedItem != null)
                    {
                        FactoryOfLists.ObjTournaments.List.Remove(SelectedItem);
                        SelectedItem = null;
                        if (w != null)
                        {
                            w.Close();
                        }
                        ChangePageAnimated(_tournamentsListPage);
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

        public async void ChangePageAnimatedToLeft(Page page)
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
