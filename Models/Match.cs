using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfLawyersSystem.Models
{
    public class Match
    {
        Team _team1;
        Team _team2;
        Team _winner;
        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
            }
        }
        public Team Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
            }
        }
        public Team Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
            }
        }

        public Match()
        {
            _team1 = new Team();
            _team2 = new Team();
            _winner = new Team();
        }

        public Match(Team team1 = null, Team team2 = null, Team winner = null)
        {
            this._team1 = team1;
            this._team2 = team2;
            this._winner = winner;
        }
    }

    public class ListOfMatches
    {
        private ObservableCollection<Match> _list;
        public ObservableCollection<Match> List
        {
            get { return _list; }
            set
            {
                _list = value;
            }
        }

        public ListOfMatches()
        {
            _list = new ObservableCollection<Match>();
        }

        public ListOfMatches(ObservableCollection<Match> ListParam = null)
        {
            _list = new ObservableCollection<Match>();
            Team team1 = new Team("NaVi", 1.05, 15, 20);
            Team team2 = new Team("FaZe", 1.12, 22, 10);
            Match somematch = new Match(team1, team2, team1);
            _list.Add(somematch);
            somematch = new Match(team2, team1, team2);
            _list.Add(somematch);
            //Обычный конструктор
            /*for (int i = 0; i < ListParam.Count; i++)
            {
                _list.Add(ListParam[i]);
            }*/
        }
    }
}
