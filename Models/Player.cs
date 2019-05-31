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
    public class Player
    {
        string _name;
        int _age;
        double _kd;
        Team _team;
        //Video

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
            }
        }
        public double KD
        {
            get { return _kd; }
            set
            {
                _kd = value;
            }
        }
        public Team Team
        {
            get { return _team; }
            set
            {
                _team = value;
            }
        }

        public Player()
        {
            _name = "";
            _age = 0;
            _kd = 0;
            _team = new Team();
        } // для серриализации

        public Player(string name = "", int age = 0, double kd = 0, Team team = null)
        { //ctor
            this._name = name;
            this._age = age;
            this._kd = kd;
            this._team = (team != null) ? _team = team : _team = new Team();
        }

        public Player(Player playerParam)
        {   /// копирование
            _name = playerParam.Name;
            _age = playerParam.Age;
            _kd = playerParam.KD;
            _team = playerParam.Team;
        }

    }

    public class ListOfPlayers// : NotifycationsPropertyChanged
    {
        private ObservableCollection<Player> _list;
        public ObservableCollection<Player> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                //base.OnPropertyChanged();
            }
        }

        public ListOfPlayers()
        {
            _list = new ObservableCollection<Player>();
        } //для серриализации

        public ListOfPlayers(ObservableCollection<Player> listParam)
        {
            _list = listParam;
            /*_list = new ObservableCollection<Player>();
            if (listParam != null)
            {
                for (int i = 0; i < listParam.Count; i++)
                {
                    _list.Add(listParam[i]);
                }
            }*/
        }

        #region Queries //Запросы
        public ListOfPlayers FindName(string qwerry)//Поиск по имени
        {
            //IEnumerable<Player> resultList;
            if (qwerry != "")
            {
                //resultList = new List<Player>().AsEnumerable<Player>();
                var resultList = from item in this.List
                     where item.Name.ToLower().Contains(qwerry.ToLower())
                     select item;



                /*foreach (var item in this.List)
                {

                }*/
                return new ListOfPlayers(new ObservableCollection<Player>(resultList));
            }
            else
            {
                return this;
            }
        }
        #endregion

        public void Redresh()
        {
            Player testPlayer = new Player("testPlayer");
            _list.Add(testPlayer);
            _list.Remove(testPlayer);
        }
        public void Add()
        { }

        public void Update()
        { }

        public void Delete()
        { }


    }
}
