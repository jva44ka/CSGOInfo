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
        [XmlIgnore]
        Team _team1;

        [XmlIgnore]
        Team _team2;

        [XmlIgnore]
        Team _winner;

        [XmlIgnore]
        public Team Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
            }
        }

        [XmlIgnore]
        public Team Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
            }
        }

        [XmlIgnore]
        public Team Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
            }
        }

        //Для серриализации
        public int id_Serialization { get; set; }
        public int team1Id_Serialization { get; set; }
        public int team2Id_Serialization { get; set; }
        public int teamWinnerId_Serialization { get; set; }
        //

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
        public ObservableCollection<Match> List { get; set; }

        public ListOfMatches()
        {
            List = new ObservableCollection<Match>();
        }

        public ListOfMatches(ObservableCollection<Match> ListParam = null)
        {
            List = new ObservableCollection<Match>();
            Team team1 = new Team("NaVi", 1.05, 15, 20);
            Team team2 = new Team("FaZe", 1.12, 22, 10);
            Match somematch = new Match(team1, team2, team1);
            List.Add(somematch);
            somematch = new Match(team2, team1, team2);
            List.Add(somematch);
            //Обычный конструктор
            /*for (int i = 0; i < ListParam.Count; i++)
            {
                _list.Add(ListParam[i]);
            }*/
        }
    }
}
