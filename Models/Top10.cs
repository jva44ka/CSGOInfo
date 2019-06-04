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
    public class Top10
    {
        [XmlIgnore]
        public ObservableCollection<Team> Top { get; set; }
        //Для серриализации
        public int[] teamIds_Serialization { get; set; }
        //
        public Top10()
        {
            Top = new ObservableCollection<Team>();
        }

        public Top10(ObservableCollection<Team> top)
        {
            Top = top;
        }
    }
}
