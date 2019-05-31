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
        ObservableCollection<Team> _top;
        public ObservableCollection<Team> Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        public Top10()
        {
            _top = new ObservableCollection<Team>();
        }

        public Top10(ObservableCollection<Team> top)
        {
            _top = top;
        }
    }
}
