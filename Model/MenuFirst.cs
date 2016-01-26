using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MenuFirst
    {
        private string _firstMenuPower;
        public string firstMenuPower
        {
            get { return this._firstMenuPower; }
            set { this._firstMenuPower = value; }
        }
        private string _firstMenuName;
        public string firstMenuName
        {
            get { return this._firstMenuName; }
            set { this._firstMenuName = value; }
        }
        private string _firstMenuNameUrl;
        public string firstMenuNameUrl
        {
            get { return this._firstMenuNameUrl; }
            set { this._firstMenuNameUrl = value; }
        }
        private int _freezeMenu = 0;
        public int freezeMenu
        {
            get { return this._freezeMenu; }
            set { this._freezeMenu = value; }
        }
    }
}
