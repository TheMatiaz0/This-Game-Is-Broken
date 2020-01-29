using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cyberevolver
{
    public class Log
    {
        public string Text
        {
            get => _Value;
            set
            {
                _Value = value;
                LogChanged(this, value);
            }
        }
#if UNITY_STANDALONE||UNITY_EDITOR
        [UnityEngine.SerializeField]
        [UnityEngine.TextArea]
#endif
        private string _Value;
        public event EventHandler<string> LogChanged = delegate { };
        public void Clear()
            => Text = String.Empty;
        public void AddLog(string added)
            => Text += $"\n {added}";
        public override string ToString()
        {
            return Text;
        }


    }


}

