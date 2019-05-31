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
    public class Tournament
    {
        string _name;
        DateTime _date;
        Player _mvp;
        ObservableCollection<Match> _matches;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
            }
        }
        public Player Mvp
        {
            get { return _mvp; }
            set
            {
                _mvp = value;
            }
        }
        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
            }
        }

        public Tournament() // Из-за datatime пришлось разделить гибридный конструктор на 2
        {
            _name = "someTournament";
            _date = new DateTime();
            _mvp = new Player();
            _matches = new ObservableCollection<Match>();
        }

        public Tournament(string name, DateTime date, Player mvp = null, ObservableCollection<Match> matches = null)
        {
            _name = (name != null) ? _name = name : _name = "someTournament";
            _date = (date != null) ? _date = date : _date = DateTime.Parse("01.01.0001 0:00:00");
            _mvp = (mvp != null) ? _mvp = mvp : _mvp = new Player();
            _matches = (matches != null) ? _matches = matches : _matches = new ObservableCollection<Match>();
        }
    }

    public class ListOfTournaments
    {
        private ObservableCollection<Tournament> _list;
        public ObservableCollection<Tournament> List
        {
            get { return _list; }
            set
            {
                _list = value;
            }
        }

        public ListOfTournaments()
        {
            _list = new ObservableCollection<Tournament>();
        }

        public ListOfTournaments(ObservableCollection<Tournament> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                _list.Add(list[i]);
            }
        }
    }
}
