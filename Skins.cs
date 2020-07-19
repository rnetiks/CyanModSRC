using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


	public class Skins
	{
      Dictionary<string,string> skinValue;
        public Skins()
        {
          skinValue = new Dictionary<string,string>();
        }
     
        public void Add(string name, string url)
        {
          skinValue.Add(name,url);
        }
        public void Remove(string name)
        {
            skinValue.Remove(name);
        }
        public Dictionary<string, string> List()
        {
            return skinValue;
        }
        public string ToStringFull(string param)
        {
            string strdfs = string.Empty;
            foreach(var sskin in skinValue)
            {
                strdfs = strdfs + "{" + sskin.Key + "," + sskin.Value + "}" + param;
            }
            return strdfs;
        }
        public string URL(string name)
        {
            foreach (var sd in skinValue)
            {
                if (sd.Key == name)
                {
                    return skinValue[name];
                }
            }
            return string.Empty;
        }
        public string ListFull()
        {
            string text = string.Empty;
            foreach (var dfdfs in skinValue)
            {
                text = text + dfdfs.Key + "`" + dfdfs.Value + "\n";
            }
            return text;
        }
        public void Save(string name, string url)
        {
            skinValue[name] = url;
        }

	}

