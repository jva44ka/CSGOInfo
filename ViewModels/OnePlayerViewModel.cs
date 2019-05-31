using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfLawyersSystem.Models;
using WpfLawyersSystem.Views;

namespace WpfLawyersSystem.ViewModels
{
    public class OnePlayerViewModel : NotifycationsPropertyChanged
    {
        Player _newPlayer;
        Player _selectedPlayer;
        ListOfTeams _listOfTeams;
        public Player NewPlayer
        {
            get { return _newPlayer; }
            set
            {
                _newPlayer = value;
                base.OnPropertyChanged();
            }
        }
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                base.OnPropertyChanged();
            }
        }

        public ListOfTeams ListOfTeams
        {
            get { return _listOfTeams; }
            set
            {
                _listOfTeams = value;
                base.OnPropertyChanged();
            }
        }

        public OnePlayerViewModel() //ctor
        {
            _newPlayer = new Player();
            //_selectedPlayer = new Player();
            _listOfTeams = FactoryOfLists.ObjTeams;
        }
        public OnePlayerViewModel(Player player) //ctorParam
        {
            //_newPlayer = new Player();
            _selectedPlayer = player;
            _listOfTeams = FactoryOfLists.ObjTeams;
        }

        public ICommand bAdd_Command
        {
            get
            {
                return new RelayCommand(() =>
                FactoryOfLists.ObjPlayers.List.Add(new Player(_newPlayer))
                );
            }
        }

        /*public ICommand bUpdate_Command // меняется само по привязке соответственно в методе нет смысла
        {
            get
            {
                return new RelayCommand(() => 
                {
                    FactoryOfLists.ObjPlayers.List.Remove(_selectedPlayer);
                    FactoryOfLists.ObjPlayers.List.Add(_selectedPlayer);
                });
            }
        }*/

        public ICommand bRemove_Command
        {
            get
            {
                return new RelayCommand(() =>
                FactoryOfLists.ObjPlayers.List.Remove(_selectedPlayer)
                );
            }
        }


        private void Add() // добавить запись //дубликат комманды на всякий
        {
            FactoryOfLists.ObjPlayers.List.Add(_newPlayer);
        }

        public void Update() // обновить эту выбранную запись
        { }

        public void Delete() // удалить эту выбраную запись
        { }
    }
}
